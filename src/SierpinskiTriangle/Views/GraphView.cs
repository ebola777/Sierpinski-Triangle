namespace SierpinskiTriangle.Views
{
    using System;
    using System.Windows.Forms;

    using SierpinskiTriangle.Models.Graph;
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Utilities;

    using WeifenLuo.WinFormsUI.Docking;

    using ZedGraph;

    public partial class GraphView : DockContent, IGraphView
    {
        #region Constants

        private const string EMPTY_TITLE = "-";

        #endregion

        #region Constructors and Destructors

        public GraphView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Events

        public event MouseEventHandler GraphMouseClick;

        public event EventHandler ViewDockStateChanged;

        #endregion

        #region Public Properties

        public GraphModel Model { get; set; }

        public ZedGraphControl ZedGraph
        {
            get
            {
                return this.zedGraphMain;
            }
        }

        #endregion

        #region Properties

        private GraphPane GraphPane
        {
            get
            {
                return this.zedGraphMain.GraphPane;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void SetVisible(bool vis)
        {
            DockPanelCustom.SetFormVisible(this, vis);
        }

        #endregion

        #region Methods

        private void GraphView_DockStateChanged(object sender, EventArgs e)
        {
            this.ViewDockStateChanged(this, EventArgs.Empty);

            DockPanelCustom.SetFormBorderStyle(this, FormBorderStyle.Sizable);
        }

        private void GraphView_Load(object sender, EventArgs e)
        {
            DockPanelCustom.Init(this);
            this.InitControls();
        }

        private void InitControls()
        {
            this.zedGraphMain.Dock = DockStyle.Fill;

            this.GraphPane.XAxis.MajorGrid.IsZeroLine = true;
            this.GraphPane.XAxis.Title.IsVisible = false;

            this.GraphPane.YAxis.Scale.IsSkipCrossLabel = true;
            this.GraphPane.YAxis.Scale.IsReverse = true;
            this.GraphPane.YAxis.Title.IsVisible = false;

            this.GraphPane.Title.Text = EMPTY_TITLE;

            this.GraphPane.Legend.IsVisible = true;

            this.ResizeToEqualScale();
        }

        private void ResizeToEqualScale()
        {
            ZedGraphHelper.SetEqualScaleContain(this.GraphPane);
            this.GraphPane.AxisChange();
            this.zedGraphMain.Refresh();
        }

        private void zedGraphMain_MouseClick(object sender, MouseEventArgs e)
        {
            this.GraphMouseClick(sender, e);

            this.ResizeToEqualScale();
        }

        private void zedGraphMain_MouseEnter(object sender, EventArgs e)
        {
            this.zedGraphMain.Focus();
        }

        private void zedGraphMain_Resize(object sender, EventArgs e)
        {
            this.ResizeToEqualScale();
        }

        private void zedGraphMain_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            this.ResizeToEqualScale();
        }

        #endregion
    }
}