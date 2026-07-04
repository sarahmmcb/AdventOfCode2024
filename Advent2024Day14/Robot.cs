using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024Day14
{
    public class Robot(OrderedPair position, OrderedPair velocity)
    {
        public OrderedPair Position { get; set; } = position;
        public OrderedPair Velocity { get; set; } = velocity;

        public void BoundedMove(int Xbound, int Ybound)
        {
            Position = Position.BoundedAdd(Velocity, Xbound, Ybound);
        }
    }
}
