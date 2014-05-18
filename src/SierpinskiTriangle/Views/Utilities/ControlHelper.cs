namespace SierpinskiTriangle.Views.Utilities
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public static class ControlHelper
    {
        #region Public Methods and Operators

        public static void FitToScreen(Control ctrl, Rectangle rect, Rectangle minBounds)
        {
            Rectangle maxBounds = Screen.FromControl(ctrl).Bounds;
            var sz = new Size
                         {
                             Width = Math.Min(Math.Max(rect.Width, minBounds.Width), maxBounds.Width),
                             Height = Math.Min(Math.Max(rect.Height, minBounds.Height), maxBounds.Height)
                         };

            ctrl.Left = Math.Min(Math.Max(rect.Left, minBounds.Left), maxBounds.Width - sz.Width);
            ctrl.Top = Math.Min(Math.Max(rect.Top, minBounds.Top), maxBounds.Height - sz.Height);

            ctrl.Size = sz;
        }

        public static Point GetCenterScreenPos(Control ctrl, Size sz)
        {
            Rectangle maxBounds = Screen.FromControl(ctrl).Bounds;

            return new Point((maxBounds.Width - sz.Width) / 2, (maxBounds.Height - sz.Height) / 2);
        }

        #endregion
    }
}