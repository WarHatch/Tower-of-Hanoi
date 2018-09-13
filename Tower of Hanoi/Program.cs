using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_of_Hanoi
{
    class Program
    {
        const int GameRingCount = 5;
        static void Main(string[] args)
        {
            List<Tower> towers = new List<Tower>(3)
            {
                new StackedTower(GameRingCount),
                new EmptyTower(),
                new EmptyTower()
            };

            Solve(towers);
        }

        // Assumes that there are 3 towers
        static void Solve(List<Tower> towers)
        {
            var stackedTower = towers.OfType<StackedTower>().First();
            int direction = stackedTower.RingCount % 2 == 0 ? 1 : -1;

            while (towers.Last().RingCount != GameRingCount)
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
                                towers[ringLosingTowerIndex].MoveRingOnto(towers[ringReceivingTowerIndex]);
                                ringMovedTowerIndex = ringReceivingTowerIndex;
                                ringMoved = true;
                            }
                        }
                    }
                    if (ringMoved) { towerIndex = -1; } // Look at towers from start
                }

                if (towers.Last().RingCount != GameRingCount)
                    throw new Exception("No more moves available");
            }
        }

        private static int NextInCircle(int currentPosition, int move, int circleSize)
        {
            int nextPosition = (currentPosition + move) % circleSize;
            return (nextPosition < 0 ? nextPosition + circleSize : nextPosition);
        }
    }
}
