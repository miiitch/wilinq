using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider.Extensions
{
    internal class WorkItemBaseHelper<T> : CustomWorkItemResolver<T>, ICustomWorkItemHelper<T>
        where T : GenericWorkItem, new()
    {
        #region ICustomWorkItemHelper<T> Members

        public T CreateItem(WorkItem workitem)
        {
            var ret = new T();
            ret.CopyValuesFromWorkItem(workitem);
            return ret;
        }

        #endregion
    }
}