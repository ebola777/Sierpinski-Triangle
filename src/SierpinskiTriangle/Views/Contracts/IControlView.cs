namespace SierpinskiTriangle.Views.Contracts
{
    using System;

    using SierpinskiTriangle.Models.Control;

    public interface IControlView : IView<ControlModel>
    {
        #region Public Events

        event EventHandler UpdateClick;

        event EventHandler ViewDockStateChanged;

        #endregion

        #region Public Methods and Operators

        void ExpandPropertyGrid();

        void SetVisible(bool vis);

        void UpdatePropertyGridSource();

        #endregion
    }
}