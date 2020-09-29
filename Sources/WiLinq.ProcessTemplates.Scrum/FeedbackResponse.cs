using System;

namespace WiLinq.ProcessTemplates.Scrum
{
    [LinqProvider.WorkItemType("Feedback Response")]
    public partial class FeedbackResponse : LinqProvider.GenericWorkItem {
    
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
    
        [LinqProvider.Field("System.CommentCount")]
        public virtual long? CommentCount => GetStructField<long>("System.CommentCount");

        [LinqProvider.Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [LinqProvider.Field("System.Parent")]
        public virtual long? Parent => GetStructField<long>("System.Parent");

        /// <summary>Overall rating provided as part of feedback response</summary>
        [LinqProvider.Field("Microsoft.VSTS.Common.Rating")]
        public virtual string Rating {
            get => GetRefField<string>("Microsoft.VSTS.Common.Rating");
            set => SetRefField("Microsoft.VSTS.Common.Rating", value);
        }
    
        [LinqProvider.Field("System.RemoteLinkCount")]
        public virtual long? RemoteLinkCount => GetStructField<long>("System.RemoteLinkCount");

        [LinqProvider.Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>Test context, provided automatically by test infrastructure</summary>
        [LinqProvider.Field("Microsoft.VSTS.TCM.SystemInfo")]
        public virtual string SystemInfo {
            get => GetRefField<string>("Microsoft.VSTS.TCM.SystemInfo");
            set => SetRefField("Microsoft.VSTS.TCM.SystemInfo", value);
        }
    }
}