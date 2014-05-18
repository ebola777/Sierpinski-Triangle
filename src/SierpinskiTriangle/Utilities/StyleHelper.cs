namespace SierpinskiTriangle.Utilities
{
    using System.Drawing;

    public static class StyleHelper
    {
        #region Public Methods and Operators

        public static Color GetInvertedColor(Color col)
        {
            return Color.FromArgb(col.ToArgb() ^ 0xFFFFFF);
        }

        #endregion
    }
}