using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.LinqProvider
{
    /// <summary>
    /// Query extender used to provider LINQ capability to the TFS model
    /// </summary>
    public static class QueryExtender
    {
        /// <summary>
        /// Extends the server for a LINQ Query on the work item store.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        public static IQueryable<WorkItem> WorkItemSet(this TfsTeamProjectCollection server)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(server,null,new TFSWorkItemHelper()));
        }

        /// <summary>
        /// Extends the project for a LINQ Query on the work item store.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static IQueryable<WorkItem> WorkItemSet(this Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }            
            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(project,new TFSWorkItemHelper()));
        }

        /// <summary>
        /// Extends the project for a specific work item type LINQ Query.
        /// </summary>
        /// <typeparam name="T">Type of work item</typeparam>
        /// <typeparam name="THelper">Related creator provider</typeparam>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static IQueryable<T> SetOf<T, THelper>(this Project project)
            where T : WorkItemBase
            where THelper : ICustomWorkItemHelper<T>, new()
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }           

            return new Query<T>(new WorkItemLinqQueryProvider<T>(project,new THelper()));
        }

        /// <summary>
        /// Extends the project for a specific work item type LINQ Query.
        /// </summary>
        /// <typeparam name="T">Type of work item</typeparam>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static IQueryable<T> SetOf<T>(this Project project) where T : WorkItemBase, new()
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }           

            if (!WorkItemPropertyUtility<T>.IsValid)
            {
                throw new InvalidOperationException(String.Format("Type '{0}' does not have a the required attributes", typeof(T)));
            }

            return new Query<T>(new WorkItemLinqQueryProvider<T>(project, WorkItemPropertyUtility<T>.Provider));
        }
#if false
        public static IQueryable<T> OfType<T, U>(this IQueryable<U> query)
            where T : U, new()
            where U : WorkItemBase, new()
        {
            WorkItemLinqQueryProvider<U> wiQP = (query as IQueryable<U>).Provider as WorkItemLinqQueryProvider<U>;
            return new Query<T>(new WorkItemLinqQueryProvider<T>(wiQP.TPC,wiQP.Project, WorkItemPropertyUtility<T>.Provider));
        }
#endif

        public static WorkItemLinkInfo [] QueryLinks(
            this TfsTeamProjectCollection tpc,
            Expression<Predicate<WorkItem>> sourcePredicate,
            Expression<Predicate<WorkItem>> targetPredicate,
            string linkType,
            QueryLinkMode mode
            )
        {
            var qb = new QueryBuilder(QueryType.Link);


            var sourceLambda = sourcePredicate as LambdaExpression;
            var targetLambda = targetPredicate as LambdaExpression;


            var whereSourceTranslator = new WhereClauseTranslator(new TFSWorkItemHelper(), "Source");

            var sourceFilter = whereSourceTranslator.Translate(sourceLambda.Body, qb, sourceLambda.Parameters[0].Name);

            if (!string.IsNullOrEmpty(sourceFilter))
            {
                qb.AddWhereClause(sourceFilter);
            }


            var whereTargetTranslator = new WhereClauseTranslator(new TFSWorkItemHelper(), "Target");

            var targetFilter = whereTargetTranslator.Translate(sourceLambda.Body, qb, targetLambda.Parameters[0].Name);

            if (!string.IsNullOrEmpty(targetFilter))
            {
                qb.AddWhereClause(targetFilter);
            }

            qb.AddQueryLinkMode(mode);
            

            var q = qb.BuildQuery(tpc, null, null);
            
            return q.GetLinks();       
        }

        public static IQueryable<WorkItemLinkInfo> LinkOfQuery<TSource,TTarget>(
            this Project project,
            Expression<Predicate<TSource>> sourcePredicate,
            Expression<Predicate<TTarget>> targetPredicate,
            string linkType)
        {
            throw new NotImplementedException();
        }

        #region Type conversions

        /// <summary>
        /// Check if a work item is of a the specified type
        /// </summary>
        /// <typeparam name="T">Workitem Type</typeparam>
        /// <param name="wi"></param>
        /// <returns></returns>
        public static bool Is<T>(this WorkItem wi) where T : WorkItemBase,new()
        {
            if (!WorkItemPropertyUtility<T>.IsValid)
            {
                throw new InvalidOperationException(String.Format("Type '{0}' does not have a the required attributes", typeof(T)));
            }

            return wi.Type.Name == WorkItemPropertyUtility<T>.WorkItemTypeName;
        }

        /// <summary>
        /// Cast a work item. 
        /// </summary>
        /// <typeparam name="T">Workitem Type</typeparam>
        /// <param name="wi">The wi.</param>
        /// <returns></returns>
        public static T As<T>(this WorkItem wi) where T : WorkItemBase, new()
        {
            if (!Is<T>(wi))
            {
                return null;
            }

            if (!WorkItemPropertyUtility<T>.IsValid)
            {
                throw new InvalidOperationException(String.Format("Type '{0}' does not have a the needed attributes", typeof(T)));
            }

            var ret = new T
            {
                WorkItem = wi
            };

            return ret;
        }

     

        /// <summary>
        /// Checks if the specified project can used the specified Work Item Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static bool IsSupported<T>(this Project project) where T : WorkItemBase,new()
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }
            return WorkItemPropertyUtility<T>.CheckProjectUsability(project);
        }
        #endregion

        /// <summary>
        /// Returns a typed workitem for the specific project
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="project"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Get<T>(this Project project, int id) where T : WorkItemBase, new()
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            if (!project.IsSupported<T>())
            {
                throw new ArgumentException(string.Format("{0} is not supported in {1}",typeof(T).FullName,project.Name),"project");
            }

            var wi = project.Store.GetWorkItem(id);

            if (wi == null)
            {
                return null;
            }

            if (wi.Project.Name != project.Name)
            {
                throw new ArgumentException(string.Format("Workitem #{0} is not in project '{1}'",id,project.Name),"id");
            }

            if (!wi.Is<T>())
            {
                throw new ArgumentException(string.Format("Workitem #{0} is of type '{1}'", id, typeof(T).FullName), "id");
            }

            var result = new T()
            {
                WorkItem = wi
            };
            return result;

        }
    }


}
