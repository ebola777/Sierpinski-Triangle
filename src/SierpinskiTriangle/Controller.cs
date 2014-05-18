namespace SierpinskiTriangle
{
    using System;
    using System.Windows.Forms;

    using SierpinskiTriangle.Lang;
    using SierpinskiTriangle.Presenters.Control;
    using SierpinskiTriangle.Presenters.Graph;
    using SierpinskiTriangle.Presenters.Main;
    using SierpinskiTriangle.Storage;
    using SierpinskiTriangle.Utilities;
    using SierpinskiTriangle.Views;
    using SierpinskiTriangle.Views.Observers;

    public static class Controller
    {
        #region Static Fields

        private static readonly SettingsManager _settingsManager = new SettingsManager();

        private static readonly ViewFactory _viewFactory = new ViewFactory();

        #endregion

        #region Public Methods and Operators

        public static void Init()
        {
            var mainView = _viewFactory.Get<MainView>();
            var controlView = _viewFactory.Get<ControlView>();
            var graphView = _viewFactory.Get<GraphView>();
            var mainViewObserver = new MainViewObserver();
            var controlViewObserver = new ControlViewObserver();
            var graphViewObserver = new GraphViewObserver();

            // settings: read settings
            ReadSettings();
            AppSettings appSettings = _settingsManager.App;

            // presenter: create new presenters
            var mainPresenter = new MainPresenter(mainView, mainViewObserver);
            var controlPresenter = new ControlPresenter(controlView, controlViewObserver);
            var graphPresenter = new GraphPresenter(graphView, graphViewObserver);

            // presenter: link view factory
            mainPresenter.ViewFactory = _viewFactory;

            // presenter: link settings
            mainPresenter.UiSettings = appSettings.UiSettings;
            mainPresenter.MainViewSettings = appSettings.MainView;
            mainPresenter.FileInfoSettings = appSettings.FileInfo;

            controlPresenter.ControlViewSettings = appSettings.ControlView;

            graphPresenter.GraphViewSettings = appSettings.GraphView;
            graphPresenter.FileInfoSettings = appSettings.FileInfo;

            // presenter: link observers
            controlPresenter.MainViewObserver = mainViewObserver;
            controlPresenter.GraphViewObserver = graphViewObserver;

            graphPresenter.MainViewObserver = mainViewObserver;

            // bind application events
            Application.ApplicationExit += OnApplicationExit;

            // run main view
            Application.Run(mainView);
        }

        public static void ResetSettings()
        {
            _settingsManager.Reset();
        }

        #endregion

        #region Methods

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            // save settings
            SaveSettings();
        }

        private static void ReadSettings()
        {
            // read settings
            _settingsManager.Read();
        }

        private static void SaveSettings()
        {
            // save settings
            try
            {
                _settingsManager.Save();
            }
            catch (Exception)
            {
                ErrorHandling.ShowError(
                    CoreLang.MessageBox_Text_Error_Save_Settings,
                    CoreLang.MessageBox_Caption_Error,
                    ErrorCode.ErrorSaveSettings,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ErrorHandling.Exit(ErrorCode.ErrorSaveSettings);
            }
        }

        #endregion
    }
}