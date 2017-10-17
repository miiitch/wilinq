using System;

namespace WiLinq.LinqProvider.Extensions
{
    /// <summary>
    ///     Attribute used to associate a work item type to its creation provider
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class WorkItemCreatorProviderAttribute : Attribute
    {
        public WorkItemCreatorProviderAttribute(Type creator)
        {
            if (creator == null)
            {
                throw new ArgumentNullException(nameof(creator), "creator is null.");
            }

            CreatorType = creator;
        }

        public Type CreatorType { get; }
    }
}