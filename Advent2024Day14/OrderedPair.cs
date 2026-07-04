
namespace Advent2024Day14
{
    public class OrderedPair
    {
        public int X { get; set; }
        public int Y { get; set; }

        public OrderedPair BoundedAdd(OrderedPair point, int Xbound, int Ybound)
        {
            var newX = X + point.X;
            var newY = Y + point.Y;

            if ( newX >= Xbound )
            {
                newX -= Xbound;
            }

            if (newX < 0 )
            {
                newX += Xbound;
            }

            if (newY >= Ybound)
            {
                newY -= Ybound;
            }

            if (newY < 0)
            {
                newY += Ybound;
            }

            return new OrderedPair { X = newX, Y = newY };
        }
    }
}
