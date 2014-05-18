namespace SierpinskiTriangle.Presenters.Graph.Cache
{
    using System.Collections.Generic;
    using System.Numerics;

    public class CacheContent
    {
        #region Constructors and Destructors

        public CacheContent()
        {
            this.GeneratedHigh = 0;
            this.Sequence = new List<BigInteger>();
        }

        #endregion

        #region Public Properties

        public int GeneratedHigh { get; set; }

        public IList<BigInteger> Sequence { get; set; }

        #endregion
    }
}