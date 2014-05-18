namespace SierpinskiTriangle.Presenters.Graph.Strategies.Contracts
{
    using System.Collections.Generic;

    public interface IStrategy
    {
        #region Public Properties

        IList<bool> Result { get; }

        #endregion

        #region Public Methods and Operators

        void Calculate();

        #endregion
    }
}