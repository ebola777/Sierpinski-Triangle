﻿namespace SierpinskiTriangle.Presenters.Graph.Drawing
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;

    using SierpinskiTriangle.Presenters.Graph.Drawing.Base;

    using ZedGraph;

    public class SquareDrawing : RegularPatternBase<BigInteger>
    {
        #region Constructors and Destructors

        public SquareDrawing(GraphPane pane, IList<BigInteger> sequence, IList<bool> visData)
            : base(pane, sequence, visData)
        {
        }

        #endregion

        #region Methods

        protected override void CalcShapeOffset(SizeF frameSize, SizeF shapeSizeRatio)
        {
            float ofsW = frameSize.Width * shapeSizeRatio.Width;
            float ofsH = frameSize.Height * shapeSizeRatio.Height;

            this.OffsetShape = new[] { new PointF(), new PointF(), new PointF(), new PointF() };

            this.OffsetShape[0].X = 0;
            this.OffsetShape[0].Y = 0;

            this.OffsetShape[1].X = ofsW;
            this.OffsetShape[1].Y = 0;

            this.OffsetShape[2].X = ofsW;
            this.OffsetShape[2].Y = ofsH;

            this.OffsetShape[3].X = 0;
            this.OffsetShape[3].Y = ofsH;
        }

        protected override PointD[] CreateShapePoints(PointF framePt)
        {
            return new[]
                       {
                           new PointD(framePt.X + this.OffsetShape[0].X, framePt.Y + this.OffsetShape[0].Y),
                           new PointD(framePt.X + this.OffsetShape[1].X, framePt.Y + this.OffsetShape[1].Y),
                           new PointD(framePt.X + this.OffsetShape[2].X, framePt.Y + this.OffsetShape[2].Y),
                           new PointD(framePt.X + this.OffsetShape[3].X, framePt.Y + this.OffsetShape[3].Y)
                       };
        }

        #endregion
    }
}