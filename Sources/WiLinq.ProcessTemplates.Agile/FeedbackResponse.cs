using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Feedback Response")]
    public class FeedbackResponse : GenericWorkItem
    {
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

        /// <summary>The build in which the bug was fixed</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        /// <summary>Overall rating provided as part of feedback response</summary>
        [Field("Microsoft.VSTS.Common.Rating")]
        public virtual string Rating
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.Rating");
            set => SetRefField("Microsoft.VSTS.Common.Rating", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>Test context, provided automatically by test infrastructure</summary>
        [Field("Microsoft.VSTS.TCM.SystemInfo")]
        public virtual string SystemInfo
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.SystemInfo");
            set => SetRefField("Microsoft.VSTS.TCM.SystemInfo", value);
        }
    }
}