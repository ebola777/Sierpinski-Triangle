namespace SierpinskiTriangle.Presenters.Graph.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    using Ciloci.Flee;

    using SierpinskiTriangle.Presenters.Graph.Strategies.Base;
    using SierpinskiTriangle.Utilities;

    public class ExpressionStrategy : StrategyBase<BigInteger>
    {
        #region Fields

        private readonly ExpressionContext _expContext;

        private readonly string _expression;

        private readonly GetSequenceNumber _seqNumHelper;

        #endregion

        #region Constructors and Destructors

        public ExpressionStrategy(IList<BigInteger> sequence, string expression)
            : base(sequence)
        {
            this._expression = expression;

            this._seqNumHelper = new GetSequenceNumber();
            this._expContext = new ExpressionContext(this._seqNumHelper);

            this._expContext.Imports.AddType(typeof(Math));
            this._expContext.Imports.AddType(typeof(BigInteger));
        }

        #endregion

        #region Public Methods and Operators

        public override void Calculate()
        {
            var dp = new Dictionary<BigInteger, bool>();

            this.Result = new List<bool>();

            try
            {
                IGenericExpression<bool> exp = this._expContext.CompileGeneric<bool>(this._expression);

                foreach (BigInteger num in this.Sequence)
                {
                    bool dpVal;

                    if (dp.TryGetValue(num, out dpVal))
                    {
                        this.Result.Add(dpVal);
                    }
                    else
                    {
                        this._seqNumHelper.Num = num;
                        bool ret = exp.Evaluate();

                        this.Result.Add(ret);
                        dp[num] = ret;
                    }
                }
            }
            catch (ExpressionCompileException ex)
            {
                ErrorHandling.ShowException(ex);
                throw;
            }
        }

        #endregion
    }

    public class GetSequenceNumber
    {
        #region Public Properties

        public BigInteger Num { get; set; }

        #endregion

        #region Public Methods and Operators

        public BigInteger n()
        {
            return this.Num;
        }

        #endregion
    }
}