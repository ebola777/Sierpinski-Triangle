namespace SierpinskiTriangle.Models.Control
{
    using System.ComponentModel;

    public class ControlModel
    {
        #region Constructors and Destructors

        public ControlModel()
        {
            this.Generator = new Generator();
            this.Pattern = new Pattern();
            this.Style = new Style();
        }

        #endregion

        #region Public Properties

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Generator Generator { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pattern Pattern { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Style Style { get; set; }

        #endregion
    }
}