namespace SierpinskiTriangle.Models.Main
{
    using System.Collections.Generic;

    public class MainModel
    {
        #region Fields

        private readonly IList<string> _supportedLang = new List<string> { "en-US", "zh-TW" };

        #endregion

        #region Public Properties

        public IList<string> SupportedLanguage
        {
            get
            {
                return this._supportedLang;
            }
        }

        #endregion
    }
}