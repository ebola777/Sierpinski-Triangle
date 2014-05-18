namespace SierpinskiTriangle.Presenters.Graph.Strategies
{
    using System.Collections.Generic;
    using System.Numerics;

    using SierpinskiTriangle.Presenters.Graph.Strategies.Base;

    public class BypassStrategy : StrategyBase<BigInteger>
    {
        #region Constructors and Destructors

        public BypassStrategy(IList<BigInteger> sequence)
            : base(sequence)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override void Calculate()
        {
            this.Result = new List<bool>();

            for (int i = 0; i != this.Sequence.Count; ++i)
            {
                this.Result.Add(true);
            }
        }

        #endregion
    }
}