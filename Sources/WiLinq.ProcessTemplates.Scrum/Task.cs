using WiLinq.LinqProvider;

namespace WiLinq.ProcessTemplates.Scrum {
 
  
  [WorkItemTypeAttribute("Task")]
  public partial class Task : GenericWorkItem {
    
    /// <summary>Type of work involved</summary>
    [FieldAttribute("Microsoft.VSTS.Common.Activity")]
    public virtual string Activity {
      get {
        return GetRefField<string>("Microsoft.VSTS.Common.Activity");
      }
      set {
        SetRefField<string>("Microsoft.VSTS.Common.Activity", value);
      }
    }
    
    [FieldAttribute("System.AuthorizedAs")]
    public virtual string AuthorizedAs {
      get {
        return GetRefField<string>("System.AuthorizedAs");
      }
    }
    
    [FieldAttribute("System.AuthorizedDate")]
    public virtual System.Nullable<System.DateTime> AuthorizedDate {
      get {
        return GetStructField<System.DateTime>("System.AuthorizedDate");
      }
    }
    
    [FieldAttribute("Microsoft.VSTS.Common.BacklogPriority")]
    public virtual System.Nullable<double> BacklogPriority {
      get {
        return GetStructField<double>("Microsoft.VSTS.Common.BacklogPriority");
      }
      set {
        SetStructField<double>("Microsoft.VSTS.Common.BacklogPriority", value);
      }
    }
    
    [FieldAttribute("Microsoft.VSTS.CMMI.Blocked")]
    public virtual string Blocked {
      get {
        return GetRefField<string>("Microsoft.VSTS.CMMI.Blocked");
      }
      set {
        SetRefField<string>("Microsoft.VSTS.CMMI.Blocked", value);
      }
    }
    
    [FieldAttribute("System.BoardColumn")]
    public virtual string BoardColumn {
      get {
        return GetRefField<string>("System.BoardColumn");
      }
    }
    
    [FieldAttribute("System.BoardColumnDone")]
    public virtual System.Nullable<bool> BoardColumnDone {
      get {
        return GetStructField<bool>("System.BoardColumnDone");
      }
    }
    
    [FieldAttribute("System.BoardLane")]
    public virtual string BoardLane {
      get {
        return GetRefField<string>("System.BoardLane");
      }
    }
    
    [FieldAttribute("Microsoft.VSTS.Common.ClosedDate")]
    public virtual System.Nullable<System.DateTime> ClosedDate {
      get {
        return GetStructField<System.DateTime>("Microsoft.VSTS.Common.ClosedDate");
      }
      set {
        SetStructField<System.DateTime>("Microsoft.VSTS.Common.ClosedDate", value);
      }
    }
    
    [FieldAttribute("System.History")]
    public virtual string History {
      get {
        return GetRefField<string>("System.History");
      }
      set {
        SetRefField<string>("System.History", value);
      }
    }
    
    [FieldAttribute("Microsoft.VSTS.Build.IntegrationBuild")]
    public virtual string IntegrationBuild {
      get {
        return GetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild");
      }
      set {
        SetRefField<string>("Microsoft.VSTS.Build.IntegrationBuild", value);
      }
    }
    
    /// <summary>Importance to business</summary>
    [FieldAttribute("Microsoft.VSTS.Common.Priority")]
    public virtual System.Nullable<int> Priority {
      get {
        return GetStructField<int>("Microsoft.VSTS.Common.Priority");
      }
      set {
        SetStructField<int>("Microsoft.VSTS.Common.Priority", value);
      }
    }
    
    /// <summary>An estimate of the number of units of work remaining to complete this task</summary>
    [FieldAttribute("Microsoft.VSTS.Scheduling.RemainingWork")]
    public virtual System.Nullable<double> RemainingWork {
      get {
        return GetStructField<double>("Microsoft.VSTS.Scheduling.RemainingWork");
      }
      set {
        SetStructField<double>("Microsoft.VSTS.Scheduling.RemainingWork", value);
      }
    }
    
    [FieldAttribute("System.RevisedDate")]
    public virtual System.Nullable<System.DateTime> RevisedDate {
      get {
        return GetStructField<System.DateTime>("System.RevisedDate");
      }
    }
    
    [FieldAttribute("Microsoft.VSTS.Common.StateChangeDate")]
    public virtual System.Nullable<System.DateTime> StateChangeDate {
      get {
        return GetStructField<System.DateTime>("Microsoft.VSTS.Common.StateChangeDate");
      }
      set {
        SetStructField<System.DateTime>("Microsoft.VSTS.Common.StateChangeDate", value);
      }
    }
    
    [FieldAttribute("System.Tags")]
    public virtual string Tags {
      get {
        return GetRefField<string>("System.Tags");
      }
      set {
        SetRefField<string>("System.Tags", value);
      }
    }
    
    [FieldAttribute("System.Watermark")]
    public virtual System.Nullable<int> Watermark {
      get {
        return GetStructField<int>("System.Watermark");
      }
    }
  }
}
