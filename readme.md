# Linq provider for VSTS/TFS workitem queries

## Prerequisites

This provider uses the VSTS Rest API to query the workitem repository. The provider uses the standard nuget [client package](https://www.nuget.org/packages/Microsoft.TeamFoundationServer.Client)

## How it queries works

The provider works like extensions to the `WorkItemTrackingHttpClient` class:

### Queries on the WorkItem class

The provider support queries on the standard WorkItem class. Use the Field extention method to target specific workitem fields:

```csharp
var assignedToMe =	from workitem in Client.All()
                    where workitem.Field<string>("System.AssignedTo") == QueryConstant.Me
                    select workitem;
```

### Queries on Generic workitem

The provider contains a specific GenericWorkItem class. This class has the following purposes:
* base class for types workitems
* typed fields
* to hide the complexity of the update api

```csharp
var projectWiQuery = from bug in Client.SetOf<GenericWorkItem>()
                where bug.CreatedBy == bug.ChangedBy
                select bug;
```
### Queries on Typed workitems

You can inherits from the Generic WorkItem class and create typed version of workitems. When used, the provider automaticaly add a filter on the workitem type

Bug definition:
```csharp
[WorkItemType("Bug")]
public class Bug : GenericWorkItem
{
    [Field("Microsoft.VSTS.Common.Priority")]
    public virtual int? Priority
    {
        get => GetStructField<int>("Microsoft.VSTS.Common.Priority");
        set => SetStructField("Microsoft.VSTS.Common.Priority", value);
    }
   
}
```

Query on bugs:
```
var projectWiQuery = from bug in Client.SetOf<Bug>()
    where bug.CreatedBy == bug.ChangedBy && bug.Priority == 2
    select bug;
```

### Queries on Process Template

You can register a set of typed workitems and let the provider instanciate the right version:

```csharp
public class ScrumTemplate : ProcessTemplate
    {
        public ScrumTemplate()
        {
            AddWorkItemType<Epic>();
            AddWorkItemType<Feature>();
            AddWorkItemType<ProductBacklogItem>();
            AddWorkItemType<Bug>();
            AddWorkItemType<Task>();
           // ...
        }
    }
```


```csharp
var q = from workitem in Client.FromTemplate<ScrumTemplate>()                
                select workitem;
```


### Syn or Async queries

As any linq provider, sync queries are supported. You can also use a specific extension method to retrieve the data as an async query:

```
var projectWiQuery = from bug in Client.SetOf<Bug>(Project)
    where bug.IsUnderIteration(iteration)
    select bug;

            
var result = await projectWiQuery.ToListAsync();
```

## Create and save typed workitems

Typed workitems can be created and updates by using the New<> and Save<> methods:
```
 var bug = Project.New<Bug>();

bug.Title = "New bug created";

await Client.Save(bug);

bug.Title = "Title Changed";
bug.Priority = 2;

await Client.Save(bug);
```