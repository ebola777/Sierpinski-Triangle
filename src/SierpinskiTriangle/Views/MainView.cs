namespace SierpinskiTriangle.Views
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using SierpinskiTriangle.Models.Main;
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Delegates.Main;

    using WeifenLuo.WinFormsUI.Docking;

    public partial class MainView : Form, IMainView
    {
        #region Constructors and Destructors

        public MainView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Events

        public event EventHandler SetDefaultSettingsHandler;

        public event ChangeLanguageDelegate SetLanguageHandler;

        public event SetViewVisibleDelegate SetViewVisibleHandler;

        public event EventHandler ViewResizeHandler;

        #endregion

        #region Public Properties

        public DockPanel DockPanelMain
        {
            get
            {
                return this.dckpnlMain;
            }
        }

        public MainModel Model { get; set; }

        public FormWindowState ViewWindowState
        {
            get
            {
                return this.WindowState;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void AddLanguage(string key, string displayName)
        {
            var item = new ToolStripMenuItem { Text = displayName, Tag = key };

            item.Click += this.mnuOptions_Language_SubItemClick;

            this.mnuOptions_Language.DropDownItems.Add(item);
        }

        public void UpdateFormOpenState(IView view, bool en)
        {
            if (view is ControlView)
            {
                this.mnuView_Control.Checked = en;
            }
            else if (view is GraphView)
            {
                this.mnuView_Graph.Checked = en;
            }
        }

        #endregion

        #region Methods

        private void InitControls()
        {
            // dckpnlMain
            this.dckpnlMain.Dock = DockStyle.Fill;
            this.dckpnlMain.DockBackColor = Color.DarkGray;
            this.dckpnlMain.DocumentStyle = DocumentStyle.DockingMdi;
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.InitControls();
        }

        private void MainView_Resize(object sender, EventArgs e)
        {
            if (null == this.ViewResizeHandler)
            {
                return;
            }

            if (this.WindowState != FormWindowState.Minimized)
            {
                this.ViewResizeHandler(this, EventArgs.Empty);
            }
        }

        private void mnuFile_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuHelp_About_Click(object sender, EventArgs e)
        {
            using (var aboutView = new AboutView())
            {
                aboutView.ShowDialog(this);
            }
        }

        private void mnuOptions_Language_SubItemClick(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;

            this.SetLanguageHandler(item.Tag.ToString());
        }

        private void mnuOptions_Language_SystemDefault_Click(object sender, EventArgs e)
        {
            this.SetLanguageHandler(string.Empty);
        }

        private void mnuOptions_ResetDefaultSettings_Click(object sender, EventArgs e)
        {
            this.SetDefaultSettingsHandler(this, EventArgs.Empty);
        }

        private void mnuView_Control_Click(object sender, EventArgs e)
        {
            this.SetViewVisibleHandler(typeof(IControlView), this.mnuView_Control.Checked);
        }

        private void mnuView_Graph_Click(object sender, EventArgs e)
        {
            this.SetViewVisibleHandler(typeof(IGraphView), this.mnuView_Graph.Checked);
        }

        #endregion
    }
}