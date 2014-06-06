namespace SierpinskiTriangle.Presenters.Graph.Drawing.Base
{
    using System.Collections.Generic;

    using ZedGraph;

    public abstract class RightAngledPatternBase<TVal> : PolyObjBase<TVal>
    {
        #region Constructors and Destructors

        protected RightAngledPatternBase(GraphPane pane, IList<TVal> sequence, IList<bool> visData)
            : base(pane, sequence, visData)
        {
        }

        #endregion
    }
}