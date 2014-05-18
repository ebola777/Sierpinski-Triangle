namespace SierpinskiTriangle.Presenters.Graph.Drawing.Base
{
    using System.Collections.Generic;
    using System.Drawing;

    using ZedGraph;

    public abstract class RegularPatternBase<TVal, TObj> : DrawingBase<TVal, TObj>
        where TObj : GraphObj
    {
        #region Constructors and Destructors

        protected RegularPatternBase(GraphPane pane, IList<TVal> sequence, IList<bool> visData)
            : base(pane, sequence, visData)
        {
        }

        #endregion

        #region Methods

        protected override void CalcFrameOffset(SizeF frameSize, SizeF distanceRatio)
        {
            this.OffsetFrameCenter = new PointF(frameSize.Width / 2, frameSize.Height / 2);
            this.OffsetNextFrame = new PointF(frameSize.Width * distanceRatio.Width, 0f);
            this.OffsetNewLine = new PointF(
                (-1) * frameSize.Width / 2 * distanceRatio.Width,
                frameSize.Height * distanceRatio.Height);
        }

        protected override void SaveScreenRectBeginLoop(PointF lastPt)
        {
            // do nothing
        }

        protected override void SaveScreenRectEndLoop(PointF lastPt)
        {
            this.ScreenRect[1].X = lastPt.X;
            this.ScreenRect[1].Y = lastPt.Y;
        }

        protected override void SaveScreenRectFinal()
        {
            // fit to perfect size
            this.ScreenRect[0].X -= (this.ScreenRect[1].X - this.OffsetNextFrame.X - this.ScreenRect[0].X);

            this.ScreenRect[1].Y += this.OffsetNewLine.Y;
        }

        #endregion
    }
}