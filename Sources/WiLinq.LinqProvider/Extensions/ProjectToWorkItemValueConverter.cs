using System;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider.Extensions
{
    internal sealed class ProjectToWorkItemValueConverter : WorkItemValueConverter
    {
        public override Type RelatedType
        {
            get { return typeof(Project); }
        }

        public override object Resolve(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "obj is null.");
            }
            var project = obj as Project;
            if (project == null)
            {
                throw new ArgumentException("obj is not a Project", "obj");
            }

            return project.Name;
        }

    }
}
