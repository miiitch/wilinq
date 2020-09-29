using System;

namespace WiLinq.ProcessTemplates.Agile
{
    [LinqProvider.WorkItemType("Shared Parameter")]
    public partial class SharedParameter : LinqProvider.GenericWorkItem {
    
        [LinqProvider.Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [LinqProvider.Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        [LinqProvider.Field("System.CommentCount")]
        public virtual long? CommentCount => GetStructField<long>("System.CommentCount");

        /// <summary>The build in which the bug was fixed</summary>
        [LinqProvider.Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.TCM.Parameters")]
        public virtual string Parameters {
            get => GetRefField<string>("Microsoft.VSTS.TCM.Parameters");
            set => SetRefField("Microsoft.VSTS.TCM.Parameters", value);
        }
    
        [LinqProvider.Field("System.Parent")]
        public virtual long? Parent => GetStructField<long>("System.Parent");

        [LinqProvider.Field("System.RemoteLinkCount")]
        public virtual long? RemoteLinkCount => GetStructField<long>("System.RemoteLinkCount");

        [LinqProvider.Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [LinqProvider.Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }
    }
}