using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
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
        /// <param name="workItemTrackingHttpClient">The server.</param>
        /// <returns></returns>
        public static IQueryable<WorkItem> All(this WorkItemTrackingHttpClient workItemTrackingHttpClient)
        {
            if (workItemTrackingHttpClient == null)
            {
                throw new ArgumentNullException(nameof(workItemTrackingHttpClient));
            }

            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(workItemTrackingHttpClient, null, new TFSWorkItemHelper()));
        }

        /// <summary>
        /// Extends the project for a LINQ Query on the work item store.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static IQueryable<WorkItem> All(this WorkItemTrackingHttpClient workItemTrackingHttpClient, ProjectInfo project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(workItemTrackingHttpClient, project.Name, new TFSWorkItemHelper()));
        }

        public static IQueryable<WorkItem> All(this WorkItemTrackingHttpClient workItemTrackingHttpClient, TeamProject project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            return new Query<WorkItem>(new WorkItemLinqQueryProvider<WorkItem>(workItemTrackingHttpClient, project.Name, new TFSWorkItemHelper()));
        }

        /// <summary>
        /// Extends the project for a specific work item type LINQ Query.
        /// </summary>
        /// <typeparam name="T">Type of work item</typeparam>
        /// <typeparam name="THelper">Related creator provider</typeparam>
        /// <param name="workItemTrackingHttpClient"></param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        internal static IQueryable<T> SetOf<T, THelper>(this WorkItemTrackingHttpClient workItemTrackingHttpClient, ProjectInfo project)
            where T : WorkItemBase
            where THelper : ICustomWorkItemHelper<T>, new()
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            return new Query<T>(new WorkItemLinqQueryProvider<T>(workItemTrackingHttpClient, project.Name, new THelper()));
        }


        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            ProjectInfo projectInfo) where T : WorkItemBase, new()
        {
            return SetOf<T>(workItemTrackingHttpClient, projectInfo.Name);
        }

        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient,
            TeamProject teamProject) where T : WorkItemBase, new()
        {
            return SetOf<T>(workItemTrackingHttpClient, teamProject.Name);
        }

        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient) where T : WorkItemBase, new()
        {
            if (!WorkItemPropertyUtility<T>.IsValid)
            {
                throw new InvalidOperationException($"Type '{typeof(T)}' does not have a the required attributes");
            }

            return new Query<T>(new WorkItemLinqQueryProvider<T>(workItemTrackingHttpClient, null, WorkItemPropertyUtility<T>.Provider));
        }

        /// <summary>
        /// Extends the project for a specific work item type LINQ Query.
        /// </summary>
        /// <typeparam name="T">Type of work item</typeparam>
        /// <param name="projectName">The project name</param>
        /// <returns></returns>
        public static IQueryable<T> SetOf<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient, string projectName) where T : WorkItemBase, new()
        {
            if (projectName == null)
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            if (!WorkItemPropertyUtility<T>.IsValid)
            {
                throw new InvalidOperationException($"Type '{typeof(T)}' does not have a the required attributes");
            }

            return new Query<T>(new WorkItemLinqQueryProvider<T>(workItemTrackingHttpClient, projectName, WorkItemPropertyUtility<T>.Provider));
        }


        public static async Task Save<T>(this WorkItemTrackingHttpClient workItemTrackingHttpClient, T workitem)
            where T : WorkItemBase
        {
            var patchDocument = workitem.CreatePatchDocument();
            try
            {
                Task<WorkItem> task;
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (workitem.Id.HasValue)
                {
                    
                    task = workItemTrackingHttpClient.UpdateWorkItemAsync(patchDocument, workitem.Id.Value);
                }
                else
                {
                    task = workItemTrackingHttpClient.CreateWorkItemAsync(patchDocument, workitem.Project,
                        workitem.WorkItemType);
                }
                var savedOrCreatedWorkItem = await task;

                workitem.CopyValuesFromWorkItem(savedOrCreatedWorkItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

    
        public static T New<T>(this ProjectInfo project, NewWorkItemOptions options = NewWorkItemOptions.Nothing) where T : WorkItemBase, new()
        {
            return NewWorkItemCore<T>(project.Name, options);
        }

        public static T New<T>(this TeamProject project, NewWorkItemOptions options = NewWorkItemOptions.Nothing) where T : WorkItemBase, new()
        {
            return NewWorkItemCore<T>(project.Name,options);
        }

        private static T NewWorkItemCore<T>(string projectName, NewWorkItemOptions options) where T : WorkItemBase, new()
        {
            if (projectName == null)
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            var typeName = WorkItemPropertyUtility<T>.WorkItemTypeName;

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
            where U : WorkItemBase, new()
        {
            WorkItemLinqQueryProvider<U> wiQP = (query as IQueryable<U>).Provider as WorkItemLinqQueryProvider<U>;
            return new Query<T>(new WorkItemLinqQueryProvider<T>(wiQP.TPC,wiQP.Project, WorkItemPropertyUtility<T>.Provider));
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
#endif

        #region Type conversions

        /// <summary>
        /// Check if a work item is of a the specified type
        /// </summary>
        /// <typeparam name="T">Workitem Type</typeparam>
        /// <param name="wi"></param>
        /// <returns></returns>
        public static bool Is<T>(this WorkItem wi) where T : WorkItemBase, new()
        {
            if (!WorkItemPropertyUtility<T>.IsValid)
            {
                throw new InvalidOperationException($"Type '{typeof(T)}' does not have a the required attributes");
            }

            return wi.Field<string>(SystemField.WorkItemType) == WorkItemPropertyUtility<T>.WorkItemTypeName;
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
                throw new InvalidOperationException($"Type '{typeof(T)}' does not have a the needed attributes");
            }

            var ret = new T();

            ret.CopyValuesFromWorkItem(wi);

            return ret;
        }


#if false
        /// <summary>
        /// Checks if the specified project can used the specified Work Item Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static bool IsSupported<T>(this ProjectInfo project) where T : WorkItemBase,new()
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            return WorkItemPropertyUtility<T>.CheckProjectUsability(project);
        }
#endif
        #endregion

        /// <summary>
        /// Returns a typed workitem for the specific project
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="project"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Get<T>(this ProjectInfo project, int id) where T : WorkItemBase, new()
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            throw new NotImplementedException();
#if false
            if (!project.IsSupported<T>())
            {
                throw new ArgumentException($"{typeof(T).FullName} is not supported in {project.Name}",nameof(project));
            }

            var wi = project.Store.GetWorkItem(id);

            if (wi == null)
            {
                return null;
            }

            if (wi.Project.Name != project.Name)
            {
                throw new ArgumentException($"Workitem #{id} is not in project '{project.Name}'",nameof(id));
            }

            if (!wi.Is<T>())
            {
                throw new ArgumentException($"Workitem #{id} is of type '{typeof(T).FullName}'", nameof(id));
            }

            var result = new T()
            {
                WorkItem = wi
            };
            return result;
#endif
        }


    }

    [Flags]
    public enum NewWorkItemOptions
    {
        Nothing = 0,
        FillAreaPath = 0b1,
        FillIterationPath = 0b10,
        FillAreaAndItertionPath = FillAreaPath | FillIterationPath

    }
}
