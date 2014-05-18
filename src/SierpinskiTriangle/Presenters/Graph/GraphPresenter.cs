namespace SierpinskiTriangle.Presenters.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;
    using System.Windows.Forms;

    using SierpinskiTriangle.Lang;
    using SierpinskiTriangle.Models.Control;
    using SierpinskiTriangle.Presenters.Base;
    using SierpinskiTriangle.Presenters.Graph.Cache;
    using SierpinskiTriangle.Presenters.Graph.Drawing;
    using SierpinskiTriangle.Presenters.Graph.Drawing.Contracts;
    using SierpinskiTriangle.Presenters.Graph.Generators;
    using SierpinskiTriangle.Presenters.Graph.Generators.Contracts;
    using SierpinskiTriangle.Presenters.Graph.Interactive;
    using SierpinskiTriangle.Presenters.Graph.Interactive.Contracts;
    using SierpinskiTriangle.Presenters.Graph.Strategies;
    using SierpinskiTriangle.Presenters.Graph.Strategies.Contracts;
    using SierpinskiTriangle.Storage.Settings;
    using SierpinskiTriangle.Utilities;
    using SierpinskiTriangle.Views.Contracts;
    using SierpinskiTriangle.Views.Observers;
    using SierpinskiTriangle.Views.Utilities;

    using ZedGraph;

    public class GraphPresenter : PresenterBase<IGraphView, GraphViewObserver>
    {
        #region Fields

        private CacheManager _cacheManager;

        private IGenerator<BigInteger> _generatorCached;

        private IInteractive _interactive;

        #endregion

        #region Constructors and Destructors

        public GraphPresenter(IGraphView view, GraphViewObserver observer)
            : base(view, observer)
        {
            this.View.Load += this.View_Load;
            this.View.ViewDockStateChanged += this.View_DockStateChanged;
            this.View.GraphMouseClick += this.Graph_MouseClick;

            this.Observer.UpdateGraphHandler += this.UpdateGraph;
        }

        #endregion

        #region Public Properties

        public FileInfoSettings FileInfoSettings { private get; set; }

        public GraphViewSettings GraphViewSettings { private get; set; }

        public MainViewObserver MainViewObserver { private get; set; }

        #endregion

        #region Methods

        private static void ConfigureGraph(
            Pattern patternSettings,
            Style styleSettings,
            ZedGraphControl zedGraph,
            GraphPane pane,
            IDrawing drawing)
        {
            zedGraph.IsAntiAlias = styleSettings.IsUseAntiAlias;

            pane.XAxis.MajorGrid.IsZeroLine = styleSettings.IsShowZeroLines;
            pane.YAxis.MajorGrid.IsZeroLine = styleSettings.IsShowZeroLines;

            pane.Chart.Fill = new Fill(styleSettings.BackColor);

            // if to reset scale
            if (styleSettings.IsResetScale)
            {
                // fit screen to data
                pane.XAxis.Scale.Min = Math.Min(drawing.ScreenRect[0].X, drawing.ScreenRect[1].X);
                pane.XAxis.Scale.Max = Math.Max(drawing.ScreenRect[0].X, drawing.ScreenRect[1].X);

                pane.YAxis.Scale.Min = Math.Min(drawing.ScreenRect[0].Y, drawing.ScreenRect[1].Y);
                pane.YAxis.Scale.Max = Math.Max(drawing.ScreenRect[0].Y, drawing.ScreenRect[1].Y);

                // set equal scale
                ZedGraphHelper.Offset(pane, patternSettings.PatternOffset);
                ZedGraphHelper.SetEqualScaleContain(pane);
                ZedGraphHelper.Scale(pane, new SizeF(patternSettings.PatternScale, patternSettings.PatternScale));
                zedGraph.AxisChange();
            }
        }

        private static IList<bool> Strategy(Generator genSettings, GeneratorResult<BigInteger> generatorResult)
        {
            IStrategy modStrategy;

            // create a new strategy
            switch (genSettings.Mode)
            {
                case Mode.Bypass:
                    {
                        modStrategy = new BypassStrategy(generatorResult.Sequence);
                    }
                    break;
                case Mode.Modulo:
                    {
                        modStrategy = new ModuloStrategy(
                            generatorResult.Sequence,
                            genSettings.ModuloBy,
                            genSettings.RemainderToHide,
                            genSettings.RemainderToShow,
                            genSettings.DefaultVisibility);
                    }
                    break;
                case Mode.Expression:
                    {
                        modStrategy = new ExpressionStrategy(generatorResult.Sequence, genSettings.Expression);
                    }
                    break;
                default:
                    {
                        return null;
                    }
            }

            // calculate
            try
            {
                modStrategy.Calculate();

                return modStrategy.Result;
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowException(ex);

                ErrorHandling.ShowError(
                    CoreLang.MessageBox_Text_Error_Calculate,
                    CoreLang.MessageBox_Caption_Error,
                    ErrorCode.ErrorCalculate,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }

        private void Draw(
            Pattern patternSettings,
            Style styleSettings,
            ZedGraphControl zedGraph,
            IList<BigInteger> sequence,
            IList<bool> result)
        {
            GraphPane pane = zedGraph.GraphPane;
            IDrawing<BigInteger, PolyObj> drawing;

            // create a new drawing
            switch (patternSettings.PatternMode)
            {
                case PatternMode.Regular:
                    {
                        drawing = new RegularDrawing(pane, sequence, result);
                    }
                    break;
                case PatternMode.Square:
                    {
                        drawing = new SquareDrawing(pane, sequence, result);
                    }
                    break;
                case PatternMode.RightAngledToLeft:
                    {
                        drawing = new RightAngledToLeftDrawing(pane, sequence, result);
                    }
                    break;
                case PatternMode.RightAngledToRight:
                    {
                        drawing = new RightAngledToRightDrawing(pane, sequence, result);
                    }
                    break;
                default:
                    {
                        return;
                    }
            }

            // draw
            drawing.Draw(patternSettings, styleSettings);

            // reset old interactive
            if (null != this._interactive)
            {
                this._interactive.Reset();
            }

            // create a new interactive and store metadata to it
            this._interactive = new RegularInteractive(this.View.ZedGraph, patternSettings.FrameSize)
                                    {
                                        Points =
                                            drawing
                                            .Points,
                                        Lookup =
                                            drawing
                                            .Lookup
                                    };

            // configure the graph
            ConfigureGraph(patternSettings, styleSettings, zedGraph, pane, drawing);

            // refresh the graph
            zedGraph.Refresh();
        }

        private GeneratorResult<BigInteger> Generate(Generator genSettings)
        {
            GeneratorResult<BigInteger> result;
            IGenerator<BigInteger> generator;

            // load generator
            if (genSettings.IsUseCache)
            {
                if (null == this._generatorCached)
                {
                    // load cache
                    this._cacheManager.Read();

                    CacheContent cacheContent = this._cacheManager.CacheContent;

                    this._generatorCached = new PascalTriangle(cacheContent.GeneratedHigh, cacheContent.Sequence);
                }

                generator = this._generatorCached;
            }
            else
            {
                if (null == this._generatorCached)
                {
                    generator = this._generatorCached = new PascalTriangle();
                }
                else
                {
                    generator = this._generatorCached;
                }
            }

            // generate
            try
            {
                result = generator.Generate(genSettings.NumLevels);
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowException(ex);

                ErrorHandling.ShowError(
                    CoreLang.MessageBox_Text_Error_Generate,
                    CoreLang.MessageBox_Caption_Error,
                    ErrorCode.ErrorGenerate,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }

            // store to cache
            if (genSettings.IsUseCache)
            {
                this.SaveCache(generator);
            }

            return result;
        }

        private void Graph_MouseClick(object sender, MouseEventArgs e)
        {
            if (null == this._interactive)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                double ptX;
                double ptY;

                this.View.ZedGraph.GraphPane.ReverseTransform(e.Location, out ptX, out ptY);
                this._interactive.ShowNearestObjectInfo(ptX, ptY);
            }
        }

        private void SaveCache(IGenerator<BigInteger> generator)
        {
            CacheContent cacheContent = this._cacheManager.CacheContent;

            if (generator.GeneratedHigh > cacheContent.GeneratedHigh)
            {
                cacheContent.GeneratedHigh = generator.GeneratedHigh;
                cacheContent.Sequence = generator.Sequence;

                try
                {
                    this._cacheManager.Save();
                }
                catch (Exception ex)
                {
                    ErrorHandling.ShowException(ex);

                    ErrorHandling.ShowError(
                        CoreLang.MessageBox_Text_Error_Save_Cache,
                        CoreLang.MessageBox_Caption_Error,
                        ErrorCode.ErrorSaveCache,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateGraph(ControlModel model)
        {
            Generator genSettings = model.Generator;
            Pattern patternSettings = model.Pattern;
            Style styleSettings = model.Style;

            // check settings
            if (genSettings.NumLevels <= 0)
            {
                ErrorHandling.ShowError(
                    CoreLang.MessageBox_Text_Warning_Invalid_Num_of_Levels,
                    CoreLang.MessageBox_Caption_Caution,
                    ErrorCode.InvalidArguments,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            // generate
            GeneratorResult<BigInteger> generatorResult = this.Generate(genSettings);
            if (null == generatorResult)
            {
                return;
            }

            // strategy
            IList<bool> strategyResult = Strategy(genSettings, generatorResult);
            if (null == strategyResult)
            {
                return;
            }

            // draw
            this.Draw(patternSettings, styleSettings, this.View.ZedGraph, generatorResult.Sequence, strategyResult);
        }

        private void View_DockStateChanged(object sender, EventArgs e)
        {
            this.MainViewObserver.UpdateFormOpenStateHelper(this.View);
        }

        private void View_Load(object sender, EventArgs e)
        {
            this._cacheManager = new CacheManager(
                this.FileInfoSettings.PathSequenceCache,
                PascalTriangle.GetGroupIndex(this.GraphViewSettings.CacheMaxNumItems),
                PascalTriangle.GetLowerByIndex(this.GraphViewSettings.CacheMaxNumItems));
        }

        #endregion
    }
}