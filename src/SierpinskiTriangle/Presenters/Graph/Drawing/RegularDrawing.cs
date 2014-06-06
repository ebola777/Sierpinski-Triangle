namespace SierpinskiTriangle.Presenters.Graph.Drawing
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;

    using SierpinskiTriangle.Presenters.Graph.Drawing.Base;

    using ZedGraph;

    public class RegularDrawing : RegularPatternBase<BigInteger>
    {
        #region Constructors and Destructors

        public RegularDrawing(GraphPane pane, IList<BigInteger> sequence, IList<bool> visData)
            : base(pane, sequence, visData)
        {
        }

        #endregion

        #region Methods

        protected override void CalcShapeOffset(SizeF frameSize, SizeF shapeSizeRatio)
        {
            float ofsFrameW = frameSize.Width / 2;
            float ofsFrameH = frameSize.Height / 2;
            float ofsW = frameSize.Width / 2 * shapeSizeRatio.Width;
            float ofsH = frameSize.Height / 2 * shapeSizeRatio.Height;

            this.OffsetShape = new[] { new PointF(), new PointF(), new PointF() };

            this.OffsetShape[0].X = ofsFrameW + 0f;
            this.OffsetShape[0].Y = ofsFrameH + (-1) * ofsH;

            this.OffsetShape[1].X = ofsFrameW + (-1) * ofsW;
            this.OffsetShape[1].Y = ofsFrameH + ofsH;

            this.OffsetShape[2].X = ofsFrameW + ofsW;
            this.OffsetShape[2].Y = ofsFrameH + ofsH;
        }

        protected override PointD[] CreateShapePoints(PointF framePt)
        {
            return new[]
                       {
                           new PointD(framePt.X + this.OffsetShape[0].X, framePt.Y + this.OffsetShape[0].Y),
                           new PointD(framePt.X + this.OffsetShape[1].X, framePt.Y + this.OffsetShape[1].Y),
                           new PointD(framePt.X + this.OffsetShape[2].X, framePt.Y + this.OffsetShape[2].Y)
                       };
        }

        #endregion
    }
}