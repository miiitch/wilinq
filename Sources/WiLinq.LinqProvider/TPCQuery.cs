using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
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
        readonly Dictionary<string, object> _defaultParameters;
        private readonly QueryType _type;

        public TPCQuery(WorkItemTrackingHttpClient workItemTrackingHttpClient, string wiql, Dictionary<string, object> defaultParameters, QueryType type)
        {
            _wiql = wiql;
            _workItemTrackingHttpClient = workItemTrackingHttpClient ?? throw new ArgumentNullException(nameof(workItemTrackingHttpClient));            
            _defaultParameters = defaultParameters;
            _type = type;
        }

        public WorkItemLinkInfo[] GetLinks()
        {
            var store = GetWorkItemStore();

            var query = new Query(store, _wiql, _defaultParameters);

            return query.RunLinkQuery();
        }

        public WorkItem[] GetWorkItems()
        {
            if (_type != QueryType.WorkItem)
            {
                throw new InvalidOperationException("This is not a workitem query");
            }

            var store = GetWorkItemStore();

         
            
            return store.Query(_wiql, _defaultParameters)
                .Cast<WorkItem>()
                .ToArray();
        }


    }
}
