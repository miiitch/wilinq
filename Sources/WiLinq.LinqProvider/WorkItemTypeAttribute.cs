using System;

namespace WiLinq.LinqProvider
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class WorkItemTypeAttribute : Attribute
    {
        public WorkItemTypeAttribute(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentException("typeName is null or empty.", nameof(typeName));
            }
            TypeName = typeName;
        }

        public string TypeName { get; }
    }
}