using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using QueryType = WiLinq.LinqProvider.QueryGeneration.QueryType;

namespace WiLinq.LinqProvider
{
    /// <summary>
    ///     This is the result of a WI Linq Query.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class TPCQuery
    {
        private const int WORKITEM_QUERY_BATCH_SIZE = 50;
        private readonly string _wiql;
        private readonly WorkItemTrackingHttpClient _workItemTrackingHttpClient;
        private string _projectName;
        private string _teamName;

        private readonly QueryType _type;

        public TPCQuery(WorkItemTrackingHttpClient workItemTrackingHttpClient, string wiql, QueryType type,
            string projectName, string teamName)
        {
            _wiql = wiql;
            _workItemTrackingHttpClient = workItemTrackingHttpClient ??
                                          throw new ArgumentNullException(nameof(workItemTrackingHttpClient));

            _type = type;
            _projectName = projectName;
            _teamName = teamName;
        }

        public LambdaExpression SelectLambda { get; set; }

#if false
        public WorkItemLinkInfo[] GetLinks()
        {
            var store = GetWorkItemStore();

            var query = new Query(store, _wiql, _defaultParameters);

            return query.RunLinkQuery();
        }

#endif
        public Task<List<int>> GetWorkItemIdsAsync(CancellationToken cancellationToken)
        {
            if (_type != QueryType.WorkItem)
            {
                throw new InvalidOperationException("This is not a workitem query");
            }

            return GetWorkItemsAsyncCore();

            async Task<List<int>> GetWorkItemsAsyncCore()
            {
                var result = await _workItemTrackingHttpClient.QueryByWiqlAsync(
                    new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.Wiql {Query = _wiql},
                    null,
                    null,
                    null,
                    cancellationToken);

                if (_type == QueryType.WorkItem && result.QueryType !=
                    Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryType.Flat)
                {
                    throw new InvalidOperationException("Invalid query type");
                }

                var ids = result.WorkItems.Select(_ => _.Id).ToList();
                return ids;
            }
        }

        public static List<List<T>> Batch<T>(List<T> locations, int nSize = 30)
        {
            var list = new List<List<T>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }

        public async Task<List<WorkItem>> GetWorkItemsAsync(CancellationToken cancellationToken)
        {
            var ids = await GetWorkItemIdsAsync(cancellationToken);
            try
            {
                var workitems = new List<WorkItem>(ids.Count);

                var batches = Batch(ids, WORKITEM_QUERY_BATCH_SIZE);
                foreach (var batch in batches)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var batchedWorkItems = await _workItemTrackingHttpClient.GetWorkItemsAsync(batch,null,null,null,null,null, cancellationToken);
                    workitems.AddRange(batchedWorkItems);
                }

                return workitems;
            }
            catch (VssServiceResponseException notFoundResponseException) when (notFoundResponseException.Message ==
                                                                                "Not Found")
            {
                return new List<WorkItem>();
            }
        }
    }
}