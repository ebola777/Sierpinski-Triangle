namespace SierpinskiTriangle.Presenters.Graph.Drawing
{
    public class MetaInfo<TVal, TObj>
    {
        #region Constructors and Destructors

        public MetaInfo(TVal number, bool visible, TObj obj)
        {
            this.Number = number;
            this.Visible = visible;
            this.Obj = obj;
        }

        #endregion

        #region Public Properties

        public TVal Number { get; set; }

        public TObj Obj { get; set; }

        public bool Visible { get; set; }

        #endregion
    }
}