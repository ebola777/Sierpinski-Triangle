namespace SierpinskiTriangle.Views
{
    using System;
    using System.Collections.Generic;

    using SierpinskiTriangle.Views.Contracts;

    public class ViewFactory
    {
        #region Fields

        private readonly Dictionary<Type, IView> _views = new Dictionary<Type, IView>();

        #endregion

        #region Public Properties

        public Dictionary<Type, IView> Views
        {
            get
            {
                return this._views;
            }
        }

        #endregion

        #region Public Methods and Operators

        public TView Get<TView>() where TView : IView
        {
            IView view;

            this._views.TryGetValue(typeof(TView), out view);
            if (null != view)
            {
                return (TView)view;
            }

            view = (IView)Activator.CreateInstance(typeof(TView));
            this._views[typeof(TView)] = view;
            return (TView)view;
        }

        #endregion
    }
}