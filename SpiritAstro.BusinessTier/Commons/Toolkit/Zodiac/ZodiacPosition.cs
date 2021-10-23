using System;
using System.Drawing;

namespace SpiritAstro.BusinessTier.Commons.Toolkit.Zodiac
{
    public class ZodiacPosition
    {
        private const int XCentre = 512;
        private const int YCentre = 363;

        private const int YBase = 580;

        private const double BaseDre = Math.PI / 6;

        public int X { get; set; }
        public int Y { get; set; }
        public ZodiacEnum Zodiac { get; set; }
        public double Dre { get; set; }

        public ZodiacPosition(ZodiacEnum zodiac, double dre)
        {
            Zodiac = zodiac;
            Dre = ((int)Zodiac - 1) * BaseDre + DreToRadian(dre);
            (X, Y) = RevertSpinPoint();
        }

        private (int, int) RevertSpinPoint()
        {
            var newX = -(YBase - YCentre) * Math.Sin(-Dre) + XCentre;
            var newY = (YBase - YCentre) * Math.Cos(-Dre) + YCentre;
            return ((int, int))(newX, newY);
        }

        public static void MyDrawLine(Graphics g, Pen pen, ZodiacPosition zp1, ZodiacPosition zp2)
        {
            g.DrawLine(pen, zp1.X, zp1.Y, zp2.X, zp2.Y);
        }

        private static double DreToRadian(double dre)
        {
            return Math.PI * dre / 180;
        }

        public double GetAngleMeasure(ZodiacPosition position)
        {
            var result = Math.Abs(Dre - position.Dre);
            if (result > Math.PI)
            {
                result = 2 * Math.PI - result;
            }

            return result * 180 / Math.PI;
        }
    }
}