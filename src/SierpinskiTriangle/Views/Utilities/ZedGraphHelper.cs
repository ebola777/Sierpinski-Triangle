namespace SierpinskiTriangle.Views.Utilities
{
    using System;
    using System.Drawing;

    using ZedGraph;

    public static class ZedGraphHelper
    {
        #region Public Methods and Operators

        public static void Offset(GraphPane pane, SizeF ofs)
        {
            pane.XAxis.Scale.Min -= ofs.Width;
            pane.XAxis.Scale.Max += ofs.Width;
            pane.YAxis.Scale.Min -= ofs.Height;
            pane.YAxis.Scale.Max += ofs.Height;
        }

        public static void Scale(GraphPane pane, SizeF ratio)
        {
            double xDis = Math.Abs(pane.XAxis.Scale.Max - pane.XAxis.Scale.Min);
            double yDis = Math.Abs(pane.YAxis.Scale.Max - pane.YAxis.Scale.Min);
            double xCenter = (pane.XAxis.Scale.Max + pane.XAxis.Scale.Min) / 2;
            double yCenter = (pane.YAxis.Scale.Max + pane.YAxis.Scale.Min) / 2;

            pane.XAxis.Scale.Min = xCenter - (xDis / 2 / ratio.Width);
            pane.XAxis.Scale.Max = xCenter + (xDis / 2 / ratio.Width);
            pane.YAxis.Scale.Min = yCenter - (yDis / 2 / ratio.Height);
            pane.YAxis.Scale.Max = yCenter + (yDis / 2 / ratio.Height);
        }

        public static void SetEqualScaleByX(GraphPane pane)
        {
            double xDis = Math.Abs(pane.XAxis.Scale.Max - pane.XAxis.Scale.Min);
            double yDis = Math.Abs(pane.YAxis.Scale.Max - pane.YAxis.Scale.Min);
            double yCenter = (pane.YAxis.Scale.Max + pane.YAxis.Scale.Min) / 2;
            double yTopOfs = pane.YAxis.Scale.Max - yCenter;
            double xPixPerUnit = pane.Rect.Width / xDis;
            double yPixPerUnit = pane.Rect.Height / yDis;
            double ratioYtoXUnit = yPixPerUnit / xPixPerUnit;

            // lock aspect ratio
            pane.YAxis.Scale.Max = yCenter + (yTopOfs * ratioYtoXUnit);
            pane.YAxis.Scale.Min = yCenter - (yTopOfs * ratioYtoXUnit);
        }

        public static void SetEqualScaleByY(GraphPane pane)
        {
            double xDis = Math.Abs(pane.XAxis.Scale.Max - pane.XAxis.Scale.Min);
            double yDis = Math.Abs(pane.YAxis.Scale.Max - pane.YAxis.Scale.Min);
            double xCenter = (pane.XAxis.Scale.Max + pane.XAxis.Scale.Min) / 2;
            double xTopOfs = pane.XAxis.Scale.Max - xCenter;
            double xPixPerUnit = pane.Rect.Width / xDis;
            double yPixPerUnit = pane.Rect.Height / yDis;
            double ratioXtoYUnit = xPixPerUnit / yPixPerUnit;

            // lock aspect ratio
            pane.XAxis.Scale.Max = xCenter + (xTopOfs * ratioXtoYUnit);
            pane.XAxis.Scale.Min = xCenter - (xTopOfs * ratioXtoYUnit);
        }

        public static void SetEqualScaleContain(GraphPane pane)
        {
            double xDis = Math.Abs(pane.XAxis.Scale.Max - pane.XAxis.Scale.Min);
            double yDis = Math.Abs(pane.YAxis.Scale.Max - pane.YAxis.Scale.Min);
            double dataRatio = xDis / yDis;
            float pixRatio = pane.Rect.Width / pane.Rect.Height;

            if (dataRatio > pixRatio)
            {
                SetEqualScaleByX(pane);
            }
            else
            {
                SetEqualScaleByY(pane);
            }
        }

        #endregion
    }
}