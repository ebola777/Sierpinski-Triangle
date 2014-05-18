namespace SierpinskiTriangle.Utilities
{
    using System;
    using System.ComponentModel;

    internal sealed class LocalizedCategoryAttribute : CategoryAttribute
    {
        #region Constants

        private const string PREFIX = "c_";

        #endregion

        #region Fields

        private readonly ComponentResourceManager _res;

        #endregion

        #region Constructors and Destructors

        public LocalizedCategoryAttribute(Type typ, string key)
            : base(key)
        {
            this._res = new ComponentResourceManager(typ);
        }

        #endregion

        #region Methods

        protected override string GetLocalizedString(string value)
        {
            return this._res.GetString(PREFIX + value);
        }

        #endregion
    }

    internal sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        #region Constants

        private const string PREFIX = "n_";

        #endregion

        #region Fields

        private readonly ComponentResourceManager _res;

        #endregion

        #region Constructors and Destructors

        public LocalizedDisplayNameAttribute(Type typ, string key)
            : base(key)
        {
            this._res = new ComponentResourceManager(typ);
        }

        #endregion

        #region Public Properties

        public override string DisplayName
        {
            get
            {
                return this._res.GetString(PREFIX + base.DisplayName);
            }
        }

        #endregion
    }

    internal sealed class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        #region Constants

        private const string PREFIX = "d_";

        #endregion

        #region Fields

        private readonly ComponentResourceManager _res;

        #endregion

        #region Constructors and Destructors

        public LocalizedDescriptionAttribute(Type typ, string key)
            : base(key)
        {
            this._res = new ComponentResourceManager(typ);
        }

        #endregion

        #region Public Properties

        public override string Description
        {
            get
            {
                return this._res.GetString(PREFIX + base.Description);
            }
        }

        #endregion
    }
}