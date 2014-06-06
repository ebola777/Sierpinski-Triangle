namespace SierpinskiTriangle.Models.Control
{
    using System.ComponentModel;
    using System.Drawing;

    using SierpinskiTriangle.Utilities;

    using Name = SierpinskiTriangle.Utilities.LocalizedDisplayNameAttribute;
    using Desc = SierpinskiTriangle.Utilities.LocalizedDescriptionAttribute;

    [LocalizedCategory(typeof(Style), "Style")]
    public class Style
    {
        #region Constructors and Destructors

        public Style()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);

            this.Visible = new Visible();
            this.Hidden = new Hidden();
        }

        #endregion

        #region Public Properties

        [DefaultValue(typeof(Color), "WhiteSmoke")]
        [Name(typeof(Style), "BackColor")]
        [Desc(typeof(Style), "BackColor")]
        public Color BackColor { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Name(typeof(Style), "Hidden")]
        [Desc(typeof(Style), "Hidden")]
        public Hidden Hidden { get; set; }

        [DefaultValue(false)]
        [Name(typeof(Style), "IsInverted")]
        [Desc(typeof(Style), "IsInverted")]
        public bool IsInverted { get; set; }

        [DefaultValue(true)]
        [Name(typeof(Style), "IsResetScale")]
        [Desc(typeof(Style), "IsResetScale")]
        public bool IsResetScale { get; set; }

        [DefaultValue(true)]
        [Name(typeof(Style), "IsShowZeroLines")]
        [Desc(typeof(Style), "IsShowZeroLines")]
        public bool IsShowZeroLines { get; set; }

        [DefaultValue(false)]
        [Name(typeof(Style), "IsUseAntiAlias")]
        [Desc(typeof(Style), "IsUseAntiAlias")]
        public bool IsUseAntiAlias { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Name(typeof(Style), "Visible")]
        [Desc(typeof(Style), "Visible")]
        public Visible Visible { get; set; }

        #endregion
    }

    public class Visible
    {
        #region Constructors and Destructors

        public Visible()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);
        }

        #endregion

        #region Public Properties

        [DefaultValue(typeof(Color), "CadetBlue")]
        [Name(typeof(Style), "Shape_BorderColor")]
        [Desc(typeof(Style), "Shape_BorderColor")]
        public Color BorderColor { get; set; }

        [DefaultValue(1)]
        [Name(typeof(Style), "Shape_BorderWidth")]
        [Desc(typeof(Style), "Shape_BorderWidth")]
        public int BorderWidth { get; set; }

        [DefaultValue(typeof(Color), "DarkTurquoise")]
        [Name(typeof(Style), "Shape_Fill")]
        [Desc(typeof(Style), "Shape_Fill")]
        public Color Fill { get; set; }

        [DefaultValue(typeof(Font), "Arial, 12")]
        [Name(typeof(Style), "Shape_Font")]
        [Desc(typeof(Style), "Shape_Font")]
        public Font Font { get; set; }

        [DefaultValue(typeof(Color), "Black")]
        [Name(typeof(Style), "Shape_FontColor")]
        [Desc(typeof(Style), "Shape_FontColor")]
        public Color FontColor { get; set; }

        [DefaultValue(true)]
        [Name(typeof(Style), "Shape_IsShow")]
        [Desc(typeof(Style), "Shape_IsShow")]
        public bool IsShow { get; set; }

        [DefaultValue(false)]
        [Name(typeof(Style), "Shape_IsShowNumber")]
        [Desc(typeof(Style), "Shape_IsShowNumber")]
        public bool IsShowNumber { get; set; }

        #endregion
    }

    public class Hidden
    {
        #region Constructors and Destructors

        public Hidden()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);
        }

        #endregion

        #region Public Properties

        [DefaultValue(typeof(Color), "Linen")]
        [Name(typeof(Style), "Shape_BorderColor")]
        [Desc(typeof(Style), "Shape_BorderColor")]
        public Color BorderColor { get; set; }

        [DefaultValue(1)]
        [Name(typeof(Style), "Shape_BorderWidth")]
        [Desc(typeof(Style), "Shape_BorderWidth")]
        public int BorderWidth { get; set; }

        [DefaultValue(typeof(Color), "Gainsboro")]
        [Name(typeof(Style), "Shape_Fill")]
        [Desc(typeof(Style), "Shape_Fill")]
        public Color Fill { get; set; }

        [DefaultValue(typeof(Font), "Arial, 12")]
        [Name(typeof(Style), "Shape_Font")]
        [Desc(typeof(Style), "Shape_Font")]
        public Font Font { get; set; }

        [DefaultValue(typeof(Color), "Black")]
        [Name(typeof(Style), "Shape_FontColor")]
        [Desc(typeof(Style), "Shape_FontColor")]
        public Color FontColor { get; set; }

        [DefaultValue(false)]
        [Name(typeof(Style), "Shape_IsShow")]
        [Desc(typeof(Style), "Shape_IsShow")]
        public bool IsShow { get; set; }

        [DefaultValue(false)]
        [Name(typeof(Style), "Shape_IsShowNumber")]
        [Desc(typeof(Style), "Shape_IsShowNumber")]
        public bool IsShowNumber { get; set; }

        #endregion
    }
}