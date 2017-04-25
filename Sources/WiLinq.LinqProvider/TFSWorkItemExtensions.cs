using System;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider
{
    // ReSharper disable once InconsistentNaming
    public static class TFSWorkItemExtensions
    {

        public static T Field<T>(this WorkItem wi, string referenceName)
        {
            return (T)(wi.Fields[referenceName]);
        }


        public static void SetField<T>(this WorkItem wi, string referenceName, T value)
        {
            if (referenceName == "System.Id")
            {
                throw new ArgumentException("Can't set Id field");
            }
            wi.Fields[referenceName] = value;
        }
    }
}
