using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Scrum
{
    [WorkItemType("Test Case")]
    public class TestCase : GenericWorkItem
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

        [Field("Microsoft.VSTS.TCM.AutomatedTestId")]
        public virtual string AutomatedTestId
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.AutomatedTestId");
            set => SetRefField("Microsoft.VSTS.TCM.AutomatedTestId", value);
        }

        [Field("Microsoft.VSTS.TCM.AutomatedTestName")]
        public virtual string AutomatedTestName
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.AutomatedTestName");
            set => SetRefField("Microsoft.VSTS.TCM.AutomatedTestName", value);
        }

        [Field("Microsoft.VSTS.TCM.AutomatedTestStorage")]
        public virtual string AutomatedTestStorage
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.AutomatedTestStorage");
            set => SetRefField("Microsoft.VSTS.TCM.AutomatedTestStorage", value);
        }

        [Field("Microsoft.VSTS.TCM.AutomatedTestType")]
        public virtual string AutomatedTestType
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.AutomatedTestType");
            set => SetRefField("Microsoft.VSTS.TCM.AutomatedTestType", value);
        }

        [Field("Microsoft.VSTS.TCM.AutomationStatus")]
        public virtual string Automationstatus
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.AutomationStatus");
            set => SetRefField("Microsoft.VSTS.TCM.AutomationStatus", value);
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

        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        [Field("Microsoft.VSTS.TCM.LocalDataSource")]
        public virtual string LocalDataSource
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.LocalDataSource");
            set => SetRefField("Microsoft.VSTS.TCM.LocalDataSource", value);
        }

        [Field("Microsoft.VSTS.TCM.Parameters")]
        public virtual string Parameters
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.Parameters");
            set => SetRefField("Microsoft.VSTS.TCM.Parameters", value);
        }

        [Field("Microsoft.VSTS.Common.Priority")]
        public virtual long? Priority
        {
            get => GetStructField<long>("Microsoft.VSTS.Common.Priority");
            set => SetStructField("Microsoft.VSTS.Common.Priority", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate
        {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }

        [Field("Microsoft.VSTS.TCM.Steps")]
        public virtual string Steps
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.Steps");
            set => SetRefField("Microsoft.VSTS.TCM.Steps", value);
        }
    }
}