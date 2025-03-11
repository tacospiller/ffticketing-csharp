namespace ffTicketingCsharp
{
    internal static class MiscExtensions
    {
        public static Point Center(this Rectangle r)
        {
            var x = (r.Left + r.Right) / 2;
            var y = (r.Top + r.Bottom) / 2;
            return new Point(x, y);
        }

        public static bool IsGraytone(this Color c)
        {
            if (Math.Abs(c.R - c.G) < 30 && Math.Abs(c.R - c.B) < 30)
            {
                return true;
            }

            return false;
        }

        public static Point Center(this Form f)
        {
            var x = (double)f.Size.Width / 2.0f + (double)f.Location.X;
            var y = (double)f.Size.Height / 2.0f + (double)f.Location.Y;
            return new Point((int)x, (int)y);
        }

        public static Point Add(this Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
    }
}
