using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;
using System.Reflection;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.LinqProvider
{
    internal class WorkItemLinqQueryProvider<T> : IWorkItemLinqQueryProvider where T:class
    {
        readonly TfsTeamProjectCollection _tpc;
        readonly Project _project;
        readonly ICustomWorkItemHelper<T> _creatorProvider;
        DateTime? _asOfDate;
        

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
            return (TOutput)Execute(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {

            return Execute(expression);

        }

        public WorkItemLinqQueryProvider(TfsTeamProjectCollection tpc, Project project, ICustomWorkItemHelper<T> creatorProvider)
        {
            if (tpc == null)
            {
                throw new ArgumentNullException("tpc");
            }

            _tpc = tpc;
            _project = project;
            _creatorProvider = creatorProvider;
        }

        public WorkItemLinqQueryProvider(Project project, ICustomWorkItemHelper<T> creatorProvider)
            : this(project.Store.TeamProjectCollection, project, creatorProvider)
        {

        }

        public WorkItemLinqQueryProvider(TfsTeamProjectCollection tpc)
            : this(tpc, null, null)
        {

        }

        private IEnumerable<TResult> ApplySelect<TResult>(T[] workItems, Func<T, TResult> select)
        {
            return workItems.Select(select);
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

        public object Execute(Expression expression)
        {
           
            var translator = new QueryTranslator(_creatorProvider);
            var queryBuilder = translator.Translate(expression);

            ConfigureExtraFilters(queryBuilder);

            var query = queryBuilder.BuildQuery(_tpc, _project, _asOfDate);

            var tmpResult = query.GetWorkItems();

            T[] wiResult;
            if (_creatorProvider != null)
            {
                wiResult = (from wi in tmpResult
                            select _creatorProvider.CreateItem(wi)).ToArray();
            }
            else
            {
                if (typeof(WorkItem) != typeof(T))
                {
                    throw new InvalidOperationException("creatorProvider required");
                }
                wiResult = tmpResult as T[];
            }


            if (queryBuilder.SelectLambda == null)
            {
                return wiResult;
            }

            var deleg = queryBuilder.SelectLambda.Compile();

            var applySelect = GetType().GetMethod("ApplySelect", BindingFlags.NonPublic | BindingFlags.Instance);

            var applySelectGeneric = applySelect.MakeGenericMethod(deleg.Method.ReturnType);

            var resultList = applySelectGeneric.Invoke(this, new object[] { wiResult, deleg });
            return resultList;
        }

        private void ConfigureExtraFilters(QueryBuilder queryBuilder)
        {
            if (_project != null)
            {
                queryBuilder.AddWhereClause(SystemField.Project + " = @project");
            }

            var attribs = typeof(T).GetCustomAttributes(typeof(WorkItemTypeAttribute), false) as WorkItemTypeAttribute[];

            if (attribs != null && attribs.Length == 1)
            {
                queryBuilder.AddWhereClause(string.Format("{0} = '{1}'", SystemField.WorkItemType, attribs[0].TypeName));
            }
        }

        public TPCQuery TransformAsWorkItemQuery(Expression expression)
        {
            return null;
        }

        public DateTime? AsOfDate
        {
            get
            {
                return _asOfDate;
            }
            set
            {
                if (_asOfDate.HasValue)
                {
                    throw new InvalidOperationException("AsOf Date already defined");
                }
                _asOfDate = value;
            }
        }
        // ReSharper disable once InconsistentNaming
        public TfsTeamProjectCollection TPC
        {
            get
            {
                return _tpc;
            }
        }
        public Project Project
        {
            get
            {
                return _project;
            }
        }

    }
}
