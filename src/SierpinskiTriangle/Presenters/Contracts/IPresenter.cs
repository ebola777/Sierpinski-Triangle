namespace SierpinskiTriangle.Presenters.Contracts
{
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Observers.Contracts;

    public interface IPresenter<out TView, out TObserver> : IPresenter
        where TView : class, IView where TObserver : IObserver
    {
        #region Public Properties

        TObserver Observer { get; }

        TView View { get; }

        #endregion
    }

    public interface IPresenter
    {
    }
}