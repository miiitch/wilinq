using System;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

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
        public int Id
        {
            get
            {
                return _workItem.Id;
            }
        }

        [Field(SystemField.AssignedTo)]
        public string AssignedTo
        {
            get
            {
                return _workItem.Field<string>(SystemField.AssignedTo);
            }
            set 
            {
                _workItem.SetField(SystemField.AssignedTo, value);
            }
        }

        [Field(SystemField.Title)]
        public string Title
        {
            get
            {
                return WorkItem.Title;
            }
            set
            {
                WorkItem.Title = value;
            }
        }

        [Field(SystemField.ChangedDate)]
        public DateTime ChangedDate
        {
            get
            {
                return WorkItem.ChangedDate;
            }
        }

        /// <summary>
        /// Gets the created by value of the work item.
        /// </summary>
        /// <value>The created by.</value>
        [Field(SystemField.CreatedBy)]
        public string CreatedBy
        {
            get
            {
                return WorkItem.CreatedBy;
            }
        }

        /// <summary>
        /// Gets the changed by value of the work item.
        /// </summary>
        /// <value>The changed by.</value>
        [Field(SystemField.ChangedBy)]
        public string ChangedBy
        {
            get
            {
                return WorkItem.ChangedBy;
            }
        }

        /// <summary>
        /// Gets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [Field(SystemField.CreatedDate)]
        public DateTime CreatedDate
        {
            get
            {
                return WorkItem.CreatedDate;
            }
        }
     
    
        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [Field(SystemField.Project)]
        public Project Project
        {
            get
            {
                return WorkItem.Project;
            }
        }

        [Field(SystemField.Description)]
        public string Description
        {
            get
            {
                return WorkItem.Description;
            }
            set
            {
                WorkItem.Description = value;
            }
        }

        /// <summary>
        /// Gets the history of the work item.
        /// </summary>
        /// <value>The history.</value>
        [Field(SystemField.History)]
        public string History
        {
            get
            {
                return WorkItem.History;
            }
            set
            {
                WorkItem.History = value;
            }
        }

        [Field(SystemField.Reason)]
        public string Reason
        {
            get
            {
                return WorkItem.Reason;
            }
            set
            {
                WorkItem.Reason = value;
            }
        }

        [Field(SystemField.WorkItemType)]
        public string Type
        {
            get
            {
                return WorkItem.Type.Name;
            }
        }

         [Field(SystemField.State)]
        public string State
        {
            get
            {
                return WorkItem.State;
            }
            set
            {
                WorkItem.State = value;
            }
        }

         [Field(SystemField.Revision)]
         public int Revision
         {
             get
             {
                 return WorkItem.Rev;
             }
         }

         [Field(SystemField.AreaPath)]
         public string Area
         {
             get
             {
                 return WorkItem.AreaPath;
             }
         }

         [Field(SystemField.IterationPath)]
         public string Iteration
         {
             get
             {                 
                 return WorkItem.IterationPath;
             }
         }

         public bool IsUnderArea(string areaPath)
         {
             if (String.IsNullOrEmpty(areaPath))
                 throw new ArgumentException("areaPath is null or empty.", "areaPath");

             return areaPath.Contains(Area);
         }

         public bool IsUnderIteration(string iterationPath)
         {
             if (String.IsNullOrEmpty(iterationPath))
                 throw new ArgumentException("iterationPath is null or empty.", "iterationPath");

             return iterationPath.Contains(Iteration);
         }
    }
}