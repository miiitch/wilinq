using System;

namespace WiLinq.LinqProvider.Extensions
{
    /// <summary>
    /// Attribute used to associate a work item type to its creation provider
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class WorkItemCreatorProviderAttribute : Attribute
    {
        private readonly Type _creator;

        public WorkItemCreatorProviderAttribute(Type creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator), "creator is null.");

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
