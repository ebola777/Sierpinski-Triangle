namespace SierpinskiTriangle.Presenters.Graph.Strategies
{
    using System.Collections.Generic;
    using System.Numerics;

    using SierpinskiTriangle.Presenters.Graph.Strategies.Base;

    public class ModuloStrategy : StrategyBase<BigInteger>
    {
        #region Fields

        private readonly bool _defaultVis;

        private readonly BigInteger _modBy;

        private readonly BigInteger _remainderToHide;

        private readonly BigInteger _remainderToShow;

        #endregion

        #region Constructors and Destructors

        public ModuloStrategy(
            IList<BigInteger> sequence,
            BigInteger modBy,
            BigInteger remainderToHide,
            BigInteger remainderToShow,
            bool defaultVis)
            : base(sequence)
        {
            this._modBy = modBy;
            this._remainderToHide = remainderToHide;
            this._remainderToShow = remainderToShow;
            this._defaultVis = defaultVis;
        }

        #endregion

        #region Public Methods and Operators

        public override void Calculate()
        {
            var dp = new Dictionary<BigInteger, bool>();

            this.Result = new List<bool>();

            foreach (BigInteger num in this.Sequence)
            {
                bool dpVal;

                if (dp.TryGetValue(num, out dpVal))
                {
                    this.Result.Add(dpVal);
                }
                else
                {
                    BigInteger remainder = num % this._modBy;

                    if (-1 != this._remainderToHide)
                    {
                        if (remainder == this._remainderToHide)
                        {
                            this.Result.Add(false);
                            dp[num] = false;

                            continue;
                        }
                    }

                    if (-1 != this._remainderToShow)
                    {
                        if (remainder == this._remainderToShow)
                        {
                            this.Result.Add(true);
                            dp[num] = true;

                            continue;
                        }
                    }

                    this.Result.Add(this._defaultVis);
                    dp[num] = this._defaultVis;
                }
            }
        }

        #endregion
    }
}