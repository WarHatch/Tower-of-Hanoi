using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tower_of_Hanoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_of_Hanoi.Tests
{
    [TestClass()]
    public class ProgramTests
    {

        [TestMethod()]
        public void SolveFrom1to10Test()
        {
            for (int gameRingCount = 1; gameRingCount <= 10; gameRingCount++)
            {
                List<Tower> towers = new List<Tower>(3)
                {
                    new StackedTower(gameRingCount),
                    new EmptyTower(),
                    new EmptyTower()
                };

                Program.Solve(towers, "solve-towers-with-" + gameRingCount + "rings");

                Assert.IsTrue(towers[0].RingCount == 0);
                Assert.IsTrue(towers[1].RingCount == 0);
                Assert.IsTrue(towers.Last().RingCount == gameRingCount);
            }
        }
    }
}