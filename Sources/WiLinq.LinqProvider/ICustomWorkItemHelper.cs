using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider
{
    public interface ICustomWorkItemHelper<out T> : ILinqResolver
        where T: class
    {
        T CreateItem(WorkItem workitem);        
    }
}