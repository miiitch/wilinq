using System;

namespace WiLinq.LinqProvider.Extensions
{


    [IgnoreField(SystemField.AreaId)]
    [IgnoreField(SystemField.IterationId)]
    [IgnoreField(SystemField.RelatedLinkCount)]
    [IgnoreField(SystemField.AttachedFileCount)]
    [IgnoreField(SystemField.ExternalLinkCount)]
    [IgnoreField(SystemField.HyperLinkCount)]
    [IgnoreField(SystemField.AttachedFileCount)]
    [IgnoreField(SystemField.NodeName)]
    public abstract class WorkItemBase
    {
        private WorkItem _workItem;

        public WorkItem WorkItem
        {
            get
            {
                if (_workItem == null)
                {
                    throw new InvalidOperationException("Workitem not assigned");
                }
                return _workItem;
            }
            set
            {
                if (_workItem != null)
                {
                    throw new InvalidOperationException("Workitem already assigned");
                }
                _workItem = value;
            }
        }

        [Field(SystemField.Id)]
        public int Id => _workItem.Id;

        [Field(SystemField.AssignedTo)]
        public string AssignedTo
        {
            get => _workItem.Field<string>(SystemField.AssignedTo);
            set => _workItem.SetField(SystemField.AssignedTo, value);
        }

        [Field(SystemField.Title)]
        public string Title
        {
            get => WorkItem.Title;
            set => WorkItem.Title = value;
        }

        [Field(SystemField.ChangedDate)]
        public DateTime ChangedDate => WorkItem.ChangedDate;

        /// <summary>
        /// Gets the created by value of the work item.
        /// </summary>
        /// <value>The created by.</value>
        [Field(SystemField.CreatedBy)]
        public string CreatedBy => WorkItem.CreatedBy;

        /// <summary>
        /// Gets the changed by value of the work item.
        /// </summary>
        /// <value>The changed by.</value>
        [Field(SystemField.ChangedBy)]
        public string ChangedBy => WorkItem.ChangedBy;

        /// <summary>
        /// Gets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [Field(SystemField.CreatedDate)]
        public DateTime CreatedDate => WorkItem.CreatedDate;


        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [Field(SystemField.Project)]
        public Project Project => WorkItem.Project;

        [Field(SystemField.Description)]
        public string Description
        {
            get => WorkItem.Description;
            set => WorkItem.Description = value;
        }

        /// <summary>
        /// Gets the history of the work item.
        /// </summary>
        /// <value>The history.</value>
        [Field(SystemField.History)]
        public string History
        {
            get => WorkItem.History;
            set => WorkItem.History = value;
        }

        [Field(SystemField.Reason)]
        public string Reason
        {
            get => WorkItem.Reason;
            set => WorkItem.Reason = value;
        }

        [Field(SystemField.WorkItemType)]
        public string Type => WorkItem.Type.Name;

        [Field(SystemField.State)]
        public string State
        {
            get => WorkItem.State;
             set => WorkItem.State = value;
         }

         [Field(SystemField.Revision)]
         public int Revision => WorkItem.Rev;

        [Field(SystemField.AreaPath)]
         public string Area => WorkItem.AreaPath;

        [Field(SystemField.IterationPath)]
         public string Iteration => WorkItem.IterationPath;

        public bool IsUnderArea(string areaPath)
         {
             if (string.IsNullOrEmpty(areaPath))
                 throw new ArgumentException("areaPath is null or empty.", nameof(areaPath));

             return areaPath.Contains(Area);
         }

         public bool IsUnderIteration(string iterationPath)
         {
             if (string.IsNullOrEmpty(iterationPath))
                 throw new ArgumentException("iterationPath is null or empty.", nameof(iterationPath));

             return iterationPath.Contains(Iteration);
         }
    }
}