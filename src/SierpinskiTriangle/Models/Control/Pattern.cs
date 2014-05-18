namespace SierpinskiTriangle.Models.Control
{
    using System.ComponentModel;
    using System.Drawing;

    using SierpinskiTriangle.Utilities;

    using Name = SierpinskiTriangle.Utilities.LocalizedDisplayNameAttribute;
    using Desc = SierpinskiTriangle.Utilities.LocalizedDescriptionAttribute;

    [LocalizedCategory(typeof(Pattern), "Pattern")]
    public class Pattern
    {
        #region Constructors and Destructors

        public Pattern()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);
        }

        #endregion

        #region Public Properties

        [DefaultValue(typeof(SizeF), "1.0, 1.0")]
        [Name(typeof(Pattern), "FrameDistanceRatio")]
        [Desc(typeof(Pattern), "FrameDistanceRatio")]
        public SizeF FrameDistanceRatio { get; set; }

        [DefaultValue(typeof(SizeF), "1.0, 1.0")]
        [Name(typeof(Pattern), "FrameSize")]
        [Desc(typeof(Pattern), "FrameSize")]
        public SizeF FrameSize { get; set; }

        [DefaultValue(PatternMode.Regular)]
        [Name(typeof(Pattern), "PatternMode")]
        [Desc(typeof(Pattern), "PatternMode")]
        public PatternMode PatternMode { get; set; }

        [DefaultValue(typeof(SizeF), "1.0, 1.0")]
        [Name(typeof(Pattern), "PatternOffset")]
        [Desc(typeof(Pattern), "PatternOffset")]
        public SizeF PatternOffset { get; set; }

        [DefaultValue(1f)]
        [Name(typeof(Pattern), "PatternScale")]
        [Desc(typeof(Pattern), "PatternScale")]
        public float PatternScale { get; set; }

        [DefaultValue(typeof(SizeF), "0.95, 0.95")]
        [Name(typeof(Pattern), "ShapeSizeRatio")]
        [Desc(typeof(Pattern), "ShapeSizeRatio")]
        public SizeF ShapeSizeRatio { get; set; }

        [DefaultValue(typeof(SizeF), "0.0, 0.0")]
        [Name(typeof(Pattern), "StartPosition")]
        [Desc(typeof(Pattern), "StartPosition")]
        public SizeF StartPosition { get; set; }

        #endregion
    }

    public enum PatternMode
    {
        Regular,

        RightAngledToLeft,

        RightAngledToRight,

        Square,
    }
}