using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Feedback Request")]
    public class FeedbackRequest : GenericWorkItem
    {

        /// <summary>Instructions to launch the specified application</summary>
        [Field("Microsoft.VSTS.Feedback.ApplicationLaunchInstructions")]
        public virtual string ApplicationLaunchInstructions
        {
            get => GetRefField<string>("Microsoft.VSTS.Feedback.ApplicationLaunchInstructions");
            set => SetRefField("Microsoft.VSTS.Feedback.ApplicationLaunchInstructions", value);
        }

        /// <summary>The path to execute the application</summary>
        [Field("Microsoft.VSTS.Feedback.ApplicationStartInformation")]
        public virtual string ApplicationStartInformation
        {
            get => GetRefField<string>("Microsoft.VSTS.Feedback.ApplicationStartInformation");
            set => SetRefField("Microsoft.VSTS.Feedback.ApplicationStartInformation", value);
        }

        /// <summary>The type of application on which to give feedback</summary>
        [Field("Microsoft.VSTS.Feedback.ApplicationType")]
        public virtual string ApplicationType
        {
            get => GetRefField<string>("Microsoft.VSTS.Feedback.ApplicationType");
            set => SetRefField("Microsoft.VSTS.Feedback.ApplicationType", value);
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

        /// <summary>The build in which the feedback was requested</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");
    }
}