namespace SierpinskiTriangle.Presenters.Graph.Drawing.Base
{
    using System.Collections.Generic;
    using System.Drawing;

    using Accord.MachineLearning.Structures;

    using SierpinskiTriangle.Models.Control;
    using SierpinskiTriangle.Presenters.Graph.Drawing.Contracts;

    using ZedGraph;

    public abstract class DrawingBase<TVal, TObj> : IDrawing<TVal, TObj>
        where TObj : GraphObj
    {
        #region Constructors and Destructors

        protected DrawingBase(GraphPane pane, IList<TVal> sequence, IList<bool> visData)
        {
            this.Pane = pane;
            this.Sequence = sequence;
            this.VisData = visData;
        }

        #endregion

        #region Public Properties

        public IDictionary<double[], MetaInfo<TVal, TObj>> Lookup { get; set; }

        public GraphPane Pane { get; set; }

        public KDTree<double> Points { get; set; }

        public PointF[] ScreenRect { get; set; }

        public IList<TVal> Sequence { get; set; }

        public IList<bool> VisData { get; set; }

        #endregion

        #region Properties

        protected PointF OffsetFrameCenter { get; set; }

        protected PointF OffsetNewLine { get; set; }

        protected PointF OffsetNextFrame { get; set; }

        protected PointF[] OffsetShape { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Draw(Pattern patternSettings, Style styleSettings)
        {
            int ind = 0;
            int line = 0;
            var basePt = new PointF(patternSettings.StartPosition.Width, patternSettings.StartPosition.Height);
            var lastPt = new PointF(basePt.X, basePt.Y);
            GraphObjList list = this.Pane.GraphObjList;

            // init data
            this.InitData();

            // offset starting frame pos
            basePt.X -= patternSettings.FrameSize.Width / 2;
            lastPt.X = basePt.X;

            // calc offsets
            this.CalcFrameOffset(patternSettings.FrameSize, patternSettings.FrameDistanceRatio);
            this.CalcShapeOffset(patternSettings.FrameSize, patternSettings.ShapeSizeRatio);

            // screen rectangle
            this.ScreenRect = new[] { basePt, new PointF() };

            while (ind < this.Sequence.Count)
            {
                int col = 0;

                // store to screen rectangle
                this.SaveScreenRectBeginLoop(lastPt);

                // even line
                for (; col <= line / 2; ++col)
                {
                    lastPt = this.CreateOne(
                        this.Sequence[ind + col],
                        this.VisData[ind + col],
                        list,
                        lastPt,
                        styleSettings);
                }

                col -= 2;
                for (; col >= 0; --col)
                {
                    lastPt = this.CreateOne(
                        this.Sequence[ind + col],
                        this.VisData[ind + col],
                        list,
                        lastPt,
                        styleSettings);
                }

                // entering next line
                basePt = this.NewLineFramePos(basePt);
                lastPt.X = basePt.X;
                lastPt.Y = basePt.Y;

                ind += (line / 2) + 1;
                ++line;

                // odd line
                col = 0;
                for (; col <= line / 2; ++col)
                {
                    lastPt = this.CreateOne(
                        this.Sequence[ind + col],
                        this.VisData[ind + col],
                        list,
                        lastPt,
                        styleSettings);
                }

                --col;
                for (; col >= 0; --col)
                {
                    lastPt = this.CreateOne(
                        this.Sequence[ind + col],
                        this.VisData[ind + col],
                        list,
                        lastPt,
                        styleSettings);
                }

                // store to screen rectangle
                this.SaveScreenRectEndLoop(lastPt);

                // entering next line
                basePt = this.NewLineFramePos(basePt);
                lastPt.X = basePt.X;
                lastPt.Y = basePt.Y;

                ind += (line / 2) + 1;
                ++line;
            }

            // adjust screen rectangle
            this.SaveScreenRectFinal();
        }

        #endregion

        #region Methods

        protected abstract void CalcFrameOffset(SizeF frameSize, SizeF distanceRatio);

        protected abstract void CalcShapeOffset(SizeF frameSize, SizeF shapeSizeRatio);

        protected abstract TObj CreateObj(PointF framePt);

        protected PointF CreateOne(TVal num, bool vis, ICollection<GraphObj> list, PointF lastPt, Style styleSettings)
        {
            TObj obj = this.CreateObj(lastPt);
            TextObj text = null;
            var centerPt = new double[] { lastPt.X + this.OffsetFrameCenter.X, lastPt.Y + this.OffsetFrameCenter.Y };
            bool toAdd = true;

            // set pattern
            if (styleSettings.IsInversed)
            {
                vis = !vis;
            }

            if (vis)
            {
                if (styleSettings.Visible.IsShow)
                {
                    // text
                    if (styleSettings.Visible.IsShowNumber)
                    {
                        text = this.CreateText(lastPt, num, styleSettings.Visible.Font, styleSettings.Visible.FontColor);
                    }

                    this.SetVisibleObjStyle(obj, styleSettings);
                }
                else
                {
                    toAdd = false;
                }
            }
            else
            {
                if (styleSettings.Hidden.IsShow)
                {
                    // text
                    if (styleSettings.Hidden.IsShowNumber)
                    {
                        text = this.CreateText(lastPt, num, styleSettings.Hidden.Font, styleSettings.Hidden.FontColor);
                    }

                    this.SetHiddenObjStyle(obj, styleSettings);
                }
                else
                {
                    toAdd = false;
                }
            }

            // whether to add the object
            if (toAdd)
            {
                // insert data
                this.InsertPoint(centerPt);

                // insert meta
                this.InsertMeta(centerPt, new MetaInfo<TVal, TObj>(num, vis, obj));

                // add to list
                list.Add(obj);

                if (null != text)
                {
                    list.Add(text);
                }
            }

            // next point
            lastPt = this.NextFramePos(lastPt);

            return lastPt;
        }

        protected abstract PointD[] CreateShapePoints(PointF framePt);

        protected TextObj CreateText(PointF framePt, TVal num, Font font, Color fontColor)
        {
            var fontSpec = new FontSpec(
                font.FontFamily.Name,
                font.Size,
                fontColor,
                font.Bold,
                font.Italic,
                font.Underline) { Border = { IsVisible = false }, Fill = { IsVisible = false } };

            return new TextObj(
                num.ToString(),
                framePt.X + this.OffsetFrameCenter.X,
                framePt.Y + this.OffsetFrameCenter.Y)
                       {
                           FontSpec = fontSpec,
                           ZOrder = ZOrder.A_InFront,
                           IsClippedToChartRect = true
                       };
        }

        protected void InitData()
        {
            GraphObjList list = this.Pane.GraphObjList;

            // remove previous objects
            list.Clear();

            // create a new kd-tree
            this.Points = new KDTree<double>(2);

            // create a new lookup info table
            this.Lookup = new Dictionary<double[], MetaInfo<TVal, TObj>>();
        }

        protected void InsertMeta(double[] centerPt, MetaInfo<TVal, TObj> meta)
        {
            this.Lookup[centerPt] = meta;
        }

        protected void InsertPoint(double[] centerPt)
        {
            this.Points.Add(centerPt, 0.0);
        }

        protected PointF NewLineFramePos(PointF basePt)
        {
            basePt.X += this.OffsetNewLine.X;
            basePt.Y += this.OffsetNewLine.Y;

            return basePt;
        }

        protected PointF NextFramePos(PointF lastPt)
        {
            lastPt.X += this.OffsetNextFrame.X;
            lastPt.Y += this.OffsetNextFrame.Y;

            return lastPt;
        }

        protected abstract void SaveScreenRectBeginLoop(PointF lastPt);

        protected abstract void SaveScreenRectEndLoop(PointF lastPt);

        protected abstract void SaveScreenRectFinal();

        protected abstract void SetHiddenObjStyle(TObj obj, Style styleSettings);

        protected abstract void SetVisibleObjStyle(TObj obj, Style styleSettings);

        #endregion
    }
}