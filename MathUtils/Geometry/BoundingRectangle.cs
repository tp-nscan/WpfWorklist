using System.Windows;

namespace MathUtils.Geometry
{
    public static class BoundingRectangle
    {

        public static Rect BoundingRect(this Point center, double radius)
        {
            return new Rect(new Point(center.X - radius, center.Y - radius), new Size(radius * 2, radius * 2));
        }

        public static Rect BoundingRect(this Point center, double width, double height)
        {
            return new Rect(new Point(center.X - width / 2, center.Y - height/2), new Size(width, height));
        }

        //public static bool Contains(this Rect rect, Point point)
        //{
        //    if(point.X < rect.Left)
        //    {
        //        return false;
        //    }
        //    if (point.X > rect.Right)
        //    {
        //        return false;
        //    }

        //    if (point.Y < rect.Bottom)
        //    {
        //        return false;
        //    }
        //    if (point.Y > rect.Top)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
