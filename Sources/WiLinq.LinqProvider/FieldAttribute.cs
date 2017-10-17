using System;

namespace WiLinq.LinqProvider
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the FieldToIgnoreAttribute class.
        /// </summary>
        public FieldAttribute(string referenceName)
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