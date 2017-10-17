using System;

namespace WiLinq.LinqProvider.Extensions
{
    public abstract class WorkItemValueConverter
    {
        public abstract Type RelatedType { get; }
        public abstract object Resolve(object obj);
    }
}