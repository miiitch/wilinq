using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WiLinq.LinqProvider.Extensions;
using WiLinq.LinqProvider.Process;

namespace WiLinq.LinqProvider
{
    /// <summary>
    ///     Query extender used to provider LINQ capability to the TFS model
    /// </summary>
    public static class QueryExtender
    {
        /// <summary>
        ///     Extends the server for a LINQ Query on the work item store.
        /// </summary>
        /// <param name="workItemTrackingHttpClient">The server.</param>
        /// <returns></returns>
        public static IQueryable<WorkItem> All(this WorkItemTrackingHttpClient workItemTrackingHttpClient)
        {
            if (workItemTrackingHttpClient == null)
            {
                throw new ArgumentNullException(nameof(workItemTrackingHttpClient));
            }

            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(workItemTrackingHttpClient, null,
                new WorkItemHelper()));
        }

        /// <summary>
        ///     Extends the project for a LINQ Query on the work item store.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static IQueryable<WorkItem> All(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            ProjectInfo project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(workItemTrackingHttpClient, project.Name,
                new WorkItemHelper()));
        }

        public static IQueryable<WorkItem> All(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            TeamProject project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(workItemTrackingHttpClient, project.Name,
                new WorkItemHelper()));
        }

        public static IQueryable<GenericWorkItem> FromTemplate<T>(
            this WorkItemTrackingHttpClient workItemTrackingHttpClient) where T : ProcessTemplate, new()
        {
            return FromTemplate(workItemTrackingHttpClient, new T());
        }

        public static IQueryable<GenericWorkItem> FromTemplate(
            this WorkItemTrackingHttpClient workItemTrackingHttpClient, ProcessTemplate template)
        {
            return new Query<GenericWorkItem>(new WorkItemLinqQueryProvider<GenericWorkItem>(workItemTrackingHttpClient,
                null, new ProcessTemplateHelper(template)));
        }

        /// <summary>
        ///     Extends the project for a specific work item type LINQ Query.
        /// </summary>
        /// <typeparam name="T">Type of work item</typeparam>
        /// <typeparam name="THelper">Related creator provider</typeparam>
        /// <param name="workItemTrackingHttpClient"></param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        internal static IQueryable<T> SetOf<T, THelper>(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            ProjectInfo project)
            where T : GenericWorkItem
            where THelper : ICustomWorkItemHelper<T>, new()
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            return new Query<T>(new WorkItemLinqQueryProvider<T>(workItemTrackingHttpClient, project.Name,
                new THelper()));
        }


        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            ProjectInfo projectInfo) where T : GenericWorkItem, new()
        {
            return SetOf<T>(workItemTrackingHttpClient, projectInfo.Name);
        }

        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            TeamProject teamProject) where T : GenericWorkItem, new()
        {
            return SetOf<T>(workItemTrackingHttpClient, teamProject.Name);
        }

        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient)
            where T : GenericWorkItem, new()
        {
            if (!GenericWorkItemHelpers<T>.IsValid)
            {
                throw new InvalidOperationException($"Type '{typeof(T)}' does not have a the required attributes");
            }

            return new Query<T>(new WorkItemLinqQueryProvider<T>(workItemTrackingHttpClient, null,
                GenericWorkItemHelpers<T>.Provider));
        }

        /// <summary>
        ///     Extends the project for a specific work item type LINQ Query.
        /// </summary>
        /// <typeparam name="T">Type of work item</typeparam>
        /// <param name="projectName">The project name</param>
        /// <returns></returns>
        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            string projectName) where T : GenericWorkItem, new()
        {
            if (projectName == null)
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            if (!GenericWorkItemHelpers<T>.IsValid)
            {
                throw new InvalidOperationException($"Type '{typeof(T)}' does not have a the required attributes");
            }

            return new Query<T>(new WorkItemLinqQueryProvider<T>(workItemTrackingHttpClient, projectName,
                GenericWorkItemHelpers<T>.Provider));
        }


        public static async Task CreateOrUpdateWorkItemAsync<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient, T workitem,CancellationToken cancellationToken = default)
            where T : GenericWorkItem
        {
            if (workItemTrackingHttpClient == null)
            {
                throw new ArgumentNullException(nameof(workItemTrackingHttpClient));
            }
            if (workitem == null)
            {
                throw new ArgumentNullException(nameof(workitem));
            }


            var patchDocument = workitem.CreatePatchDocument();

            Task<WorkItem> task;
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (workitem.Id.HasValue)
            {
                task = workItemTrackingHttpClient.UpdateWorkItemAsync(patchDocument, workitem.Id.Value,null,null,null,cancellationToken);
            }
            else
            {
                task = workItemTrackingHttpClient.CreateWorkItemAsync(patchDocument, workitem.Project,workitem.WorkItemType,null,null,null,cancellationToken);
            }
            var savedOrCreatedWorkItem = await task;

            workitem.CopyValuesFromWorkItem(savedOrCreatedWorkItem);

        }

        public static async Task<T> GetWorkItemAsync<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient, int id, CancellationToken cancellationToken = default) where T : GenericWorkItem, new()
        {
            if (workItemTrackingHttpClient == null)
            {
                throw new ArgumentNullException(nameof(workItemTrackingHttpClient));
            }

            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            var workItem = await workItemTrackingHttpClient.GetWorkItemAsync(id,cancellationToken:cancellationToken);

            var expectedWorkItemType = GenericWorkItemHelpers<T>.WorkItemTypeName;
            if (!string.IsNullOrWhiteSpace(expectedWorkItemType))
            {
                var type = (string)workItem.Fields[SystemField.WorkItemType];
                if (type != expectedWorkItemType)
                {
                    throw new InvalidOperationException($"Expected workitem type '{expectedWorkItemType}' but received '{type}'");
                }
            }           

            var result = new T();
            result.CopyValuesFromWorkItem(workItem);
            return result;
        }


        public static T New<T>(this ProjectInfo project, NewWorkItemOptions options = NewWorkItemOptions.Nothing)
            where T : GenericWorkItem, new()
        {
            return NewWorkItemCore<T>(project.Name, options);
        }

        public static T New<T>(this TeamProject project, NewWorkItemOptions options = NewWorkItemOptions.Nothing)
            where T : GenericWorkItem, new()
        {
            return NewWorkItemCore<T>(project.Name, options);
        }

        private static T NewWorkItemCore<T>(string projectName, NewWorkItemOptions options)
            where T : GenericWorkItem, new()
        {
            if (projectName == null)
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            var typeName = GenericWorkItemHelpers<T>.WorkItemTypeName;

            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentException($"The type {typeof(T).FullName} is not linked to an workitem type");
            }

            var result = new T
            {
                WorkItemType = typeName,
                Project = projectName
            };

            if ((options & NewWorkItemOptions.FillAreaPath) == NewWorkItemOptions.FillAreaPath)
            {
                result.Area = projectName;
            }

            if ((options & NewWorkItemOptions.FillIterationPath) == NewWorkItemOptions.FillIterationPath)
            {
                result.Iteration = projectName;
            }

            return result;
        }


#if false
        public static IQueryable<T> OfType<T, U>(this IQueryable<U> query)
            where T : U, new()
            where U : GenericWorkItem, new()
        {
            WorkItemLinqQueryProvider<U> wiQP = (query as IQueryable<U>).Provider as WorkItemLinqQueryProvider<U>;
            return new Query<T>(new WorkItemLinqQueryProvider<T>(wiQP.TPC,wiQP.Project, GenericWorkItemHelpers<T>.Provider));
        }
#endif
#if false
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


            var whereSourceTranslator = new WhereClauseTranslator(new WorkItemHelper(), "Source");

            var sourceFilter = whereSourceTranslator.Translate(sourceLambda.Body, qb, sourceLambda.Parameters[0].Name);

            if (!string.IsNullOrEmpty(sourceFilter))
            {
                qb.AddWhereClause(sourceFilter);
            }


            var whereTargetTranslator = new WhereClauseTranslator(new WorkItemHelper(), "Target");

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
#endif

        #region Type conversions

        /// <summary>
        ///     Check if a work item is of a the specified type
        /// </summary>
        /// <typeparam name="T">Workitem Type</typeparam>
        /// <param name="wi"></param>
        /// <returns></returns>
        public static bool Is<T>(this WorkItem wi) where T : GenericWorkItem, new()
        {
            if (!GenericWorkItemHelpers<T>.IsValid)
            {
                throw new InvalidOperationException($"Type '{typeof(T)}' does not have a the required attributes");
            }

            return wi.Field<string>(SystemField.WorkItemType) == GenericWorkItemHelpers<T>.WorkItemTypeName;
        }

        /// <summary>
        ///     Cast a work item.
        /// </summary>
        /// <typeparam name="T">Workitem Type</typeparam>
        /// <param name="wi">The wi.</param>
        /// <returns></returns>
        public static T ConvertIn<T>(this WorkItem wi) where T : GenericWorkItem, new()
        {
            if (!GenericWorkItemHelpers<T>.IsValid)
            {
                throw new ArgumentException($"Type '{typeof(T)}' does not have a the needed attributes");
            }

            if (!Is<T>(wi))
            {
                throw new ArgumentException(nameof(wi),
                    $"Source type is '{wi.Field<string>(SystemField.WorkItemType)}' but the target type is {GenericWorkItemHelpers<T>.WorkItemTypeName}");
            }

            var ret = new T();

            ret.CopyValuesFromWorkItem(wi);

            return ret;
        }


#if false /// <summary>
/// Checks if the specified project can used the specified Work Item Type
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="project">The project.</param>
/// <returns></returns>
        public static bool IsSupported<T>(this ProjectInfo project) where T : GenericWorkItem,new()
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            return GenericWorkItemHelpers<T>.CheckProjectUsability(project);
        }
#endif

        #endregion
    }
}