using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider
{
    internal interface ICustomWorkItemHelper<out T> : ILinqResolver
        where T : class
    {
        T CreateItem(WorkItem workitem);
    }
}