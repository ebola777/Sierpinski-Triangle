namespace SierpinskiTriangle.Views.Contracts
{
    using System;
    using System.Windows.Forms;

    using SierpinskiTriangle.Models.Graph;

    using ZedGraph;

    public interface IGraphView : IView<GraphModel>
    {
        #region Public Events

        event MouseEventHandler GraphMouseClick;

        event EventHandler ViewDockStateChanged;

        #endregion

        #region Public Properties

        ZedGraphControl ZedGraph { get; }

        #endregion

        #region Public Methods and Operators

        void SetVisible(bool vis);

        #endregion
    }
}