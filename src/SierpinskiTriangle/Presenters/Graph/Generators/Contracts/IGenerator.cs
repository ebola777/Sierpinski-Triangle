namespace SierpinskiTriangle.Presenters.Graph.Generators.Contracts
{
    using System.Collections.Generic;

    public interface IGenerator<TVal>
    {
        #region Public Properties

        int GeneratedHigh { get; set; }

        IList<TVal> Sequence { get; set; }

        #endregion

        #region Public Methods and Operators

        GeneratorResult<TVal> Generate(int numLevels);

        void GenerateLines(int beginLine, int endLine);

        #endregion
    }
}