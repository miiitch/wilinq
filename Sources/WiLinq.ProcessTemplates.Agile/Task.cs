using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Task")]
    public class Task : GenericWorkItem
    {

        [Field("Microsoft.VSTS.Common.ActivatedBy")]
        public virtual string ActivatedBy
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ActivatedBy");
            set => SetRefField("Microsoft.VSTS.Common.ActivatedBy", value);
        }

        [Field("Microsoft.VSTS.Common.ActivatedDate")]
        public virtual DateTime? ActivatedDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ActivatedDate");
            set => SetStructField("Microsoft.VSTS.Common.ActivatedDate", value);
        }

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

        [Field("Microsoft.VSTS.Common.ClosedBy")]
        public virtual string ClosedBy
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ClosedBy");
            set => SetRefField("Microsoft.VSTS.Common.ClosedBy", value);
        }

        [Field("Microsoft.VSTS.Common.ClosedDate")]
        public virtual DateTime? ClosedDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ClosedDate");
            set => SetStructField("Microsoft.VSTS.Common.ClosedDate", value);
        }

        /// <summary>The number of units of work that have been spent on this task</summary>
        [Field("Microsoft.VSTS.Scheduling.CompletedWork")]
        public virtual double? CompletedWork
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.CompletedWork");
            set => SetStructField("Microsoft.VSTS.Scheduling.CompletedWork", value);
        }

        /// <summary>The date to finish the task</summary>
        [Field("Microsoft.VSTS.Scheduling.FinishDate")]
        public virtual DateTime? FinishDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.FinishDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.FinishDate", value);
        }

        /// <summary>The build in which the bug was fixed</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        /// <summary>Initial value for Remaining Work - set once, when work begins</summary>
        [Field("Microsoft.VSTS.Scheduling.OriginalEstimate")]
        public virtual double? OriginalEstimate
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.OriginalEstimate");
            set => SetStructField("Microsoft.VSTS.Scheduling.OriginalEstimate", value);
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

        /// <summary>Work first on items with lower-valued stack rank. Set in triage.</summary>
        [Field("Microsoft.VSTS.Common.StackRank")]
        public virtual double? StackRank
        {
            get => GetStructField<double>("Microsoft.VSTS.Common.StackRank");
            set => SetStructField("Microsoft.VSTS.Common.StackRank", value);
        }

        /// <summary>The date to start the task</summary>
        [Field("Microsoft.VSTS.Scheduling.StartDate")]
        public virtual DateTime? StartDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.StartDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.StartDate", value);
        }

        [Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }
    }
}