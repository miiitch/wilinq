using WiLinq.LinqProvider.Extensions;

namespace WiLinq.Tests
{
    [WorkItemType("Bug")]
    public class Bug : WorkItemBase
    {
        public Bug()
        {

        }
    }


    [WiLinq.LinqProvider.Extensions.WorkItemTypeAttribute("Task")]
    public partial class Task : WiLinq.LinqProvider.Extensions.WorkItemBase
    {

        /// <summary>Type of work involved</summary>
        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.Common.Activity")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.AreaPath")]
        public virtual string AreaPath
        {
            get
            {
                return this.GetRefField<string>("System.AreaPath");
            }
        }

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.AuthorizedAs")]
        public virtual string AuthorizedAs
        {
            get
            {
                return this.GetRefField<string>("System.AuthorizedAs");
            }
        }

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.AuthorizedDate")]
        public virtual System.Nullable<System.DateTime> AuthorizedDate
        {
            get
            {
                return this.GetStructField<System.DateTime>("System.AuthorizedDate");
            }
        }

        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.Common.BacklogPriority")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.CMMI.Blocked")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.BoardColumn")]
        public virtual string BoardColumn
        {
            get
            {
                return this.GetRefField<string>("System.BoardColumn");
            }
        }

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.BoardColumnDone")]
        public virtual System.Nullable<bool> BoardColumnDone
        {
            get
            {
                return this.GetStructField<bool>("System.BoardColumnDone");
            }
        }

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.BoardLane")]
        public virtual string BoardLane
        {
            get
            {
                return this.GetRefField<string>("System.BoardLane");
            }
        }

        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.Common.ClosedDate")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.History")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.Build.IntegrationBuild")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.IterationPath")]
        public virtual string IterationPath
        {
            get
            {
                return this.GetRefField<string>("System.IterationPath");
            }
        }

        /// <summary>Importance to business</summary>
        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.Common.Priority")]
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
        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.Scheduling.RemainingWork")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.RevisedDate")]
        public virtual System.Nullable<System.DateTime> RevisedDate
        {
            get
            {
                return this.GetStructField<System.DateTime>("System.RevisedDate");
            }
        }

        [WiLinq.LinqProvider.Extensions.FieldAttribute("Microsoft.VSTS.Common.StateChangeDate")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.Tags")]
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

        [WiLinq.LinqProvider.Extensions.FieldAttribute("System.Watermark")]
        public virtual System.Nullable<int> Watermark
        {
            get
            {
                return this.GetStructField<int>("System.Watermark");
            }
        }
    }
}