using System;
using WiLinq.LinqProvider;


namespace WiLinq.ProcessTemplates.Scrum
{
    [WorkItemType("Test Suite")]
    public class TestSuite : GenericWorkItem {
    
        [Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        [Field("System.BoardColumn")]
        public virtual string BoardColumn => GetRefField<string>("System.BoardColumn");

        [Field("System.BoardColumnDone")]
        public virtual bool? BoardColumnDone => GetStructField<bool>("System.BoardColumnDone");

        [Field("System.BoardLane")]
        public virtual string BoardLane => GetRefField<string>("System.BoardLane");

        /// <summary>Discussion thread plus automatic record of changes</summary>
        [Field("System.History")]
        public virtual string History {
            get => GetRefField<string>("System.History");
            set => SetRefField("System.History", value);
        }
    
        /// <summary>The build on which the test suite was executed</summary>
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [Field("Microsoft.VSTS.TCM.QueryText")]
        public virtual string QueryText {
            get => GetRefField<string>("Microsoft.VSTS.TCM.QueryText");
            set => SetRefField("Microsoft.VSTS.TCM.QueryText", value);
        }
    
        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [Field("System.Tags")]
        public virtual string Tags {
            get => GetRefField<string>("System.Tags");
            set => SetRefField("System.Tags", value);
        }
    
        /// <summary>Captures the test suite audit trail.</summary>
        [Field("Microsoft.VSTS.TCM.TestSuiteAudit")]
        public virtual string TestSuiteAudit {
            get => GetRefField<string>("Microsoft.VSTS.TCM.TestSuiteAudit");
            set => SetRefField("Microsoft.VSTS.TCM.TestSuiteAudit", value);
        }
    
        /// <summary>Specifies the category of the test suite.</summary>
        [Field("Microsoft.VSTS.TCM.TestSuiteType")]
        public virtual string TestSuiteType {
            get => GetRefField<string>("Microsoft.VSTS.TCM.TestSuiteType");
            set => SetRefField("Microsoft.VSTS.TCM.TestSuiteType", value);
        }
    
        [Field("Microsoft.VSTS.TCM.TestSuiteTypeId")]
        public virtual int? TestSuiteTypeId {
            get => GetStructField<int>("Microsoft.VSTS.TCM.TestSuiteTypeId");
            set => SetStructField("Microsoft.VSTS.TCM.TestSuiteTypeId", value);
        }
    
        [Field("System.Watermark")]
        public virtual int? Watermark => GetStructField<int>("System.Watermark");
    }
}