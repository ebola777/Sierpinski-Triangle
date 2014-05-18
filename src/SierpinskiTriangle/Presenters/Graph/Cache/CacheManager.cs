namespace SierpinskiTriangle.Presenters.Graph.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    using Newtonsoft.Json;

    using SierpinskiTriangle.Utilities;

    public class CacheManager
    {
        #region Fields

        private readonly int _maxGeneratedHigh;

        private readonly int _maxNumItems;

        private readonly string _pathCache;

        private int _lastGeneratedHigh;

        #endregion

        #region Constructors and Destructors

        public CacheManager(string pathCache, int maxGenerateHigh, int maxNumItems)
        {
            this._pathCache = pathCache;
            this._lastGeneratedHigh = 0;
            this._maxGeneratedHigh = maxGenerateHigh;
            this._maxNumItems = maxNumItems;
        }

        #endregion

        #region Public Properties

        public CacheContent CacheContent { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Read()
        {
            try
            {
                this.CacheContent = Json<CacheContent>.Read(this._pathCache);
            }
            catch (Exception)
            {
                this.CacheContent = new CacheContent();
            }

            // validation
            if (null == this.CacheContent.Sequence)
            {
                this.CacheContent = new CacheContent();
            }
        }

        public void Save()
        {
            if (this._maxGeneratedHigh <= this._lastGeneratedHigh)
            {
                return;
            }

            var serializer = new JsonSerializer();
            var sequence = (List<BigInteger>)this.CacheContent.Sequence;

            FileSystemHelper.EnsurePathExists(this._pathCache);

            // limit size
            if (sequence.Count > this._maxNumItems)
            {
                this.CacheContent.GeneratedHigh = this._maxGeneratedHigh;
                this.CacheContent.Sequence = sequence.GetRange(0, this._maxNumItems);
            }

            Json<CacheContent>.Write(this._pathCache, this.CacheContent, serializer);

            this._lastGeneratedHigh = this.CacheContent.GeneratedHigh;
        }

        #endregion
    }
}