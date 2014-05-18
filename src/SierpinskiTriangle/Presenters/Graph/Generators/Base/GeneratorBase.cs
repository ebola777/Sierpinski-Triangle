namespace SierpinskiTriangle.Presenters.Graph.Generators.Base
{
    using System.Collections.Generic;

    using SierpinskiTriangle.Presenters.Graph.Generators.Contracts;

    public abstract class GeneratorBase<TVal> : IGenerator<TVal>
    {
        #region Constructors and Destructors

        protected GeneratorBase()
        {
            this.GeneratedHigh = 0;
            this.Sequence = new List<TVal>();
        }

        protected GeneratorBase(int generatedHigh, IList<TVal> sequence)
        {
            this.GeneratedHigh = generatedHigh;
            this.Sequence = sequence;
        }

        #endregion

        #region Public Properties

        public int GeneratedHigh { get; set; }

        public IList<TVal> Sequence { get; set; }

        #endregion

        #region Public Methods and Operators

        public abstract GeneratorResult<TVal> Generate(int numLevels);

        public abstract void GenerateLines(int beginLine, int endLine);

        #endregion
    }
}