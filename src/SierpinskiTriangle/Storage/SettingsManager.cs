namespace SierpinskiTriangle.Storage
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    using Newtonsoft.Json;

    using SierpinskiTriangle.Lang;
    using SierpinskiTriangle.Storage.Settings;
    using SierpinskiTriangle.Utilities;
    using SierpinskiTriangle.Views.Utilities;

    public class SettingsManager
    {
        #region Constants

        private const string FILE_DEFAULT = @"settings/default.settings";

        private const string FILE_EXTENSION = ".json";

        private const string FILE_LANG = @"settings/lang.settings";

        private const string FILE_USER = @"settings/user.settings";

        #endregion

        #region Fields

        private readonly string _pathAsm = FileSystemHelper.GetAssemblyDirectory();

        private readonly string _pathDefault;

        private readonly string _pathLang;

        private readonly string _pathUser;

        #endregion

        #region Constructors and Destructors

        public SettingsManager()
        {
            this._pathLang = Path.Combine(this._pathAsm, (FILE_LANG + FILE_EXTENSION));
            this._pathDefault = Path.Combine(this._pathAsm, (FILE_DEFAULT + FILE_EXTENSION));
            this._pathUser = Path.Combine(this._pathAsm, (FILE_USER + FILE_EXTENSION));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Current settings
        /// </summary>
        public AppSettings App { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Read()
        {
            UiSettings uiSettings;

            // read language settings
            this.ReadLang(out uiSettings);

            // read default and user settings
            if (File.Exists(this._pathDefault))
            {
                try
                {
                    this.App = Json<AppSettings>.Read(this._pathDefault);
                }
                catch (Exception)
                {
                    this.App = new AppSettings();

                    // remove default file
                    File.Delete(this._pathDefault);
                }
            }
            else
            {
                this.App = new AppSettings();
            }

            // set UiSettings
            this.App.UiSettings = uiSettings;

            // check updated version
            if (this.App.Version.Version != AssemblyInfo.GetAssemblyVersion().ToString())
            {
                this.App = new AppSettings();

                // remove default file
                File.Delete(this._pathDefault);
            }

            // populate user settings to app settings
            if (File.Exists(this._pathUser))
            {
                try
                {
                    Json<AppSettings>.Populate(this._pathUser, this.App);
                }
                catch (Exception ex)
                {
                    ErrorHandling.ShowException(ex);

                    // ask user if to continue, if so, ignore old settings as a result
                    if (DialogResult.Cancel
                        == ErrorHandling.ShowError(
                            CoreLang.MessageBox_Text_Error_Populate_Settings,
                            CoreLang.MessageBox_Caption_Caution,
                            ErrorCode.ErrorPopulateUserSettings,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Exclamation))
                    {
                        ErrorHandling.Exit(ErrorCode.ErrorPopulateUserSettings);
                    }

                    this.App = new AppSettings();
                }
            }

            // load read-only settings
            this.SwitchReadOnlySettings(true);
        }

        public void Reset()
        {
            File.Delete(this._pathDefault);
            File.Delete(this._pathUser);
        }

        public void Save()
        {
            var serializer = new JsonSerializer
                                 {
                                     DefaultValueHandling = DefaultValueHandling.Ignore,
                                     NullValueHandling = NullValueHandling.Ignore,
                                     Formatting = Formatting.Indented
                                 };

            // unload read-only settings
            this.SwitchReadOnlySettings(false);

            // ensure path exists
            FileSystemHelper.EnsurePathExists(this._pathDefault);
            FileSystemHelper.EnsurePathExists(this._pathUser);

            try
            {
                // unload app-only settings
                this.UnloadAppOnlySettings();

                // write language settings
                Json<UiSettings>.Write(this._pathLang, this.App.UiSettings, serializer);

                // write user settings
                Json<AppSettings>.Write(this._pathUser, this.App, serializer);

                // write default settings to file if it doesn't exist
                if (!File.Exists(this._pathDefault))
                {
                    // customize serializer
                    serializer.DefaultValueHandling = DefaultValueHandling.Include;

                    // create new settings
                    var defaultSettings = new AppSettings();

                    Json<AppSettings>.Write(this._pathDefault, defaultSettings, serializer);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowException(ex);
                throw;
            }
        }

        /// <summary>
        ///     Switch read-only settings which will not be written to file, but accessible to program
        /// </summary>
        public void SwitchReadOnlySettings(bool en)
        {
            if (en)
            {
                this.App.FileInfo = new FileInfoSettings();
            }
            else
            {
                this.App.FileInfo = null;
            }
        }

        public void UnloadAppOnlySettings()
        {
            this.App.Version = null;
        }

        #endregion

        #region Methods

        private void ReadLang(out UiSettings uiSettings)
        {
            uiSettings = null;

            if (File.Exists(this._pathLang))
            {
                try
                {
                    uiSettings = Json<UiSettings>.Read(this._pathLang);

                    string key = uiSettings.Language;

                    if (string.Empty == key)
                    {
                        key = LocalizableView.GetSystemLanguageName();
                    }

                    // apply localization
                    var ci = new CultureInfo(key);
                    Thread.CurrentThread.CurrentUICulture = ci;
                }
                catch (Exception)
                {
                    uiSettings = new UiSettings();
                }
            }
        }

        #endregion
    }
}