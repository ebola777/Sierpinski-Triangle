namespace SierpinskiTriangle.Presenters.Graph.Generators
{
    using System.Collections.Generic;

    public class GeneratorResult<TVal>
    {
        #region Constructors and Destructors

        public GeneratorResult()
        {
        }

        public GeneratorResult(int generatedHigh, IList<TVal> sequence)
        {
            this.GeneratedHigh = generatedHigh;
            this.Sequence = sequence;
        }

        #endregion

        #region Public Properties

        public int GeneratedHigh { get; set; }

        public IList<TVal> Sequence { get; set; }

        #endregion
    }
}