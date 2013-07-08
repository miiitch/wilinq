using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;

namespace WiLinq.LinqProvider
{
    /// <summary>
    /// This is the result of a WI Linq Query.
    /// </summary>
    internal class TPCQuery
    {

        private string _wiql;
        private TfsTeamProjectCollection _tpc;
        Dictionary<string, object> _defaultParameters;
        private QueryType _type;

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
