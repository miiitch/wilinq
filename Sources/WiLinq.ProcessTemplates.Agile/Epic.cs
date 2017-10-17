using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Epic")]
    public class Epic : GenericWorkItem
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

        [Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        /// <summary>The business value for the customer when the epic is released</summary>
        [Field("Microsoft.VSTS.Common.BusinessValue")]
        public virtual long? BusinessValue
        {
            get => GetStructField<long>("Microsoft.VSTS.Common.BusinessValue");
            set => SetStructField("Microsoft.VSTS.Common.BusinessValue", value);
        }

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

        /// <summary>The estimated effort to implemented the epic</summary>
        [Field("Microsoft.VSTS.Scheduling.Effort")]
        public virtual double? Effort
        {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.Effort");
            set => SetStructField("Microsoft.VSTS.Scheduling.Effort", value);
        }

        /// <summary>The build in which the epic was fixed</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        /// <summary>Priority for completing the epic, based on business goals</summary>
        [Field("Microsoft.VSTS.Common.Priority")]
        public virtual long? Priority
        {
            get => GetStructField<long>("Microsoft.VSTS.Common.Priority");
            set => SetStructField("Microsoft.VSTS.Common.Priority", value);
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

        [Field("Microsoft.VSTS.Common.ResolvedReason")]
        public virtual string ResolvedReason
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ResolvedReason");
            set => SetRefField("Microsoft.VSTS.Common.ResolvedReason", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>Uncertainty in epic</summary>
        [Field("Microsoft.VSTS.Common.Risk")]
        public virtual string Risk
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.Risk");
            set => SetRefField("Microsoft.VSTS.Common.Risk", value);
        }

        /// <summary>Work first on items with lower-valued stack rank.</summary>
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

        /// <summary>The target date for completing the epic</summary>
        [Field("Microsoft.VSTS.Scheduling.TargetDate")]
        public virtual DateTime? TargetDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.TargetDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.TargetDate", value);
        }

        /// <summary>How does the business value decay over time. Higher values make the epic more time critical</summary>
        [Field("Microsoft.VSTS.Common.TimeCriticality")]
        public virtual double? TimeCriticality
        {
            get => GetStructField<double>("Microsoft.VSTS.Common.TimeCriticality");
            set => SetStructField("Microsoft.VSTS.Common.TimeCriticality", value);
        }

        /// <summary>
        ///     Business = Customer-facing epics; Architectural = Technology initiatives to support current and future
        ///     business needs
        /// </summary>
        [Field("Microsoft.VSTS.Common.ValueArea")]
        public virtual string ValueArea
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ValueArea");
            set => SetRefField("Microsoft.VSTS.Common.ValueArea", value);
        }
    }
}