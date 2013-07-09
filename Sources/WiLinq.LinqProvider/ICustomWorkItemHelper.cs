using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;
using System.Reflection;

namespace WiLinq.LinqProvider
{
    public interface ICustomWorkItemHelper<T> : ILinqResolver
        where T: class
    {
        T CreateItem(WorkItem workitem);        
    }
}