using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using WiLinq.LinqProvider.Wiql;

namespace WiLinq.LinqProvider
{
    /// <summary>
    /// This is the result of a WI Linq Query.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class TPCQuery
    {
        private readonly string _wiql;
        private readonly WorkItemTrackingHttpClient _workItemTrackingHttpClient;
        private string _projectName;
        private string _teamName;
            
        private readonly QueryType _type;

        public TPCQuery(WorkItemTrackingHttpClient workItemTrackingHttpClient, string wiql,QueryType type, string projectName, string teamName)
        {
            _wiql = wiql;
            _workItemTrackingHttpClient = workItemTrackingHttpClient ??
                                          throw new ArgumentNullException(nameof(workItemTrackingHttpClient));
           
            _type = type;
            _projectName = projectName;
            _teamName = teamName;
        }
#if false
        public WorkItemLinkInfo[] GetLinks()
        {
            var store = GetWorkItemStore();

            var query = new Query(store, _wiql, _defaultParameters);

            return query.RunLinkQuery();
        }

#endif
        public Task<List<WorkItem>> GetWorkItemsAsync()
        {
            if (_type != QueryType.WorkItem)
            {
                throw new InvalidOperationException("This is not a workitem query");
            }

            return GetWorkItemsAsyncCore();

            async Task<List<WorkItem>> GetWorkItemsAsyncCore()
            {
                
                var result = await _workItemTrackingHttpClient.QueryByWiqlAsync(new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.Wiql() { Query = _wiql});
                if (_type == QueryType.WorkItem && result.QueryType !=
                    Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryType.Flat)
                {
                    throw new InvalidOperationException("Invalid query type");
                }

                var ids = result.WorkItems.Select(_ => _.Id);
                try
                {
                    var workitems = await _workItemTrackingHttpClient.GetWorkItemsAsync(ids);
                    return workitems;
                }
                catch (VssServiceResponseException notFoundResponseException) when(notFoundResponseException.Message == "Not Found")
                {
                    return new List<WorkItem>();
                }
            }
        }
    }
}