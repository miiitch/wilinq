using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Code Review Response")]
    public class CodeReviewResponse : GenericWorkItem
    {

        [Field("Microsoft.VSTS.CodeReview.AcceptedBy")]
        public virtual string AcceptedBy
        {
            get => GetRefField<string>("Microsoft.VSTS.CodeReview.AcceptedBy");
            set => SetRefField("Microsoft.VSTS.CodeReview.AcceptedBy", value);
        }

        [Field("Microsoft.VSTS.CodeReview.AcceptedDate")]
        public virtual DateTime? AcceptedDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.CodeReview.AcceptedDate");
            set => SetStructField("Microsoft.VSTS.CodeReview.AcceptedDate", value);
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

        [Field("Microsoft.VSTS.CodeReview.ClosedStatus")]
        public virtual string ClosedStatus
        {
            get => GetRefField<string>("Microsoft.VSTS.CodeReview.ClosedStatus");
            set => SetRefField("Microsoft.VSTS.CodeReview.ClosedStatus", value);
        }

        [Field("Microsoft.VSTS.CodeReview.ClosedStatusCode")]
        public virtual long? ClosedStatusCode
        {
            get => GetStructField<long>("Microsoft.VSTS.CodeReview.ClosedStatusCode");
            set => SetStructField("Microsoft.VSTS.CodeReview.ClosedStatusCode", value);
        }

        [Field("Microsoft.VSTS.CodeReview.ClosingComment")]
        public virtual string ClosingComment
        {
            get => GetRefField<string>("Microsoft.VSTS.CodeReview.ClosingComment");
            set => SetRefField("Microsoft.VSTS.CodeReview.ClosingComment", value);
        }

        /// <summary>The build in which the bug was fixed</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        [Field("Microsoft.VSTS.Common.ReviewedBy")]
        public virtual string ReviewedBy
        {
            get => GetRefField<string>("Microsoft.VSTS.Common.ReviewedBy");
            set => SetRefField("Microsoft.VSTS.Common.ReviewedBy", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [Field("Microsoft.VSTS.Common.StateCode")]
        public virtual long? StateCode
        {
            get => GetStructField<long>("Microsoft.VSTS.Common.StateCode");
            set => SetStructField("Microsoft.VSTS.Common.StateCode", value);
        }
    }
}