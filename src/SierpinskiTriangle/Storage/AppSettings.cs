namespace SierpinskiTriangle.Storage
{
    using SierpinskiTriangle.Storage.Settings;

    public class AppSettings
    {
        #region Constructors and Destructors

        public AppSettings()
        {
            this.Version = new VersionInfoSettings();
            this.MainView = new MainViewSettings();
            this.ControlView = new ControlViewSettings();
            this.GraphView = new GraphViewSettings();
        }

        #endregion

        #region Public Properties

        public ControlViewSettings ControlView { get; set; }

        public FileInfoSettings FileInfo { get; set; }

        public GraphViewSettings GraphView { get; set; }

        public MainViewSettings MainView { get; set; }

        public UiSettings UiSettings { get; set; }

        public VersionInfoSettings Version { get; set; }

        #endregion
    }
}