using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Test Plan")]
    public class TestPlan : GenericWorkItem
    {

        [Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        /// <summary>The completion date for running all the tests in this test plan.</summary>
        [Field("Microsoft.VSTS.Scheduling.FinishDate")]
        public virtual DateTime? FinishDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.FinishDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.FinishDate", value);
        }

        /// <summary>The build on which the test plan was executed</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>The start date to run the tests in this test plan.</summary>
        [Field("Microsoft.VSTS.Scheduling.StartDate")]
        public virtual DateTime? StartDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.StartDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.StartDate", value);
        }
    }
}