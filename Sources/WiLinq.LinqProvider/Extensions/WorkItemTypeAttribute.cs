using System;

namespace WiLinq.LinqProvider.Extensions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class WorkItemTypeAttribute : Attribute
    {
        private readonly string _typeName;

        public WorkItemTypeAttribute(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentException("typeName is null or empty.", nameof(typeName));
            _typeName = typeName;
        }

        public string TypeName
        {
            get
            {
                return _typeName;
            }
        }
    }
}
