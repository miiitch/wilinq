using System;

namespace WiLinq.ProcessTemplates.Agile
{
    [LinqProvider.WorkItemType("Code Review Response")]
    public partial class CodeReviewResponse : LinqProvider.GenericWorkItem {
    
        [LinqProvider.Field("Microsoft.VSTS.CodeReview.AcceptedBy")]
        public virtual string AcceptedBy {
            get => GetRefField<string>("Microsoft.VSTS.CodeReview.AcceptedBy");
            set => SetRefField("Microsoft.VSTS.CodeReview.AcceptedBy", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.CodeReview.AcceptedDate")]
        public virtual DateTime? AcceptedDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.CodeReview.AcceptedDate");
            set => SetStructField("Microsoft.VSTS.CodeReview.AcceptedDate", value);
        }
    
        [LinqProvider.Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [LinqProvider.Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        [LinqProvider.Field("Microsoft.VSTS.Common.ClosedBy")]
        public virtual string ClosedBy {
            get => GetRefField<string>("Microsoft.VSTS.Common.ClosedBy");
            set => SetRefField("Microsoft.VSTS.Common.ClosedBy", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.Common.ClosedDate")]
        public virtual DateTime? ClosedDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ClosedDate");
            set => SetStructField("Microsoft.VSTS.Common.ClosedDate", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.CodeReview.ClosedStatus")]
        public virtual string ClosedStatus {
            get => GetRefField<string>("Microsoft.VSTS.CodeReview.ClosedStatus");
            set => SetRefField("Microsoft.VSTS.CodeReview.ClosedStatus", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.CodeReview.ClosedStatusCode")]
        public virtual long? ClosedStatusCode {
            get => GetStructField<long>("Microsoft.VSTS.CodeReview.ClosedStatusCode");
            set => SetStructField("Microsoft.VSTS.CodeReview.ClosedStatusCode", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.CodeReview.ClosingComment")]
        public virtual string ClosingComment {
            get => GetRefField<string>("Microsoft.VSTS.CodeReview.ClosingComment");
            set => SetRefField("Microsoft.VSTS.CodeReview.ClosingComment", value);
        }
    
        [LinqProvider.Field("System.CommentCount")]
        public virtual long? CommentCount => GetStructField<long>("System.CommentCount");

        /// <summary>The build in which the bug was fixed</summary>
        [LinqProvider.Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [LinqProvider.Field("System.Parent")]
        public virtual long? Parent => GetStructField<long>("System.Parent");

        [LinqProvider.Field("System.RemoteLinkCount")]
        public virtual long? RemoteLinkCount => GetStructField<long>("System.RemoteLinkCount");

        [LinqProvider.Field("Microsoft.VSTS.Common.ReviewedBy")]
        public virtual string ReviewedBy {
            get => GetRefField<string>("Microsoft.VSTS.Common.ReviewedBy");
            set => SetRefField("Microsoft.VSTS.Common.ReviewedBy", value);
        }
    
        [LinqProvider.Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [LinqProvider.Field("Microsoft.VSTS.Common.StateCode")]
        public virtual long? StateCode {
            get => GetStructField<long>("Microsoft.VSTS.Common.StateCode");
            set => SetStructField("Microsoft.VSTS.Common.StateCode", value);
        }
    }
}