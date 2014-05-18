namespace SierpinskiTriangle.Views.Utilities
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    public static class LocalizableView
    {
        #region Public Methods and Operators

        public static ComponentResourceManager ChangeLanguage(Control frm)
        {
            frm.SuspendLayout();

            var res = new ComponentResourceManager(frm.GetType());
            res.ApplyResources(frm, "$this");

            ApplyResources(res, frm);

            frm.ResumeLayout(false);

            return res;
        }

        public static string GetSystemLanguageName()
        {
            return CultureInfo.InstalledUICulture.Name;
        }

        #endregion

        #region Methods

        private static void ApplyResources(ComponentResourceManager res, Control parentCtrl)
        {
            foreach (Control ctrl in parentCtrl.Controls)
            {
                string text = res.GetString(ctrl.Name + ".Text");

                if (null != text)
                {
                    ctrl.Text = text;
                }

                // determine type of control
                if (ctrl is MenuStrip)
                {
                    ApplyResourcesMenuStrip(res, (MenuStrip)ctrl);
                }
                else
                {
                    ApplyResources(res, ctrl);
                }
            }
        }

        private static void ApplyResourcesMenuStrip(ComponentResourceManager res, ToolStrip menu)
        {
            foreach (ToolStripMenuItem item in menu.Items)
            {
                res.ApplyResources(item, item.Name);

                foreach (ToolStripMenuItem dropDownItem in item.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    ApplyResourcesToolStripMenuItem(res, dropDownItem);
                }
            }
        }

        private static void ApplyResourcesToolStripMenuItem(ComponentResourceManager res, ToolStripDropDownItem item)
        {
            res.ApplyResources(item, item.Name);

            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                ApplyResourcesToolStripMenuItem(res, dropDownItem);
            }
        }

        #endregion
    }
}