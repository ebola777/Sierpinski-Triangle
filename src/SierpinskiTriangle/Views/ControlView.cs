namespace SierpinskiTriangle.Views
{
    using System;
    using System.Windows.Forms;

    using SierpinskiTriangle.Models.Control;
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Utilities;

    using WeifenLuo.WinFormsUI.Docking;

    public partial class ControlView : DockContent, IControlView
    {
        #region Constructors and Destructors

        public ControlView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Events

        public event EventHandler UpdateClick;

        public event EventHandler ViewDockStateChanged;

        #endregion

        #region Public Properties

        public ControlModel Model { get; set; }

        #endregion

        #region Public Methods and Operators

        public void ExpandPropertyGrid()
        {
            GridItem root = this.propgrdMain.SelectedGridItem;

            while (null != root.Parent)
            {
                root = root.Parent;
            }

            this.ExpandPropertyGridDeep(root, 2);
        }

        public void SetVisible(bool vis)
        {
            DockPanelCustom.SetFormVisible(this, vis);
        }

        public void UpdatePropertyGridSource()
        {
            this.propgrdMain.SelectedObject = this.Model;
        }

        #endregion

        #region Methods

        private void ControlView_Load(object sender, EventArgs e)
        {
            DockPanelCustom.Init(this);
            this.InitView();
            this.InitControls();
        }

        private void ExpandPropertyGridDeep(GridItem root, int depth)
        {
            if (0 == depth)
            {
                return;
            }

            foreach (GridItem item in root.GridItems)
            {
                item.Expanded = true;

                this.ExpandPropertyGridDeep(item, depth - 1);
            }
        }

        private void FormControl_DockStateChanged(object sender, EventArgs e)
        {
            this.ViewDockStateChanged(this, EventArgs.Empty);

            DockPanelCustom.SetFormBorderStyle(this, FormBorderStyle.Sizable);
        }

        private void InitControls()
        {
            // tlpMain
            this.tlpMain.Dock = DockStyle.Fill;
            this.tlpMain.RowStyles[0].SizeType = SizeType.AutoSize;

            // propgrdMain
            this.propgrdMain.Dock = DockStyle.Fill;
        }

        private void InitView()
        {
            this.AutoScroll = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.UpdateClick(this, EventArgs.Empty);
        }

        #endregion
    }
}