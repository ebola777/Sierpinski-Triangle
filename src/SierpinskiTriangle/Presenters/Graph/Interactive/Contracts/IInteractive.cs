namespace SierpinskiTriangle.Presenters.Graph.Interactive.Contracts
{
    using System.Collections.Generic;
    using System.Drawing;

    using Accord.MachineLearning.Structures;

    using SierpinskiTriangle.Presenters.Graph.Drawing;

    using ZedGraph;

    public interface IInteractive<TVal, TObj> : IInteractive
    {
        #region Public Properties

        IDictionary<double[], MetaInfo<TVal, TObj>> Lookup { get; set; }

        KDTree<double> Points { get; set; }

        #endregion
    }

    public interface IInteractive
    {
        #region Public Properties

        SizeF FrameSize { get; set; }

        ZedGraphControl Graph { get; set; }

        #endregion

        #region Public Methods and Operators

        void Reset();

        void ShowNearestObjectInfo(double ptX, double ptY);

        #endregion
    }
}