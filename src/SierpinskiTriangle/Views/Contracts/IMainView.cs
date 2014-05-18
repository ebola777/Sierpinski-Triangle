namespace SierpinskiTriangle.Views.Contracts
{
    using System;
    using System.Windows.Forms;

    using SierpinskiTriangle.Models.Main;
    using SierpinskiTriangle.Views.Delegates.Main;

    using WeifenLuo.WinFormsUI.Docking;

    public interface IMainView : IView<MainModel>
    {
        #region Public Events

        event EventHandler SetDefaultSettingsHandler;

        event ChangeLanguageDelegate SetLanguageHandler;

        event SetViewVisibleDelegate SetViewVisibleHandler;

        event EventHandler ViewResizeHandler;

        #endregion

        #region Public Properties

        DockPanel DockPanelMain { get; }

        FormWindowState ViewWindowState { get; }

        #endregion

        #region Public Methods and Operators

        void AddLanguage(string key, string displayName);

        void UpdateFormOpenState(IView view, bool en);

        #endregion
    }
}