namespace SierpinskiTriangle.Presenters.Main
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using SierpinskiTriangle.Lang;
    using SierpinskiTriangle.Models.Main;
    using SierpinskiTriangle.Presenters.Base;
    using SierpinskiTriangle.Storage.Settings;
    using SierpinskiTriangle.Utilities;
    using SierpinskiTriangle.Views;
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Observers;
    using SierpinskiTriangle.Views.Utilities;

    using WeifenLuo.WinFormsUI.Docking;

    public class MainPresenter : PresenterBase<IMainView, MainViewObserver>
    {
        #region Fields

        private FormWindowState _lastWindowState;

        #endregion

        #region Constructors and Destructors

        public MainPresenter(IMainView view, MainViewObserver observer)
            : base(view, observer)
        {
            this.View.Model = new MainModel();

            this.View.Load += this.View_Load;
            this.View.Closed += this.View_Closed;
            this.View.ViewResizeHandler += this.View_Resize;
            this.View.SetViewVisibleHandler += this.Menu_SetViewVisible;
            this.View.SetLanguageHandler += this.Menu_ChangeLanguage;
            this.View.SetDefaultSettingsHandler += this.Menu_SetDefaultSettings;

            this.Observer.UpdateFormOpenStateHandler += this.View.UpdateFormOpenState;
        }

        #endregion

        #region Public Properties

        public FileInfoSettings FileInfoSettings { private get; set; }

        public MainViewSettings MainViewSettings { private get; set; }

        public UiSettings UiSettings { private get; set; }

        public ViewFactory ViewFactory { private get; set; }

        #endregion

        #region Methods

        private void AddLanguagesToMenu()
        {
            IList<string> lang = this.View.Model.SupportedLanguage;

            // each supported language
            foreach (string item in lang)
            {
                this.View.AddLanguage(item, CultureInfo.GetCultureInfo(item).NativeName);
            }
        }

        private void ApplyLanguageToForms()
        {
            // apply to forms
            foreach (Form frm in this.ViewFactory.Views.Select(item => (Form)item.Value))
            {
                LocalizableView.ChangeLanguage(frm);
            }
        }

        private IDockContent GetContent(string str)
        {
            if (str == typeof(ControlView).ToString())
            {
                return this.ViewFactory.Get<ControlView>();
            }

            if (str == typeof(GraphView).ToString())
            {
                return this.ViewFactory.Get<GraphView>();
            }

            return null;
        }

        private void Menu_ChangeLanguage(string key)
        {
            this.UiSettings.Language = key;

            if (MessageBox.Show(
                CoreLang.MessageBox_Text_Change_Lang_Restart,
                CoreLang.MessageBox_Caption_Info,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void Menu_SetDefaultSettings(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                CoreLang.MessageBox_Text_Set_Default_Settings,
                CoreLang.MessageBox_Caption_Info,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information) == DialogResult.OK)
            {
                Controller.ResetSettings();
                ErrorHandling.Exit(ErrorCode.Default);
            }
        }

        private void Menu_SetViewVisible(Type typView, bool vis)
        {
            if (typeof(IControlView) == typView)
            {
                this.ViewFactory.Get<ControlView>().SetVisible(vis);
            }
            if (typeof(IGraphView) == typView)
            {
                this.ViewFactory.Get<GraphView>().SetVisible(vis);
            }
        }

        private void ReadLayout(DockPanel dckpnl)
        {
            string pathLayout = this.FileInfoSettings.PathLayout;

            if (File.Exists(pathLayout))
            {
                try
                {
                    dckpnl.LoadFromXml(pathLayout, this.GetContent);
                }
                catch (Exception)
                {
                    this.ResetLayout();
                }
            }
            else
            {
                this.ResetLayout();
            }
        }

        private void ResetLayout()
        {
            (this.ViewFactory.Get<ControlView>()).Show(this.View.DockPanelMain);
            (this.ViewFactory.Get<GraphView>()).Show(this.View.DockPanelMain);
        }

        private void SaveLayout(DockPanel dckpnl)
        {
            FileSystemHelper.EnsurePathExists(this.FileInfoSettings.PathLayout);

            try
            {
                dckpnl.SaveAsXml(this.FileInfoSettings.PathLayout);
            }
            catch (Exception)
            {
                ErrorHandling.ShowError(
                    CoreLang.MessageBox_Text_Error_Save_Layout,
                    CoreLang.MessageBox_Caption_Error,
                    ErrorCode.ErrorSaveLayout,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SavePosSize(Form frm)
        {
            MainViewSettings mainViewSettings = this.MainViewSettings;

            if (frm.WindowState == FormWindowState.Normal)
            {
                mainViewSettings.Left = frm.Left;
                mainViewSettings.Top = frm.Top;
                mainViewSettings.Width = frm.Width;
                mainViewSettings.Height = frm.Height;
            }
            else
            {
                mainViewSettings.Left = frm.RestoreBounds.Left;
                mainViewSettings.Top = frm.RestoreBounds.Top;
                mainViewSettings.Width = frm.RestoreBounds.Width;
                mainViewSettings.Height = frm.RestoreBounds.Height;
            }
        }

        private void SaveWindowState(Form frm)
        {
            FormWindowState state = frm.WindowState;

            if (state == FormWindowState.Minimized)
            {
                state = this._lastWindowState;
            }

            this.MainViewSettings.WindowState = state;
        }

        private void SetLastWindowState(FormWindowState state)
        {
            this._lastWindowState = state;
        }

        private void SetPosSize(Control frm)
        {
            int? left = this.MainViewSettings.Left;
            int? top = this.MainViewSettings.Top;
            int? width = this.MainViewSettings.Width;
            int? height = this.MainViewSettings.Height;

            int nWidth = width ?? MainViewSettings.Defaults.Width;
            int nHeight = height ?? MainViewSettings.Defaults.Height;

            Point posCenter = ControlHelper.GetCenterScreenPos(frm, new Size(nWidth, nHeight));

            int nLeft = left ?? posCenter.X;
            int nTop = top ?? posCenter.Y;

            ControlHelper.FitToScreen(
                frm,
                new Rectangle(nLeft, nTop, nWidth, nHeight),
                new Rectangle(
                    MainViewSettings.Constraints.MinLeft,
                    MainViewSettings.Constraints.MinTop,
                    MainViewSettings.Constraints.MinWidth,
                    MainViewSettings.Constraints.MinHeight));
        }

        private void SetWindowState(Form frm)
        {
            frm.WindowState = this.MainViewSettings.WindowState;
        }

        private void View_Closed(object sender, EventArgs e)
        {
            var frm = (Form)this.View;

            this.SaveWindowState(frm);
            this.SavePosSize(frm);
            this.SaveLayout(this.View.DockPanelMain);
        }

        private void View_Load(object sender, EventArgs e)
        {
            var frm = (Form)this.View;

            // read layout
            this.ReadLayout(this.View.DockPanelMain);

            // localize
            this.ApplyLanguageToForms();

            // set window state, position and size
            this.SetWindowState(frm);
            this.SetPosSize(frm);

            // misc
            this.AddLanguagesToMenu();
        }

        private void View_Resize(object sender, EventArgs e)
        {
            this.SetLastWindowState(this.View.ViewWindowState);
        }

        #endregion
    }
}