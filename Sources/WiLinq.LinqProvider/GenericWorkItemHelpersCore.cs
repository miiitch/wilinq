using System;

namespace WiLinq.LinqProvider
{
    internal static class GenericWorkItemHelpersCore
    {
        public static string GetWorkItemType(Type workItemType)
        {

            if (!(workItemType.GetCustomAttributes(typeof(WorkItemTypeAttribute), false) is WorkItemTypeAttribute[]
                    modelAttribs) || modelAttribs.Length != 1)
            {
                return null;
            }
            else
            {
                return  modelAttribs[0].TypeName;
            }
        }

        public static string GetWorkItemType<T>() where T: GenericWorkItem
        {
            return GetWorkItemType(typeof(T));
        }
    }
}