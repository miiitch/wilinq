using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
  

    [WorkItemType("Bug")]
    public class Bug : GenericWorkItem
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

        /// <summary>The number of units of work that have been spent on this bug</summary>
        [Field("Microsoft.VSTS.Scheduling.CompletedWork")]
        public virtual double? CompletedWork
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.CompletedWork");
            set => SetStructField("Microsoft.VSTS.Scheduling.CompletedWork", value);
        }

        /// <summary>The build in which the bug was found</summary>
        [Field("Microsoft.VSTS.Build.FoundIn")]
        public virtual string FoundIn
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.FoundIn");
            set => SetRefField("Microsoft.VSTS.Build.FoundIn", value);
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

        /// <summary>Business importance. 1=must fix; 4=unimportant.</summary>
        [Field("Microsoft.VSTS.Common.Priority")]
        public virtual long? Priority
        {
            get => GetStructField<long>("Microsoft.VSTS.Common.Priority");
            set => SetStructField("Microsoft.VSTS.Common.Priority", value);
        }

        /// <summary>An estimate of the number of units of work remaining to complete this bug</summary>
        [Field("Microsoft.VSTS.Scheduling.RemainingWork")]
        public virtual double? RemainingWork
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.RemainingWork");
            set => SetStructField("Microsoft.VSTS.Scheduling.RemainingWork", value);
        }

        /// <summary>How to see the bug. End by contrasting expected with actual behavior.</summary>
        [Field("Microsoft.VSTS.TCM.ReproSteps")]
        public virtual string ReproSteps
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.ReproSteps");
            set => SetRefField("Microsoft.VSTS.TCM.ReproSteps", value);
        }

        [Field("Microsoft.VSTS.Common.ResolvedBy")]
        public virtual string ResolvedBy
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ResolvedBy");
            set => SetRefField("Microsoft.VSTS.Common.ResolvedBy", value);
        }

        [Field("Microsoft.VSTS.Common.ResolvedDate")]
        public virtual DateTime? ResolvedDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ResolvedDate");
            set => SetStructField("Microsoft.VSTS.Common.ResolvedDate", value);
        }

        /// <summary>The reason why the bug was resolved</summary>
        [Field("Microsoft.VSTS.Common.ResolvedReason")]
        public virtual string ResolvedReason
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ResolvedReason");
            set => SetRefField("Microsoft.VSTS.Common.ResolvedReason", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>Assessment of the effect of the bug on the project</summary>
        [Field("Microsoft.VSTS.Common.Severity")]
        public virtual string Severity
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.Severity");
            set => SetRefField("Microsoft.VSTS.Common.Severity", value);
        }

        /// <summary>Work first on items with lower-valued stack rank. Set in triage.</summary>
        [Field("Microsoft.VSTS.Common.StackRank")]
        public virtual double? StackRank
        {
            get => GetStructField<double>("Microsoft.VSTS.Common.StackRank");
            set => SetStructField("Microsoft.VSTS.Common.StackRank", value);
        }

        [Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }

        /// <summary>The size of work estimated for fixing the bug</summary>
        [Field("Microsoft.VSTS.Scheduling.StoryPoints")]
        public virtual double? StoryPoints
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.StoryPoints");
            set => SetStructField("Microsoft.VSTS.Scheduling.StoryPoints", value);
        }

        /// <summary>Test context, provided automatically by test infrastructure</summary>
        [Field("Microsoft.VSTS.TCM.SystemInfo")]
        public virtual string SystemInfo
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.SystemInfo");
            set => SetRefField("Microsoft.VSTS.TCM.SystemInfo", value);
        }

        /// <summary>The type should be set to Business primarily to represent customer-facing issues. Work to change the architecture should be added as a User Story</summary>
        [Field("Microsoft.VSTS.Common.ValueArea")]
        public virtual string ValueArea
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ValueArea");
            set => SetRefField("Microsoft.VSTS.Common.ValueArea", value);
        }
    }
}
