using System;

namespace WiLinq.ProcessTemplates.Agile
{
    [LinqProvider.WorkItemType("Test Suite")]
    public partial class TestSuite : LinqProvider.GenericWorkItem {
    
        [LinqProvider.Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [LinqProvider.Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        [LinqProvider.Field("System.CommentCount")]
        public virtual long? CommentCount => GetStructField<long>("System.CommentCount");

        /// <summary>The build on which the test suite was executed</summary>
        [LinqProvider.Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [LinqProvider.Field("System.Parent")]
        public virtual long? Parent => GetStructField<long>("System.Parent");

        [LinqProvider.Field("Microsoft.VSTS.TCM.QueryText")]
        public virtual string QueryText {
            get => GetRefField<string>("Microsoft.VSTS.TCM.QueryText");
            set => SetRefField("Microsoft.VSTS.TCM.QueryText", value);
        }
    
        [LinqProvider.Field("System.RemoteLinkCount")]
        public virtual long? RemoteLinkCount => GetStructField<long>("System.RemoteLinkCount");

        [LinqProvider.Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>Captures the test suite audit trail.</summary>
        [LinqProvider.Field("Microsoft.VSTS.TCM.TestSuiteAudit")]
        public virtual string TestSuiteAudit {
            get => GetRefField<string>("Microsoft.VSTS.TCM.TestSuiteAudit");
            set => SetRefField("Microsoft.VSTS.TCM.TestSuiteAudit", value);
        }
    
        /// <summary>Specifies the category of the test suite.</summary>
        [LinqProvider.Field("Microsoft.VSTS.TCM.TestSuiteType")]
        public virtual string TestSuiteType {
            get => GetRefField<string>("Microsoft.VSTS.TCM.TestSuiteType");
            set => SetRefField("Microsoft.VSTS.TCM.TestSuiteType", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.TCM.TestSuiteTypeId")]
        public virtual long? TestSuiteTypeId {
            get => GetStructField<long>("Microsoft.VSTS.TCM.TestSuiteTypeId");
            set => SetStructField("Microsoft.VSTS.TCM.TestSuiteTypeId", value);
        }
    }
}