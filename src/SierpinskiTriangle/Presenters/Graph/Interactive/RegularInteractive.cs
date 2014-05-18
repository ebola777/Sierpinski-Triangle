namespace SierpinskiTriangle.Presenters.Graph.Interactive
{
    using System;
    using System.Drawing;
    using System.Numerics;

    using Accord.MachineLearning.Structures;

    using SierpinskiTriangle.Presenters.Graph.Drawing;
    using SierpinskiTriangle.Presenters.Graph.Interactive.Base;
    using SierpinskiTriangle.Utilities;

    using ZedGraph;

    public class RegularInteractive : InteractiveBase<BigInteger, PolyObj>
    {
        #region Constants

        private const string EMPTY_TITLE = "-";

        #endregion

        #region Fields

        private PolyObj _lastSelectedObj;

        #endregion

        #region Constructors and Destructors

        public RegularInteractive(ZedGraphControl graph, SizeF frameSize)
            : base(graph, frameSize)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override void Reset()
        {
            this.UnhighlightObject(this._lastSelectedObj);
            UpdateTitle(this.Graph, EMPTY_TITLE);
        }

        public override void ShowNearestObjectInfo(double ptX, double ptY)
        {
            var query = new[] { ptX, ptY };
            KDTreeNodeCollection<double> neighbors = this.Points.Nearest(
                query,
                Math.Max(this.FrameSize.Width / 2, this.FrameSize.Height / 2),
                1);
            ZedGraphControl graph = this.Graph;

            if (0 != neighbors.Count)
            {
                double[] pos = neighbors.Nearest.Position;
                MetaInfo<BigInteger, PolyObj> meta;

                if (this.Lookup.TryGetValue(pos, out meta))
                {
                    // highlight object
                    this.SelectObject(meta.Obj);

                    // update info
                    UpdateTitle(graph, string.Format("Number: {0} Visible: {1}", meta.Number, meta.Visible));
                }
                else
                {
                    this.UnhighlightObject(this._lastSelectedObj);

                    // update info
                    UpdateTitle(graph, EMPTY_TITLE);
                }
            }
            else
            {
                this.UnhighlightObject(this._lastSelectedObj);

                // update info
                UpdateTitle(graph, EMPTY_TITLE);
            }
        }

        #endregion

        #region Methods

        private static void UpdateTitle(ZedGraphControl graph, string text)
        {
            graph.GraphPane.Title.Text = text;
            graph.Invalidate();
        }

        private void HighlightObject(PolyObj obj)
        {
            obj.Border.Width *= 3;
            obj.Border.Color = StyleHelper.GetInvertedColor(obj.Border.Color);
            obj.Fill.Color = StyleHelper.GetInvertedColor(obj.Fill.Color);

            this._lastSelectedObj = obj;
        }

        private void SelectObject(PolyObj obj)
        {
            if (obj != this._lastSelectedObj)
            {
                if (null != this._lastSelectedObj)
                {
                    this.UnhighlightObject(this._lastSelectedObj);
                }

                this.HighlightObject(obj);
            }
        }

        private void UnhighlightObject(BoxObj obj)
        {
            if (null == obj)
            {
                return;
            }

            obj.Border.Width /= 3;
            obj.Border.Color = StyleHelper.GetInvertedColor(obj.Border.Color);
            obj.Fill.Color = StyleHelper.GetInvertedColor(obj.Fill.Color);

            this._lastSelectedObj = null;
        }

        #endregion
    }
}