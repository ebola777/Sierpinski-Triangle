namespace SierpinskiTriangle.Presenters.Control
{
    using System;

    using SierpinskiTriangle.Presenters.Base;
    using SierpinskiTriangle.Storage.Settings;
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Observers;

    public class ControlPresenter : PresenterBase<IControlView, ControlViewObserver>
    {
        #region Constructors and Destructors

        public ControlPresenter(IControlView view, ControlViewObserver observer)
            : base(view, observer)
        {
            this.View.Load += this.View_Load;
            this.View.UpdateClick += this.View_UpdateClick;
            this.View.ViewDockStateChanged += this.View_DockStateChanged;
        }

        #endregion

        #region Public Properties

        public ControlViewSettings ControlViewSettings { private get; set; }

        public GraphViewObserver GraphViewObserver { private get; set; }

        public MainViewObserver MainViewObserver { private get; set; }

        #endregion

        #region Methods

        private void View_DockStateChanged(object sender, EventArgs e)
        {
            this.MainViewObserver.UpdateFormOpenStateHelper(this.View);
        }

        private void View_Load(object sender, EventArgs e)
        {
            this.View.Model = this.ControlViewSettings.ControlModel;

            this.View.UpdatePropertyGridSource();
            this.View.ExpandPropertyGrid();
        }

        private void View_UpdateClick(object sender, EventArgs e)
        {
            this.GraphViewObserver.UpdateGraphHandler(this.View.Model);
        }

        #endregion
    }
}