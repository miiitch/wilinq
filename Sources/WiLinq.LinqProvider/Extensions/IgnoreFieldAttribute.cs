using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Extensions
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]
    public sealed class IgnoreFieldAttribute : Attribute
    {
        public string ReferenceName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the FieldToIgnoreAttribute class.
        /// </summary>
        public IgnoreFieldAttribute(string referenceName)
        {
            if (String.IsNullOrEmpty(referenceName))
                throw new ArgumentException("referenceName is null or empty.", "referenceName");
            ReferenceName = referenceName;
        }
    }
}
