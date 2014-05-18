namespace SierpinskiTriangle.Views.Observers
{
    using SierpinskiTriangle.Models.Control;
    using SierpinskiTriangle.Views.Observers.Contracts;

    public class GraphViewObserver : IObserver
    {
        #region Fields

        public UpdateGraphDelegate UpdateGraphHandler;

        #endregion

        #region Delegates

        public delegate void UpdateGraphDelegate(ControlModel model);

        #endregion
    }
}