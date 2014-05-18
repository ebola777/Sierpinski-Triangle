namespace SierpinskiTriangle.Views.Delegates.Main
{
    using System;
    using System.Windows.Forms;

    using WeifenLuo.WinFormsUI.Docking;

    public delegate void ReadLayoutDelegate(DockPanel dckpnl);

    public delegate void ViewClosedDelegate(Form frm);

    public delegate void SetViewVisibleDelegate(Type view, bool vis);

    public delegate void ChangeLanguageDelegate(string key);
}