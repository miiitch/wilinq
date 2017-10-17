using System;

namespace WiLinq.LinqProvider
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class IgnoreFieldAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the FieldToIgnoreAttribute class.
        /// </summary>
        public IgnoreFieldAttribute(string referenceName)
        {
            if (string.IsNullOrEmpty(referenceName))
            {
                throw new ArgumentException("referenceName is null or empty.", nameof(referenceName));
            }
            ReferenceName = referenceName;
        }

        public string ReferenceName { get; }
    }
}