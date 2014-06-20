using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider
{
    /// <summary>
    /// This is the result of a WI Linq Query.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class TPCQuery
    {

        private readonly string _wiql;
        private readonly TfsTeamProjectCollection _tpc;
        readonly Dictionary<string, object> _defaultParameters;
        private readonly QueryType _type;

        public TPCQuery(TfsTeamProjectCollection tpc, string wiql, Dictionary<string, object> defaultParameters, QueryType type)
        {
            if (tpc == null)
            {
                throw new ArgumentNullException("tpc");
            }
            
            _wiql = wiql;
            _tpc = tpc;            
            _defaultParameters = defaultParameters;
            _type = type;
        }

        public WorkItemLinkInfo[] GetLinks()
        {
            var store = GetWorkItemStore();

            var query = new Query(store, _wiql, _defaultParameters);

            return query.RunLinkQuery();
        }

        private WorkItemStore GetWorkItemStore()
        {
            var store = _tpc.GetService(typeof(WorkItemStore)) as WorkItemStore;
            return store;
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
