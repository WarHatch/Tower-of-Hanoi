using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_of_Hanoi
{
    public class StackedTower: EmptyTower 
    {
        public StackedTower(int ringCount)
        {
            for (int i = ringCount; i > 0; i--)
            {
                stack.Push(i);
            }
        }
    }
}
