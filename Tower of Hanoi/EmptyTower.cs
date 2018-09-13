using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_of_Hanoi
{
    public class EmptyTower : Tower
    {
        protected Stack<int> stack = new Stack<int>();

        public int RingCount => stack.Count;
        public int PeekTopRing => HasRings ? stack.Peek() : 0;
        public bool HasRings => stack.Count > 0;

        public void AddRing(int ring) => stack.Push(ring);

        public void MoveRingOnto(Tower otherTower)
        {
            if (otherTower.PeekTopRing <= PeekTopRing && otherTower.HasRings)
                throw new Exception("Tried to put " + otherTower.PeekTopRing + " on top of " + PeekTopRing);
            otherTower.AddRing(stack.Pop());
        }
    }
}

