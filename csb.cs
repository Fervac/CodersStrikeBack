using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

class Player
{
    static void Main(string[] args)
    {
        int thrust = 100;
        bool boostUp = true;
        string[] inputs;
        List<Vector2> cps = new List<Vector2>();

        while (true)
        {
            inputs = Console.ReadLine().Split(' ');

            int nextCheckpointDist = int.Parse(inputs[4]);
            int nextCheckpointAngle = int.Parse(inputs[5]);

            // My pod coordinates
            Vector2 myVector = new Vector2(((float)int.Parse(inputs[0])), ((float)int.Parse(inputs[1])));

            // Checkpoint coordinates
            Vector2 checkpointVector = new Vector2(((float)int.Parse(inputs[2])), ((float)int.Parse(inputs[3])));

            // Store checkpoints
            if (!cps.Contains(new Vector2(checkpointVector.X, checkpointVector.Y)))
            {
                cps.Add(new Vector2(checkpointVector.X, checkpointVector.Y));
                Console.Error.WriteLine("cp.x: " + checkpointVector.X);
                Console.Error.WriteLine("cp.y: " + checkpointVector.Y);
            }

            inputs = Console.ReadLine().Split(' ');

            // Enemy coordinates
            Vector2 enemyVector = new Vector2(((float)int.Parse(inputs[0])), ((float)int.Parse(inputs[1])));

            // Distance between me and enemy
            float meToEnemyDist = Vector2.Distance(myVector, enemyVector);

            // Distance between enemy and checkpoint
            float EnemyToCheckpointDist = Vector2.Distance(enemyVector, checkpointVector);

            // Distance between me and checkpoint in float
            float meToCheckpointDist = Vector2.Distance(myVector, checkpointVector);

            // Pseudo code for perfect acceleration / deceleration after we stored all checkpoints...
            // velocity = normalize(checkpointVector - myVector) * 100;
            // desired_velocity = normalize(nextCheckpointVector - myVector) * 100;
            // steering = desired_velocity - velocity;

            // 0 thrust if wrong way
            if (nextCheckpointAngle > 90 || nextCheckpointAngle < -90)
                thrust = 0;
            else if (nextCheckpointDist < 1600)
                thrust = 40;
            else if (nextCheckpointDist < 1100)
                thrust = 25;
            else if (nextCheckpointDist < 800)
                thrust = 10;
            else
                thrust = 100;

            // Shield when ennemy get to a checkpoint just before us
            if (thrust > 80 && meToEnemyDist < 1100 && meToCheckpointDist > EnemyToCheckpointDist && meToCheckpointDist < 1000)
            {
                Console.WriteLine(checkpointVector.X + " " + checkpointVector.Y + " " + "SHIELD");
            }
            else if (nextCheckpointAngle == 0 && nextCheckpointDist > 5000 && boostUp == true) // Boost if long straight line
            {
                Console.WriteLine(checkpointVector.X + " " + checkpointVector.Y + " " + "BOOST");
                boostUp = false;
            }
            else
                Console.WriteLine(checkpointVector.X + " " + checkpointVector.Y + " " + thrust);
        }
    }
}
