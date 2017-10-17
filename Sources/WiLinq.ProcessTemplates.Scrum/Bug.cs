using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Scrum
{
    [WorkItemType("Bug")]
    public class Bug : GenericWorkItem
    {
        [Field("Microsoft.VSTS.Common.AcceptanceCriteria")]
        public virtual string AcceptanceCriteria
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.AcceptanceCriteria");
            set => SetRefField("Microsoft.VSTS.Common.AcceptanceCriteria", value);
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

        [Field("Microsoft.VSTS.Common.BacklogPriority")]
        public virtual double? BacklogPriority
        {
            get => GetStructField<double>("Microsoft.VSTS.Common.BacklogPriority");
            set => SetStructField("Microsoft.VSTS.Common.BacklogPriority", value);
        }

        [Field("Microsoft.VSTS.Common.ClosedDate")]
        public virtual DateTime? ClosedDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ClosedDate");
            set => SetStructField("Microsoft.VSTS.Common.ClosedDate", value);
        }

        /// <summary>The estimated effort to implemented the bug</summary>
        [Field("Microsoft.VSTS.Scheduling.Effort")]
        public virtual double? Effort
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.Effort");
            set => SetStructField("Microsoft.VSTS.Scheduling.Effort", value);
        }

        [Field("Microsoft.VSTS.Build.FoundIn")]
        public virtual string FoundIn
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.FoundIn");
            set => SetRefField("Microsoft.VSTS.Build.FoundIn", value);
        }

        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
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

        [Field("Microsoft.VSTS.TCM.ReproSteps")]
        public virtual string ReproSteps
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.ReproSteps");
            set => SetRefField("Microsoft.VSTS.TCM.ReproSteps", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [Field("Microsoft.VSTS.Common.Severity")]
        public virtual string Severity
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.Severity");
            set => SetRefField("Microsoft.VSTS.Common.Severity", value);
        }

        [Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }

        [Field("Microsoft.VSTS.TCM.SystemInfo")]
        public virtual string SystemInfo
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.SystemInfo");
            set => SetRefField("Microsoft.VSTS.TCM.SystemInfo", value);
        }

        /// <summary>
        ///     The type should be set to Business primarily to represent customer-facing issues. Work to change the
        ///     architecture should be added as a Product Backlog Item
        /// </summary>
        [Field("Microsoft.VSTS.Common.ValueArea")]
        public virtual string ValueArea
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ValueArea");
            set => SetRefField("Microsoft.VSTS.Common.ValueArea", value);
        }
    }
}