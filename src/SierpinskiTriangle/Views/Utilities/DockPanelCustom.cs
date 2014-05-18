namespace SierpinskiTriangle.Views.Utilities
{
    using System.Windows.Forms;

    using WeifenLuo.WinFormsUI.Docking;

    /// <summary>
    ///     Custom control on DockPanel Suite
    /// </summary>
    public static class DockPanelCustom
    {
        #region Public Methods and Operators

        public static void Init(DockContent dck)
        {
            dck.HideOnClose = true;
        }

        public static bool IsFormOpen(DockContent dck)
        {
            return (dck.DockState != DockState.Hidden);
        }

        public static void SetFormBorderStyle(DockContent dck, FormBorderStyle style)
        {
            if (null != dck.Pane && null != dck.Pane.FloatWindow)
            {
                if (dck.Pane.FloatWindow.IsFloat)
                {
                    dck.Pane.FloatWindow.FormBorderStyle = style;
                }
            }
        }

        public static void SetFormVisible(DockContent dck, bool vis)
        {
            dck.IsHidden = !vis;
        }

        #endregion
    }
}