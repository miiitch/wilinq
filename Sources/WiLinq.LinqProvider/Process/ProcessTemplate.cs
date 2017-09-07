using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider.Process
{
    
    public class ProcessTemplate
    {
        private readonly Dictionary<string, Type> _workitemTypes;

        public ProcessTemplate()
        {
            _workitemTypes = new Dictionary<string, Type>();
        }

        public void AddWorkItemType<T>() where T : GenericWorkItem, new()
        {
            var workItemType = WorkItemPropertyUtility<T>.WorkItemTypeName;

            if (string.IsNullOrWhiteSpace(workItemType))
            {
                throw new ArgumentException($"Type '{typeof(T)}' does not have a the needed attributes");
            }

            if (_workitemTypes.ContainsKey(workItemType))
            {
                throw new ArgumentException($"Type '{workItemType}' alread registrated");
            }

            _workitemTypes.Add(workItemType,typeof(T));
        }

        internal GenericWorkItem Convert(WorkItem workItem, bool failIfTypeUnknown = false)
        {
            var type = workItem.Field<string>(SystemField.WorkItemType);

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Workitem type missing");
            }

            GenericWorkItem result;

            if (_workitemTypes.TryGetValue(type, out var typeInfo))
            {
                var constructorInfo = typeInfo.GetConstructor(new Type[0]);
                if (constructorInfo == null)
                {
                    throw new InvalidOperationException($"Missing default constructor on type '{typeInfo.FullName}'");
                }
                result = (GenericWorkItem) constructorInfo.Invoke(null);
            }
            else
            {
                result = new GenericWorkItem();
            }

            result.CopyValuesFromWorkItem(workItem);
            return result;
        }



    }


     
}
