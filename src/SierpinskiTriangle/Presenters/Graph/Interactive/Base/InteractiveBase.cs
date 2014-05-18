namespace SierpinskiTriangle.Presenters.Graph.Interactive.Base
{
    using System.Collections.Generic;
    using System.Drawing;

    using Accord.MachineLearning.Structures;

    using SierpinskiTriangle.Presenters.Graph.Drawing;
    using SierpinskiTriangle.Presenters.Graph.Interactive.Contracts;

    using ZedGraph;

    public abstract class InteractiveBase<TVal, TObj> : IInteractive<TVal, TObj>
    {
        #region Constructors and Destructors

        protected InteractiveBase(ZedGraphControl graph, SizeF frameSize)
        {
            this.FrameSize = frameSize;
            this.Graph = graph;
        }

        #endregion

        #region Public Properties

        public SizeF FrameSize { get; set; }

        public ZedGraphControl Graph { get; set; }

        public IDictionary<double[], MetaInfo<TVal, TObj>> Lookup { get; set; }

        public KDTree<double> Points { get; set; }

        #endregion

        #region Public Methods and Operators

        public abstract void Reset();

        public abstract void ShowNearestObjectInfo(double ptX, double ptY);

        #endregion
    }
}