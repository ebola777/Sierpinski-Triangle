namespace SierpinskiTriangle.Utilities
{
    using System.ComponentModel;

    public static class DynamicPropertiesHelper
    {
        #region Public Methods and Operators

        public static void SetAllPropertiesToDefault<TClass>(TClass cls) where TClass : class
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(cls))
            {
                var myAttribute = (DefaultValueAttribute)prop.Attributes[typeof(DefaultValueAttribute)];

                if (null != myAttribute)
                {
                    prop.SetValue(cls, myAttribute.Value);
                }
            }
        }

        #endregion
    }
}