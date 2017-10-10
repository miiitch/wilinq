using System;
using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Agile
{
    [WorkItemType("Test Suite")]
    public class TestSuite : GenericWorkItem
    {

        [Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        /// <summary>The build on which the test suite was executed</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild
        {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }

        [Field("Microsoft.VSTS.TCM.QueryText")]
        public virtual string QueryText
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.QueryText");
            set => SetRefField("Microsoft.VSTS.TCM.QueryText", value);
        }

        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>Captures the test suite audit trail.</summary>
        [Field("Microsoft.VSTS.TCM.TestSuiteAudit")]
        public virtual string TestSuiteAudit
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.TestSuiteAudit");
            set => SetRefField("Microsoft.VSTS.TCM.TestSuiteAudit", value);
        }

        /// <summary>Specifies the category of the test suite.</summary>
        [Field("Microsoft.VSTS.TCM.TestSuiteType")]
        public virtual string TestSuiteType
        {
            get => GetRefField<string>("Microsoft.VSTS.TCM.TestSuiteType");
            set => SetRefField("Microsoft.VSTS.TCM.TestSuiteType", value);
        }

        [Field("Microsoft.VSTS.TCM.TestSuiteTypeId")]
        public virtual long? TestSuiteTypeId
        {
            get => GetStructField<long>("Microsoft.VSTS.TCM.TestSuiteTypeId");
            set => SetStructField("Microsoft.VSTS.TCM.TestSuiteTypeId", value);
        }
    }
}