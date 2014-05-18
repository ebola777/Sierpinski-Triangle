namespace SierpinskiTriangle.Utilities
{
    using System;
    using System.Reflection;

    public static class AssemblyInfo
    {
        #region Public Methods and Operators

        public static Version GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        #endregion
    }
}