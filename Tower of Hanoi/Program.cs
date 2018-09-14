using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_of_Hanoi
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Define ring count: ");
            int gameRingCount = Convert.ToInt32(Console.ReadLine());

            List<Tower> towers = new List<Tower>(3)
            {
                new StackedTower(gameRingCount),
                new EmptyTower(),
                new EmptyTower()
            };

            Solve(towers);

            Console.ReadKey();
        }

        // Assumes that there are 3 towers
        public static void Solve(List<Tower> towers, string logOutputFilename = "")
        {
            var stackedTower = towers.OfType<StackedTower>().First();
            int direction = stackedTower.RingCount % 2 == 0 ? 1 : -1;
            int gameRingCount = stackedTower.RingCount;

            while (towers.Last().RingCount != gameRingCount)
            {
                int ringMovedTowerIndex = -1;
                for (int towerIndex = 0; towerIndex < towers.Count; towerIndex++)
                {
                    bool ringMoved = false;
                    int ringLosingTowerIndex = NextInCircle(0, towerIndex, 3);
                    if (ringLosingTowerIndex != ringMovedTowerIndex && towers[ringLosingTowerIndex].HasRings)
                    {
                        var ringLosingTower = towers[ringLosingTowerIndex];

                        // Find where to place next ring
                        for (int move = 1; move < towers.Count && !ringMoved; move++)
                        {
                            int ringReceivingTowerIndex = NextInCircle(ringLosingTowerIndex, direction * move, 3);
                            if (!towers[ringReceivingTowerIndex].HasRings ||
                                towers[ringReceivingTowerIndex].PeekTopRing > towers[ringLosingTowerIndex].PeekTopRing)
                            {
                                string newLogEntry = CreateLogEntry(towers, ringLosingTowerIndex, ringReceivingTowerIndex);
                                Console.WriteLine(newLogEntry);
                                if (logOutputFilename != "")
                                    using (StreamWriter logOutputFile = File.AppendText(
                                        (DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "-" + logOutputFilename + ".txt")))
                                    {
                                        logOutputFile.WriteLine(newLogEntry);
                                    }

                                towers[ringLosingTowerIndex].MoveRingOnto(towers[ringReceivingTowerIndex]);
                                ringMovedTowerIndex = ringReceivingTowerIndex;
                                ringMoved = true;
                            }
                        }
                    }
                    if (ringMoved) { towerIndex = -1; } // Look at towers from start
                }

                if (towers.Last().RingCount != gameRingCount)
                    throw new Exception("No more moves available");
            }
        }

        private static int NextInCircle(int currentPosition, int move, int circleSize)
        {
            int nextPosition = (currentPosition + move) % circleSize;
            return (nextPosition < 0 ? nextPosition + circleSize : nextPosition);
        }

        private static string CreateLogEntry(List<Tower> towers, int ringLosingTowerIndex, int ringReceivingTowerIndex)
        {
            return "Ring of size " + towers[ringLosingTowerIndex].PeekTopRing +
                            " from tower " + ringLosingTowerIndex +
                            " was moved to tower " + ringReceivingTowerIndex;
        }

        private static void LogOutputToFile(string filename, string logMessage)
        {
            using (StreamWriter output = File.AppendText(filename))
            {
                output.WriteLine(logMessage);
            }
        }
    }
}
