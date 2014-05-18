namespace SierpinskiTriangle.Presenters.Graph.Strategies.Base
{
    using System.Collections.Generic;

    using SierpinskiTriangle.Presenters.Graph.Strategies.Contracts;

    public abstract class StrategyBase<TVal> : IStrategy
    {
        #region Constructors and Destructors

        protected StrategyBase(IList<TVal> sequence)
        {
            this.Sequence = sequence;
        }

        #endregion

        #region Public Properties

        public IList<bool> Result { get; set; }

        public IList<TVal> Sequence { get; set; }

        #endregion

        #region Public Methods and Operators

        public abstract void Calculate();

        #endregion
    }
}