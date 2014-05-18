namespace SierpinskiTriangle.Views
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    using SierpinskiTriangle.Utilities;

    public partial class AboutView : Form
    {
        #region Constants

        private const string FILE_ABOUT = @"data/about.html";

        #endregion

        #region Fields

        private bool _firstNavigation = true;

        private Uri _uri;

        #endregion

        #region Constructors and Destructors

        public AboutView()
        {
            this.InitializeComponent();

            this.Size = new Size(800, 600);
        }

        #endregion

        #region Methods

        private void AboutView_Load(object sender, EventArgs e)
        {
            string path = "file:///" + Path.Combine(FileSystemHelper.GetAssemblyDirectory(), FILE_ABOUT);

            this._uri = new Uri(path);
            this.webMain.Url = this._uri;
        }

        private void webMain_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (this._firstNavigation)
            {
                this._firstNavigation = false;
                return;
            }

            e.Cancel = true;

            Process.Start(e.Url.ToString());
        }

        #endregion
    }
}