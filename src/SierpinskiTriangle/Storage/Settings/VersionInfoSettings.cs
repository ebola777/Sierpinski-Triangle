namespace SierpinskiTriangle.Storage.Settings
{
    using SierpinskiTriangle.Utilities;

    public class VersionInfoSettings
    {
        #region Constructors and Destructors

        public VersionInfoSettings()
        {
            this.Version = AssemblyInfo.GetAssemblyVersion().ToString();
        }

        #endregion

        #region Public Properties

        public string Version { get; set; }

        #endregion
    }
}