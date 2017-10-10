using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Issue")]
    public class Issue : GenericWorkItem
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

        /// <summary>The date by which this issue needs to be closed</summary>
        [Field("Microsoft.VSTS.Scheduling.DueDate")]
        public virtual DateTime? DueDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.DueDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.DueDate", value);
        }

        /// <summary>The build in which the bug was fixed</summary>
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

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

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
    }
}