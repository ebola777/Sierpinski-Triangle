namespace SierpinskiTriangle.Presenters.Graph.Drawing
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;

    using SierpinskiTriangle.Presenters.Graph.Drawing.Base;

    using ZedGraph;

    public class RightAngledToLeftDrawing : PolyObjBase<BigInteger>
    {
        #region Constructors and Destructors

        public RightAngledToLeftDrawing(GraphPane pane, IList<BigInteger> sequence, IList<bool> visData)
            : base(pane, sequence, visData)
        {
        }

        #endregion

        #region Methods

        protected override void CalcFrameOffset(SizeF frameSize, SizeF distanceRatio)
        {
            this.OffsetFrameCenter = new PointF(frameSize.Width / 2, frameSize.Height / 2);
            this.OffsetNextFrame = new PointF((-1) * frameSize.Width * distanceRatio.Width, 0f);
            this.OffsetNewLine = new PointF(0, frameSize.Height * distanceRatio.Height);
        }

        protected override void CalcShapeOffset(SizeF frameSize, SizeF shapeSizeRatio)
        {
            float ofsW = frameSize.Width * shapeSizeRatio.Width;
            float ofsH = frameSize.Height * shapeSizeRatio.Height;

            this.OffsetShape = new[] { new PointF(), new PointF(), new PointF() };

            this.OffsetShape[0].X = ofsW;
            this.OffsetShape[0].Y = 0;

            this.OffsetShape[1].X = 0;
            this.OffsetShape[1].Y = ofsH;

            this.OffsetShape[2].X = ofsW;
            this.OffsetShape[2].Y = ofsH;
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

        protected override void SaveScreenRectFinal()
        {
            // fit
            this.ScreenRect[0].X -= this.OffsetNextFrame.X;

            this.ScreenRect[1].X -= this.OffsetNextFrame.X;
            this.ScreenRect[1].Y += this.OffsetNewLine.Y;
        }

        #endregion
    }
}