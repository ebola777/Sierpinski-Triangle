namespace SierpinskiTriangle.Storage.Settings
{
    using SierpinskiTriangle.Models.Control;

    public class ControlViewSettings
    {
        #region Constructors and Destructors

        public ControlViewSettings()
        {
            this.ControlModel = new ControlModel();
        }

        #endregion

        #region Public Properties

        public ControlModel ControlModel { get; set; }

        #endregion
    }
}