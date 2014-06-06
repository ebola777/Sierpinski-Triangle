namespace SierpinskiTriangle.Presenters.Graph.Drawing.Base
{
    using System.Collections.Generic;
    using System.Drawing;

    using SierpinskiTriangle.Models.Control;

    using ZedGraph;

    public abstract class PolyObjBase<TVal> : DrawingBase<TVal, PolyObj>
    {
        #region Constructors and Destructors

        protected PolyObjBase(GraphPane pane, IList<TVal> sequence, IList<bool> visData)
            : base(pane, sequence, visData)
        {
        }

        #endregion

        #region Methods

        protected override PolyObj CreateObj(PointF framePt)
        {
            return new PolyObj(this.CreateShapePoints(framePt))
                       {
                           ZOrder = ZOrder.C_BehindChartBorder,
                           IsClippedToChartRect = true
                       };
        }

        protected override void SetHiddenObjStyle(PolyObj obj, Style styleSettings)
        {
            Hidden settings = styleSettings.Hidden;

            obj.IsVisible = settings.IsShow;

            if (settings.IsShow)
            {
                obj.Border.Color = settings.BorderColor;
                obj.Border.Width = settings.BorderWidth;
                obj.Fill.Color = settings.Fill;
            }
        }

        protected override void SetVisibleObjStyle(PolyObj obj, Style styleSettings)
        {
            Visible settings = styleSettings.Visible;

            obj.IsVisible = settings.IsShow;

            if (settings.IsShow)
            {
                obj.Border.Color = settings.BorderColor;
                obj.Border.Width = settings.BorderWidth;
                obj.Fill.Color = settings.Fill;
            }
        }

        #endregion
    }
}