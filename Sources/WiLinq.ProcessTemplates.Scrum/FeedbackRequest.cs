using System;

namespace WiLinq.ProcessTemplates.Scrum
{
    [LinqProvider.WorkItemType("Feedback Request")]
    public partial class FeedbackRequest : LinqProvider.GenericWorkItem {
    
        /// <summary>Instructions to launch the specified application</summary>
        [LinqProvider.Field("Microsoft.VSTS.Feedback.ApplicationLaunchInstructions")]
        public virtual string ApplicationLaunchInstructions {
            get => GetRefField<string>("Microsoft.VSTS.Feedback.ApplicationLaunchInstructions");
            set => SetRefField("Microsoft.VSTS.Feedback.ApplicationLaunchInstructions", value);
        }
    
        /// <summary>The path to execute the application</summary>
        [LinqProvider.Field("Microsoft.VSTS.Feedback.ApplicationStartInformation")]
        public virtual string ApplicationStartInformation {
            get => GetRefField<string>("Microsoft.VSTS.Feedback.ApplicationStartInformation");
            set => SetRefField("Microsoft.VSTS.Feedback.ApplicationStartInformation", value);
        }
    
        /// <summary>The type of application on which to give feedback</summary>
        [LinqProvider.Field("Microsoft.VSTS.Feedback.ApplicationType")]
        public virtual string ApplicationType {
            get => GetRefField<string>("Microsoft.VSTS.Feedback.ApplicationType");
            set => SetRefField("Microsoft.VSTS.Feedback.ApplicationType", value);
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
    
        [LinqProvider.Field("System.CommentCount")]
        public virtual long? CommentCount => GetStructField<long>("System.CommentCount");

        [LinqProvider.Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [LinqProvider.Field("System.Parent")]
        public virtual long? Parent => GetStructField<long>("System.Parent");

        [LinqProvider.Field("System.RemoteLinkCount")]
        public virtual long? RemoteLinkCount => GetStructField<long>("System.RemoteLinkCount");

        [LinqProvider.Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");
    }
}