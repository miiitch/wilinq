using WiLinq.LinqProvider;

namespace WiLinq.Tests
{
    [WorkItemType("Bug")]
    public class Bug : GenericWorkItem
    {
        [Field("Microsoft.VSTS.Common.Priority")]
        public virtual int? Priority
        {
            get => GetStructField<int>("Microsoft.VSTS.Common.Priority");
            set => SetStructField("Microsoft.VSTS.Common.Priority", value);
        }
   
    }


    [WorkItemType("Task")]
    public partial class ScrumTask : GenericWorkItem
    {

        /// <summary>Type of work involved</summary>
        [Field("Microsoft.VSTS.Common.Activity")]
        public virtual string Activity
        {
            get
            {
                return this.GetRefField<string>("Microsoft.VSTS.Common.Activity");
            }
            set
            {
                this.SetRefField<string>("Microsoft.VSTS.Common.Activity", value);
            }
        }

        [Field("System.AreaPath")]
        public virtual string AreaPath
        {
            get
            {
                return this.GetRefField<string>("System.AreaPath");
            }
        }

        [Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs
        {
            get
            {
                return this.GetRefField<string>("System.AuthorizedAs");
            }
        }

        [Field("System.AuthorizedDate")]
        public virtual System.Nullable<System.DateTime> AuthorizedDate
        {
            get
            {
                return this.GetStructField<System.DateTime>("System.AuthorizedDate");
            }
        }

        [Field("Microsoft.VSTS.Common.BacklogPriority")]
        public virtual System.Nullable<double> BacklogPriority
        {
            get
            {
                return this.GetStructField<double>("Microsoft.VSTS.Common.BacklogPriority");
            }
            set
            {
                this.SetStructField<double>("Microsoft.VSTS.Common.BacklogPriority", value);
            }
        }

        [Field("Microsoft.VSTS.CMMI.Blocked")]
        public virtual string Blocked
        {
            get
            {
                return this.GetRefField<string>("Microsoft.VSTS.CMMI.Blocked");
            }
            set
            {
                this.SetRefField<string>("Microsoft.VSTS.CMMI.Blocked", value);
            }
        }

        [Field("System.BoardColumn")]
        public virtual string BoardColumn
        {
            get
            {
                return this.GetRefField<string>("System.BoardColumn");
            }
        }

        [Field("System.BoardColumnDone")]
        public virtual System.Nullable<bool> BoardColumnDone
        {
            get
            {
                return this.GetStructField<bool>("System.BoardColumnDone");
            }
        }

        [Field("System.BoardLane")]
        public virtual string BoardLane
        {
            get
            {
                return this.GetRefField<string>("System.BoardLane");
            }
        }

        [Field("Microsoft.VSTS.Common.ClosedDate")]
        public virtual System.Nullable<System.DateTime> ClosedDate
        {
            get
            {
                return this.GetStructField<System.DateTime>("Microsoft.VSTS.Common.ClosedDate");
            }
            set
            {
                this.SetStructField<System.DateTime>("Microsoft.VSTS.Common.ClosedDate", value);
            }
        }

        [Field("System.History")]
        public virtual string History
        {
            get
            {
                return this.GetRefField<string>("System.History");
            }
            set
            {
                this.SetRefField<string>("System.History", value);
            }
        }

        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get
            {
                return this.GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            }
            set
            {
                this.SetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild", value);
            }
        }

        [Field("System.IterationPath")]
        public virtual string IterationPath
        {
            get
            {
                return this.GetRefField<string>("System.IterationPath");
            }
        }

        /// <summary>Importance to business</summary>
        [Field("Microsoft.VSTS.Common.Priority")]
        public virtual System.Nullable<int> Priority
        {
            get
            {
                return this.GetStructField<int>("Microsoft.VSTS.Common.Priority");
            }
            set
            {
                this.SetStructField<int>("Microsoft.VSTS.Common.Priority", value);
            }
        }

        /// <summary>An estimate of the number of units of work remaining to complete this task</summary>
        [Field("Microsoft.VSTS.Scheduling.RemainingWork")]
        public virtual System.Nullable<double> RemainingWork
        {
            get
            {
                return this.GetStructField<double>("Microsoft.VSTS.Scheduling.RemainingWork");
            }
            set
            {
                this.SetStructField<double>("Microsoft.VSTS.Scheduling.RemainingWork", value);
            }
        }

        [Field("System.RevisedDate")]
        public virtual System.Nullable<System.DateTime> RevisedDate
        {
            get
            {
                return this.GetStructField<System.DateTime>("System.RevisedDate");
            }
        }

        [Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual System.Nullable<System.DateTime> StateChangeDate
        {
            get
            {
                return this.GetStructField<System.DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            }
            set
            {
                this.SetStructField<System.DateTime>("Microsoft.VSTS.Common.StateChangeDate", value);
            }
        }

        [Field("System.Tags")]
        public virtual string Tags
        {
            get
            {
                return this.GetRefField<string>("System.Tags");
            }
            set
            {
                this.SetRefField<string>("System.Tags", value);
            }
        }

        [Field("System.Watermark")]
        public virtual System.Nullable<int> Watermark
        {
            get
            {
                return this.GetStructField<int>("System.Watermark");
            }
        }
    }
}