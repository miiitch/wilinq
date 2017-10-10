using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Scrum
{
    [WorkItemType("Task")]
    public class Task : GenericWorkItem
    {

        /// <summary>Type of work involved</summary>
        [Field("Microsoft.VSTS.Common.Activity")]
        public virtual string Activity
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.Activity");
            set => SetRefField("Microsoft.VSTS.Common.Activity", value);
        }

        [Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        [Field("Microsoft.VSTS.Common.BacklogPriority")]
        public virtual double? BacklogPriority
        {
            get => GetStructField<double>("Microsoft.VSTS.Common.BacklogPriority");
            set => SetStructField("Microsoft.VSTS.Common.BacklogPriority", value);
        }

        [Field("Microsoft.VSTS.CMMI.Blocked")]
        public virtual string Blocked
        {
            get => GetRefField<string>("Microsoft.VSTS.CMMI.Blocked");
            set => SetRefField("Microsoft.VSTS.CMMI.Blocked", value);
        }

        [Field("Microsoft.VSTS.Common.ClosedDate")]
        public virtual DateTime? ClosedDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ClosedDate");
            set => SetStructField("Microsoft.VSTS.Common.ClosedDate", value);
        }

        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        /// <summary>Importance to business</summary>
        [Field("Microsoft.VSTS.Common.Priority")]
        public virtual long? Priority
        {
            get => GetStructField<long>("Microsoft.VSTS.Common.Priority");
            set => SetStructField("Microsoft.VSTS.Common.Priority", value);
        }

        /// <summary>An estimate of the number of units of work remaining to complete this task</summary>
        [Field("Microsoft.VSTS.Scheduling.RemainingWork")]
        public virtual double? RemainingWork
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.RemainingWork");
            set => SetStructField("Microsoft.VSTS.Scheduling.RemainingWork", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }
    }
}