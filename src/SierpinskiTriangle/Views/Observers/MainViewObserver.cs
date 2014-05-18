namespace SierpinskiTriangle.Views.Observers
{
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Observers.Contracts;
    using SierpinskiTriangle.Views.Utilities;

    using WeifenLuo.WinFormsUI.Docking;

    public class MainViewObserver : IObserver
    {
        #region Delegates

        public delegate void UpdateFormOpenStateDelgate(IView view, bool en);

        #endregion

        #region Public Events

        public event UpdateFormOpenStateDelgate UpdateFormOpenStateHandler;

        #endregion

        #region Public Methods and Operators

        public void UpdateFormOpenStateHelper(IView view)
        {
            this.UpdateFormOpenStateHandler(view, DockPanelCustom.IsFormOpen((DockContent)view));
        }

        #endregion
    }
}