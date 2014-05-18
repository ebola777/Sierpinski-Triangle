namespace SierpinskiTriangle.Presenters.Base
{
    using SierpinskiTriangle.Presenters.Contracts;
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Observers.Contracts;

    public abstract class PresenterBase<TView, TObserver> : IPresenter<TView, TObserver>
        where TView : class, IView where TObserver : IObserver
    {
        #region Fields

        private readonly TObserver _observer;

        private readonly TView _view;

        #endregion

        #region Constructors and Destructors

        protected PresenterBase(TView view, TObserver observer)
        {
            this._view = view;
            this._observer = observer;
        }

        #endregion

        #region Public Properties

        public TObserver Observer
        {
            get
            {
                return this._observer;
            }
        }

        public TView View
        {
            get
            {
                return this._view;
            }
        }

        #endregion
    }
}