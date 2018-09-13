using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_of_Hanoi
{
    public interface Tower
    {
        void MoveRingOnto(Tower otherTower);
        void AddRing(int ring);
        int RingCount { get; }
        int PeekTopRing { get; }
        bool HasRings { get; }
    }
}
