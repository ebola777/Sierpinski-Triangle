namespace SierpinskiTriangle.Storage.Settings
{
    using System.ComponentModel;

    using SierpinskiTriangle.Utilities;

    public class GraphViewSettings
    {
        #region Constructors and Destructors

        public GraphViewSettings()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);
        }

        #endregion

        #region Public Properties

        [DefaultValue(20000)]
        public int CacheMaxNumItems { get; set; }

        #endregion
    }
}