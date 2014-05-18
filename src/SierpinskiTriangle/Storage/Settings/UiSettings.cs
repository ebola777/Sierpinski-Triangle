namespace SierpinskiTriangle.Storage.Settings
{
    using System.ComponentModel;

    using SierpinskiTriangle.Utilities;

    public class UiSettings
    {
        #region Constructors and Destructors

        public UiSettings()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);
        }

        #endregion

        #region Public Properties

        [DefaultValue("")]
        public string Language { get; set; }

        #endregion
    }
}