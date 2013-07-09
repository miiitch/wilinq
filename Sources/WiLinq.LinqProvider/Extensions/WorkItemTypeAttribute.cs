using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;

namespace WiLinq.LinqProvider.Extensions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class WorkItemTypeAttribute : Attribute
    {
        private string _typeName;

        public WorkItemTypeAttribute(string typeName)
        {
            if (String.IsNullOrEmpty(typeName))
                throw new ArgumentException("typeName is null or empty.", "typeName");
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
