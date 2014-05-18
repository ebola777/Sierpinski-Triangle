namespace SierpinskiTriangle.Views.Contracts
{
    using System;

    public interface IView<TModel> : IView
    {
        #region Public Properties

        TModel Model { get; set; }

        #endregion
    }

    public interface IView
    {
        #region Public Events

        event EventHandler Closed;

        event EventHandler Load;

        #endregion
    }
}