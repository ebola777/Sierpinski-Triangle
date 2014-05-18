namespace SierpinskiTriangle.Storage.Settings
{
    using System.IO;

    using SierpinskiTriangle.Utilities;

    public class FileInfoSettings
    {
        #region Constants

        private const string DOCK_PANEL_SUITE_LAYOUT = @"settings/layout.xml";

        private const string PASCAL_TRIANGLE_SEQUENCE_CACHE = @"cache/sequence.json";

        #endregion

        #region Constructors and Destructors

        public FileInfoSettings()
        {
            this.PathLayout = Path.Combine(FileSystemHelper.GetAssemblyDirectory(), DOCK_PANEL_SUITE_LAYOUT);
            this.PathSequenceCache = Path.Combine(
                FileSystemHelper.GetAssemblyDirectory(),
                PASCAL_TRIANGLE_SEQUENCE_CACHE);
        }

        #endregion

        #region Public Properties

        public string PathLayout { private set; get; }

        public string PathSequenceCache { private set; get; }

        #endregion
    }
}