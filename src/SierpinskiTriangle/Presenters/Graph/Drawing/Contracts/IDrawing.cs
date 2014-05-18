namespace SierpinskiTriangle.Presenters.Graph.Drawing.Contracts
{
    using System.Collections.Generic;
    using System.Drawing;

    using Accord.MachineLearning.Structures;

    using SierpinskiTriangle.Models.Control;

    using ZedGraph;

    public interface IDrawing<TVal, TObj> : IDrawing
        where TObj : GraphObj
    {
        #region Public Properties

        IDictionary<double[], MetaInfo<TVal, TObj>> Lookup { get; set; }

        IList<TVal> Sequence { set; }

        IList<bool> VisData { set; }

        #endregion
    }

    public interface IDrawing
    {
        #region Public Properties

        GraphPane Pane { get; set; }

        KDTree<double> Points { get; set; }

        PointF[] ScreenRect { get; }

        #endregion

        #region Public Methods and Operators

        void Draw(Pattern patternSettings, Style styleSettings);

        #endregion
    }
}