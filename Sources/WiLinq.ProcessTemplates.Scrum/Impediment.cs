using System;

namespace WiLinq.ProcessTemplates.Scrum
{
    [LinqProvider.WorkItemType("Impediment")]
    public partial class Impediment : LinqProvider.GenericWorkItem {
    
        [LinqProvider.Field("Microsoft.VSTS.Common.ActivatedBy")]
        public virtual string ActivatedBy {
            get => GetRefField<string>("Microsoft.VSTS.Common.ActivatedBy");
            set => SetRefField("Microsoft.VSTS.Common.ActivatedBy", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.Common.ActivatedDate")]
        public virtual DateTime? ActivatedDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ActivatedDate");
            set => SetStructField("Microsoft.VSTS.Common.ActivatedDate", value);
        }
    
        [LinqProvider.Field("System.AuthorizedAs")]
        public virtual string AuthorizedAs => GetRefField<string>("System.AuthorizedAs");

        [LinqProvider.Field("System.AuthorizedDate")]
        public virtual DateTime? AuthorizedDate => GetStructField<DateTime>("System.AuthorizedDate");

        [LinqProvider.Field("Microsoft.VSTS.Common.BacklogPriority")]
        public virtual double? BacklogPriority {
            get => GetStructField<double>("Microsoft.VSTS.Common.BacklogPriority");
            set => SetStructField("Microsoft.VSTS.Common.BacklogPriority", value);
        }
    
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

        [LinqProvider.Field("Microsoft.VSTS.Scheduling.Effort")]
        public virtual double? Effort {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.Effort");
            set => SetStructField("Microsoft.VSTS.Scheduling.Effort", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [LinqProvider.Field("System.Parent")]
        public virtual long? Parent => GetStructField<long>("System.Parent");

        [LinqProvider.Field("Microsoft.VSTS.Common.Priority")]
        public virtual long? Priority {
            get => GetStructField<long>("Microsoft.VSTS.Common.Priority");
            set => SetStructField("Microsoft.VSTS.Common.Priority", value);
        }
    
        [LinqProvider.Field("System.RemoteLinkCount")]
        public virtual long? RemoteLinkCount => GetStructField<long>("System.RemoteLinkCount");

        [LinqProvider.Field("Microsoft.VSTS.Common.Resolution")]
        public virtual string Resolution {
            get => GetRefField<string>("Microsoft.VSTS.Common.Resolution");
            set => SetRefField("Microsoft.VSTS.Common.Resolution", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.Common.ResolvedBy")]
        public virtual string ResolvedBy {
            get => GetRefField<string>("Microsoft.VSTS.Common.ResolvedBy");
            set => SetRefField("Microsoft.VSTS.Common.ResolvedBy", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.Common.ResolvedDate")]
        public virtual DateTime? ResolvedDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.ResolvedDate");
            set => SetStructField("Microsoft.VSTS.Common.ResolvedDate", value);
        }
    
        [LinqProvider.Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        [LinqProvider.Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }
    }
}