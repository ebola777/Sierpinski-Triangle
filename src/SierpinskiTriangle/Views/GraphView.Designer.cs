namespace SierpinskiTriangle.Views
{
    partial class GraphView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphView));
            this.zedGraphMain = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zedGraphMain
            // 
            resources.ApplyResources(this.zedGraphMain, "zedGraphMain");
            this.zedGraphMain.Name = "zedGraphMain";
            this.zedGraphMain.ScrollGrace = 0D;
            this.zedGraphMain.ScrollMaxX = 0D;
            this.zedGraphMain.ScrollMaxY = 0D;
            this.zedGraphMain.ScrollMaxY2 = 0D;
            this.zedGraphMain.ScrollMinX = 0D;
            this.zedGraphMain.ScrollMinY = 0D;
            this.zedGraphMain.ScrollMinY2 = 0D;
            this.zedGraphMain.ZoomEvent += new ZedGraph.ZedGraphControl.ZoomEventHandler(this.zedGraphMain_ZoomEvent);
            this.zedGraphMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphMain_MouseClick);
            this.zedGraphMain.MouseEnter += new System.EventHandler(this.zedGraphMain_MouseEnter);
            this.zedGraphMain.Resize += new System.EventHandler(this.zedGraphMain_Resize);
            // 
            // GraphView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zedGraphMain);
            this.Name = "GraphView";
            this.DockStateChanged += new System.EventHandler(this.GraphView_DockStateChanged);
            this.Load += new System.EventHandler(this.GraphView_Load);
            this.ResumeLayout(false);

        }

        private ZedGraph.ZedGraphControl zedGraphMain;

        #endregion
    }
}