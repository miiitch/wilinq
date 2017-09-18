# Linq provider for VSTS/TFS workitem queries

## Prerequisites

This provider uses the VSTS Rest API to query the workitem repository. The provider uses the standard nuget [client package](https://www.nuget.org/packages/Microsoft.TeamFoundationServer.Client)

## How it works

The provider works like extensions to the `WorkItemTrackingHttpClient` class:

### Queries on the WorkItem class

The provider support queries on the standard WorkItem class. Use the Field extention method to target specific workitem fields:

```csharp
var assignedToMe =	from workitem in Client.All()
                    where workitem.Field<string>("System.AssignedTo") == QueryConstant.Me
                    select workitem;
```


### Queries on Generic workitem

### Queries on Typed workitems

### Queries on Process Template

### Syn or Async queries