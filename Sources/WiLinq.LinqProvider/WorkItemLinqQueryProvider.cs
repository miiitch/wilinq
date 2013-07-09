using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;
using System.Reflection;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.LinqProvider
{
    internal class WorkItemLinqQueryProvider<T> : IWorkItemLinqQueryProvider where T:class
    {
        TfsTeamProjectCollection _tpc;
        Project _project;
        DateTime? _asOfDate;
        ICustomWorkItemHelper<T> _creatorProvider;

        IQueryable<S> IQueryProvider.CreateQuery<S>(Expression expression)
        {
            return new Query<S>(this, expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            throw new NotSupportedException();
        }

        S IQueryProvider.Execute<S>(Expression expression)
        {
            return (S)this.Execute(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {

            return this.Execute(expression);

        }

        public WorkItemLinqQueryProvider(TfsTeamProjectCollection tpc, Project project, ICustomWorkItemHelper<T> creatorProvider)
            : base()
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


        public string GetQueryText(Expression expression)
        {
            //QueryTranslator translator = new QueryTranslator();
            //QueryBuilder queryBuilder = translator.Translate(expression);

            //ConfigureExtraFilters(queryBuilder);

            //WorkItemQuery query = queryBuilder.BuildQuery(_server, _project, _asOfDate);
            //return query.WIQL;
            throw new NotImplementedException();
        }

        private List<TResult> ApplySelect<TResult>(T[] wiArray, Func<T, TResult> deleg)
        {
            List<TResult> result = new List<TResult>();

            foreach (var wi in wiArray)
            {
                result.Add(deleg(wi));
            }

            return result;
        }

        public object Execute(Expression expression)
        {
           
            QueryTranslator translator = new QueryTranslator(_creatorProvider);
            QueryBuilder queryBuilder = translator.Translate(expression);

            ConfigureExtraFilters(queryBuilder);

            TPCQuery query = queryBuilder.BuildQuery(_tpc, _project, _asOfDate);

            WorkItem[] tmpResult = query.GetWorkItems();

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
            else
            {
                Delegate deleg = queryBuilder.SelectLambda.Compile();

                MethodInfo applySelect = this.GetType().GetMethod("ApplySelect", BindingFlags.NonPublic | BindingFlags.Instance);

                MethodInfo applySelectGeneric = applySelect.MakeGenericMethod(deleg.Method.ReturnType);

                object resultList = applySelectGeneric.Invoke(this, new object[] { wiResult, deleg });
                return resultList;
            }
        }

        private void ConfigureExtraFilters(QueryBuilder queryBuilder)
        {
            if (_project != null)
            {
                queryBuilder.AddWhereClause(SystemField.Project + " = @project");
            }

            WorkItemTypeAttribute[] attribs = typeof(T).GetCustomAttributes(typeof(WorkItemTypeAttribute), false) as WorkItemTypeAttribute[];

            if (attribs != null && attribs.Length == 1)
            {
                queryBuilder.AddWhereClause(String.Format("{0} = '{1}'", SystemField.WorkItemType, attribs[0].TypeName));
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
