using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider.Extensions
{
    public abstract class WorkItemValueConverter
    {
        public abstract Type RelatedType { get; }
        public abstract object Resolve(object obj);
    }
}
