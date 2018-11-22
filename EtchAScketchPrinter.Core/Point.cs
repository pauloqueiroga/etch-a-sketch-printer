namespace PQ.EtchASketchPrinter.Core
{
    public class Point
    {
        public Point(Point fromPoint)
        {
            X = fromPoint.X;
            Y = fromPoint.Y;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; internal set; }
        public int Y { get; internal set; }
    }
}