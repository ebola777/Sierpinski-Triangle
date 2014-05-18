namespace SierpinskiTriangle.Storage.Settings
{
    using System.ComponentModel;
    using System.Windows.Forms;

    using SierpinskiTriangle.Utilities;

    public class MainViewSettings
    {
        #region Constructors and Destructors

        public MainViewSettings()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);
        }

        #endregion

        #region Public Properties

        [DefaultValue(Defaults.Height)]
        public int? Height { get; set; }

        public int? Left { get; set; }

        public int? Top { get; set; }

        [DefaultValue(Defaults.Width)]
        public int? Width { get; set; }

        [DefaultValue(FormWindowState.Normal)]
        public FormWindowState WindowState { get; set; }

        #endregion

        internal static class Constraints
        {
            #region Constants

            public const int MinHeight = 300;

            public const int MinLeft = 0;

            public const int MinTop = 0;

            public const int MinWidth = 400;

            #endregion
        }

        internal static class Defaults
        {
            #region Constants

            public const int Height = 600;

            public const int Width = 800;

            #endregion
        }
    }
}