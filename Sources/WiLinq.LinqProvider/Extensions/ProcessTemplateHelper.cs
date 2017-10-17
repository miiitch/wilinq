using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WiLinq.LinqProvider.Process;

namespace WiLinq.LinqProvider.Extensions
{
    internal class ProcessTemplateHelper : CustomWorkItemResolver<GenericWorkItem>,
        ICustomWorkItemHelper<GenericWorkItem>
    {
        private readonly bool _failIfTypeUnknown;
        private readonly ProcessTemplate _processTemplate;


        public ProcessTemplateHelper(ProcessTemplate processTemplate, bool failIfTypeUnknown = false)
        {
            _processTemplate = processTemplate;
            _failIfTypeUnknown = failIfTypeUnknown;
        }

        public GenericWorkItem CreateItem(WorkItem workitem)
        {
            return _processTemplate.Convert(workitem, _failIfTypeUnknown);
        }
    }
}