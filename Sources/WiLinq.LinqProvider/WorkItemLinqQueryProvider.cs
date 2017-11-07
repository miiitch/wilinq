using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WiLinq.LinqProvider.QueryGeneration;

namespace WiLinq.LinqProvider
{
    internal class WorkItemLinqQueryProvider<T> : IWorkItemLinqQueryProvider where T : class
    {
        private readonly ICustomWorkItemHelper<T> _creatorProvider;
        private readonly string _projectName;
        private DateTime? _asOfDate;

        public WorkItemLinqQueryProvider(WorkItemTrackingHttpClient workItemTrackingHttpClient, string projectName
            , ICustomWorkItemHelper<T> creatorProvider)
        {
            WorkItemTrackingHttpClient = workItemTrackingHttpClient ??
                                         throw new ArgumentNullException(nameof(workItemTrackingHttpClient));
            _projectName = projectName;
            _creatorProvider = creatorProvider;
        }


        public WorkItemLinqQueryProvider(WorkItemTrackingHttpClient workItemTrackingHttpClient)
            : this(workItemTrackingHttpClient, null, null)
        {
        }

        public WorkItemTrackingHttpClient WorkItemTrackingHttpClient { get; }


        IQueryable<TOutput> IQueryProvider.CreateQuery<TOutput>(Expression expression)
        {
            return new Query<TOutput>(this, expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            throw new NotSupportedException();
        }

        TOutput IQueryProvider.Execute<TOutput>(Expression expression)
        {
            return (TOutput) Execute(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return Execute(expression);
        }

        public string GetQueryText(Expression expression)
        {
            //QueryTranslator translator = new QueryTranslator();
            //QueryBuilder queryBuilder = translator.Translate(expression);

            //ConfigureExtraFilters(queryBuilder);

            //WorkItemQuery query = queryBuilder.BuildQuery(_server, _project, _asOfDate);
            //return query.WIQL;
            throw new NotImplementedException();
        }

        public async Task<List<int>> ExecuteAndGetIdsAsync(Expression expression)
        {
            var query = BuildQuery(expression);

            var result = await query.GetWorkItemIdsAsync();

            return result;
        }

        public async Task<object> ExecuteAync(Expression expression)
        {
            var query = BuildQuery(expression);

            var tmpResult = await query.GetWorkItemsAsync();

            List<T> wiResult;
            if (_creatorProvider != null)
            {
                wiResult = (from wi in tmpResult
                    select _creatorProvider.CreateItem(wi)).ToList();
            }
            else
            {
                if (typeof(WorkItem) != typeof(T))
                {
                    throw new InvalidOperationException("creatorProvider required");
                }
                wiResult = tmpResult as List<T>;
            }


            if (query.SelectLambda == null)
            {
                return wiResult;
            }

            var deleg = query.SelectLambda.Compile();

            var applySelect = GetType().GetMethod("ApplySelect", BindingFlags.NonPublic | BindingFlags.Instance);

            var applySelectGeneric = applySelect.MakeGenericMethod(deleg.Method.ReturnType);

            var resultList = applySelectGeneric.Invoke(this, new object[] {wiResult, deleg});
            return resultList;
        }

        public TPCQuery TransformAsWorkItemQuery(Expression expression)
        {
            return null;
        }

        public DateTime? AsOfDate
        {
            get => _asOfDate;
            set
            {
                if (_asOfDate.HasValue)
                {
                    throw new InvalidOperationException("AsOf Date already defined");
                }
                _asOfDate = value;
            }
        }

        private IEnumerable<TResult> ApplySelect<TResult>(IEnumerable<T> workItems, Func<T, TResult> select)
        {
            return workItems.Select(select).ToList();
        }

        private object Execute(Expression expression)
        {
            return Task.Run(() => ExecuteAync(expression)).Result;
        }

        private TPCQuery BuildQuery(Expression expression)
        {
            var translator = new QueryTranslator(_creatorProvider);
            var queryBuilder = translator.Translate(expression);

            ConfigureExtraFilters(queryBuilder);

            var query = queryBuilder.BuildQuery(WorkItemTrackingHttpClient, _projectName, _asOfDate);

            return query;
        }

        private void ConfigureExtraFilters(QueryBuilder queryBuilder)
        {
            var attribs =
                typeof(T).GetCustomAttributes(typeof(WorkItemTypeAttribute), false) as WorkItemTypeAttribute[];

            if (attribs != null && attribs.Length == 1)
            {
                queryBuilder.AddWhereClause($"{SystemField.WorkItemType} = '{attribs[0].TypeName}'");
            }
        }
    }
}