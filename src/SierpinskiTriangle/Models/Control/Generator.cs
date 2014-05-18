namespace SierpinskiTriangle.Models.Control
{
    using System.ComponentModel;

    using SierpinskiTriangle.Utilities;

    using Name = SierpinskiTriangle.Utilities.LocalizedDisplayNameAttribute;
    using Desc = SierpinskiTriangle.Utilities.LocalizedDescriptionAttribute;

    [LocalizedCategory(typeof(Generator), "Generator")]
    public class Generator
    {
        #region Constructors and Destructors

        public Generator()
        {
            DynamicPropertiesHelper.SetAllPropertiesToDefault(this);
        }

        #endregion

        #region Public Properties

        [DefaultValue(true)]
        [Name(typeof(Generator), "DefaultVisibility")]
        [Desc(typeof(Generator), "DefaultVisibility")]
        public bool DefaultVisibility { get; set; }

        [DefaultValue(@"0 <> n() % 2")]
        [Name(typeof(Generator), "Expression")]
        [Desc(typeof(Generator), "Expression")]
        public string Expression { get; set; }

        [DefaultValue(true)]
        [Name(typeof(Generator), "IsUseCache")]
        [Desc(typeof(Generator), "IsUseCache")]
        public bool IsUseCache { get; set; }

        [DefaultValue(Mode.Modulo)]
        [Name(typeof(Generator), "Mode")]
        [Desc(typeof(Generator), "Mode")]
        public Mode Mode { get; set; }

        [DefaultValue(2)]
        [Name(typeof(Generator), "ModuloBy")]
        [Desc(typeof(Generator), "ModuloBy")]
        public int ModuloBy { get; set; }

        [DefaultValue(10)]
        [Name(typeof(Generator), "NumLevels")]
        [Desc(typeof(Generator), "NumLevels")]
        public int NumLevels { get; set; }

        [DefaultValue(0)]
        [Name(typeof(Generator), "RemainderToHide")]
        [Desc(typeof(Generator), "RemainderToHide")]
        public int RemainderToHide { get; set; }

        [DefaultValue(-1)]
        [Name(typeof(Generator), "RemainderToShow")]
        [Desc(typeof(Generator), "RemainderToShow")]
        public int RemainderToShow { get; set; }

        #endregion
    }

    public enum Mode
    {
        Bypass,

        Modulo,

        Expression
    }
}