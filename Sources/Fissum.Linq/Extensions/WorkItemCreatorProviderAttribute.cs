using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiLinq.LinqProvider.Extensions
{
    /// <summary>
    /// Attribute used to associate a work item type to its creation provider
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class WorkItemCreatorProviderAttribute : Attribute
    {
        private Type _creator;

        public WorkItemCreatorProviderAttribute(Type creator)
        {
            if (creator == null)
                throw new ArgumentNullException("creator", "creator is null.");

            _creator = creator;
        }

        public Type CreatorType
        {
            get
            {
                return _creator;
            }
        }
    }
}
