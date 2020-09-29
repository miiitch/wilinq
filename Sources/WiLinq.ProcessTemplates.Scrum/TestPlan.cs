using System;

namespace WiLinq.ProcessTemplates.Scrum
{
    [LinqProvider.WorkItemType("Test Plan")]
    public partial class TestPlan : LinqProvider.GenericWorkItem {
    
        [LinqProvider.Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [LinqProvider.Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        [LinqProvider.Field("System.CommentCount")]
        public virtual long? CommentCount => GetStructField<long>("System.CommentCount");

        /// <summary>The completion date for running all the tests in this test plan.</summary>
        [LinqProvider.Field("Microsoft.VSTS.Scheduling.FinishDate")]
        public virtual DateTime? FinishDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.FinishDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.FinishDate", value);
        }
    
        /// <summary>The build on which the test plan was executed"</summary>
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

        /// <summary>The start date to run the tests in this test plan.</summary>
        [LinqProvider.Field("Microsoft.VSTS.Scheduling.StartDate")]
        public virtual DateTime? StartDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.StartDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.StartDate", value);
        }
    }
}