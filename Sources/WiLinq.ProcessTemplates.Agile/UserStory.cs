using System;

namespace WiLinq.ProcessTemplates.Agile
{
    [LinqProvider.WorkItemType("User Story")]
    public partial class UserStory : LinqProvider.GenericWorkItem {
    
        [LinqProvider.Field("Microsoft.VSTS.Common.AcceptanceCriteria")]
        public virtual string AcceptanceCriteria {
            get => GetRefField<string>("Microsoft.VSTS.Common.AcceptanceCriteria");
            set => SetRefField("Microsoft.VSTS.Common.AcceptanceCriteria", value);
        }
    
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

        /// <summary>The completion date for all the tasks implementing this story</summary>
        [LinqProvider.Field("Microsoft.VSTS.Scheduling.FinishDate")]
        public virtual DateTime? FinishDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.FinishDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.FinishDate", value);
        }
    
        /// <summary>The build in which the bug was fixed</summary>
        [LinqProvider.Field("Microsoft.VSTS.Build.IntegrationBuild")]
        public virtual string IntegrationBuild {
            get => GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
            set => SetRefField("Microsoft.VSTS.Build.IntegrationBuild", value);
        }
    
        [LinqProvider.Field("System.Parent")]
        public virtual long? Parent => GetStructField<long>("System.Parent");

        /// <summary>Business importance. 1=must fix; 4=unimportant.</summary>
        [LinqProvider.Field("Microsoft.VSTS.Common.Priority")]
        public virtual long? Priority {
            get => GetStructField<long>("Microsoft.VSTS.Common.Priority");
            set => SetStructField("Microsoft.VSTS.Common.Priority", value);
        }
    
        [LinqProvider.Field("System.RemoteLinkCount")]
        public virtual long? RemoteLinkCount => GetStructField<long>("System.RemoteLinkCount");

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
    
        [LinqProvider.Field("Microsoft.VSTS.Common.ResolvedReason")]
        public virtual string ResolvedReason {
            get => GetRefField<string>("Microsoft.VSTS.Common.ResolvedReason");
            set => SetRefField("Microsoft.VSTS.Common.ResolvedReason", value);
        }
    
        [LinqProvider.Field("System.RevisedDate")]
        public virtual DateTime? RevisedDate => GetStructField<DateTime>("System.RevisedDate");

        /// <summary>Uncertainty in requirement or design</summary>
        [LinqProvider.Field("Microsoft.VSTS.Common.Risk")]
        public virtual string Risk {
            get => GetRefField<string>("Microsoft.VSTS.Common.Risk");
            set => SetRefField("Microsoft.VSTS.Common.Risk", value);
        }
    
        /// <summary>Work first on items with lower-valued stack rank. Set in triage.</summary>
        [LinqProvider.Field("Microsoft.VSTS.Common.StackRank")]
        public virtual double? StackRank {
            get => GetStructField<double>("Microsoft.VSTS.Common.StackRank");
            set => SetStructField("Microsoft.VSTS.Common.StackRank", value);
        }
    
        /// <summary>The start date for implementation of this story</summary>
        [LinqProvider.Field("Microsoft.VSTS.Scheduling.StartDate")]
        public virtual DateTime? StartDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Scheduling.StartDate");
            set => SetStructField("Microsoft.VSTS.Scheduling.StartDate", value);
        }
    
        [LinqProvider.Field("Microsoft.VSTS.Common.StateChangeDate")]
        public virtual DateTime? StateChangeDate {
            get => GetStructField<DateTime>("Microsoft.VSTS.Common.StateChangeDate");
            set => SetStructField("Microsoft.VSTS.Common.StateChangeDate", value);
        }
    
        /// <summary>The size of work estimated for implementing this user story</summary>
        [LinqProvider.Field("Microsoft.VSTS.Scheduling.StoryPoints")]
        public virtual double? StoryPoints {
            get => GetStructField<double>("Microsoft.VSTS.Scheduling.StoryPoints");
            set => SetStructField("Microsoft.VSTS.Scheduling.StoryPoints", value);
        }
    
        /// <summary>Business = delivers value to a user or another system; Architectural = work to support other stories or components</summary>
        [LinqProvider.Field("Microsoft.VSTS.Common.ValueArea")]
        public virtual string ValueArea {
            get => GetRefField<string>("Microsoft.VSTS.Common.ValueArea");
            set => SetRefField("Microsoft.VSTS.Common.ValueArea", value);
        }
    }
}