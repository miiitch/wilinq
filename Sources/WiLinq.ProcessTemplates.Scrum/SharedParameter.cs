using System;
using WiLinq.LinqProvider;
namespace WiLinq.ProcessTemplates.Scrum
{
    [WorkItemType("Shared Parameter")]
    public class SharedParameter : GenericWorkItem {
    
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

        [Field("System.History")]
        public virtual string History {
            get => GetRefField<string>("System.History");
            set => SetRefField("System.History", value);
        }
    
        [Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [Field("Microsoft.VSTS.TCM.Parameters")]
        public virtual string Parameters {
            get => GetRefField<string>("Microsoft.VSTS.TCM.Parameters");
            set => SetRefField("Microsoft.VSTS.TCM.Parameters", value);
        }
    
        [Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }
    
        [Field("System.Tags")]
        public virtual string Tags {
            get => GetRefField<string>("System.Tags");
            set => SetRefField("System.Tags", value);
        }
    
        [Field("System.Watermark")]
        public virtual int? Watermark => GetStructField<int>("System.Watermark");
    }
}