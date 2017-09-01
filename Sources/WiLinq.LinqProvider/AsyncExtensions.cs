using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.LinqProvider
{
    public static class AsyncExtensions
    {
        /// <summary>
        /// Returns a list of workitems asynchronously
        /// </summary>
        /// <typeparam name="T">Type of workitem, inherits from GenericWorkItem</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query) where T : GenericWorkItem
        {
            if (!(query is Query<T> workItemQuery))
            {
                throw new ArgumentException("invalid linq provider", nameof(query));
            }

            var enumerable = await workItemQuery.ExecuteAsync();

            if (enumerable is List<T> list)
            {
                return list;
            }

            return enumerable.ToList();
        }

        /// <summary>
        /// Returns a list of workitems asynchronously
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<List<WorkItem>> ToListAsync(this IQueryable<WorkItem> query)
        {
            if (!(query is Query<WorkItem> workItemQuery))
            {
                throw new ArgumentException("invalid linq provider",nameof(query));
            }
            var enumerable = await workItemQuery.ExecuteAsync();

            if (enumerable is List<WorkItem> list)
            {
                return list;
            }

            return enumerable.ToList();
        }

        /// <summary>
        /// Return the ids of the queries workitems asynchonously
        /// </summary>
        /// <typeparam name="T">Type of workitem, inherits from GenericWorkItem</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<List<int>> ToIdListAsync<T>(this IQueryable<T> query) where T : GenericWorkItem
        {
            if (!(query is Query<T> workItemQuery))
            {
                throw new ArgumentException("invalid linq provider", nameof(query));
            }

            var result = await workItemQuery.ToIdListAsync();

            return result;
        }

        /// <summary>
        /// Return the ids of the queries workitems asynchonously
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<List<int>> ToIdListAsync(this IQueryable<WorkItem> query)
        {
            if (!(query is Query<WorkItem> workItemQuery))
            {
                throw new ArgumentException("invalid linq provider", nameof(query));
            }
            var result = await workItemQuery.ExecuteAndGetIdsAsync();

            return result;
        }
    }
}