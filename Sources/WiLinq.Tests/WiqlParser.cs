using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WiLinq.Tests
{
	[TestFixture]
	public class WiqlParser
	{
		[Test]
		public void Test_Select_WithOneField()
		{
			var query = "SELECT [System.ID] from Workitems";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_WithTwoFields()
		{
			var query = "SELECT [System.ID],[System.CreatedBy] from Workitems";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(2, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
			Assert.AreEqual("System.CreatedBy", output.Fields[1]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test()
		{
			var query = "SELECT [System.ID] from Workitems where [System.ID] > 1";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test_And_Parens()
		{
			var query = "SELECT [System.ID] from Workitems where ( [System.ID] > 1 )";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_Two_Tests()
		{
			var query = "SELECT [System.ID] from Workitems where [System.ID] > 1 AND [System.ID] < 3";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test_And_Parameter_Value()
		{

			var query = "SELECT [System.ID] from Workitems where [System.Project] = @MyProject";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test_And_Parameter_String_With_Quote()
		{

			var query = "SELECT [System.ID] from Workitems where [System.Project] = 'He''llo'";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test_And_Parameter_String_With_DoubleQuote()
		{

			var query = "SELECT [System.ID] from Workitems where [System.Project] = \"He\"\"llo\"";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test_And_Today()
		{

			var query = "SELECT [System.ID] from Workitems where [System.CreateDate] = @today";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test_And_Me()
		{

			var query = "SELECT [System.ID] from Workitems where [System.CreateDate] = @me";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}


		[Test]
		public void Test_Select_Where_With_One_Test_And_Project()
		{

			var query = "SELECT [System.ID] from Workitems where [System.CreateDate] = @project";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Select_Where_With_One_Test_And_Order_By_One_Field()
		{

			var query = "SELECT [System.ID] from Workitems where [System.CreateDate] = @project order by [System.ID]";
			var output = Wiql.Parser.Process(query);
			Assert.AreEqual(1, output.Fields.Count);
			Assert.AreEqual("System.ID", output.Fields[0]);
		}

		[Test]
		public void Test_Query_In_Statement()
		{

			var query = @"SELECT [System.Id], [System.WorkItemType], [Microsoft.VSTS.Common.Rank], [System.State], [System.AssignedTo], [System.Title] FROM WorkItems WHERE [System.TeamProject] = @project AND [System.State] IN ('Active', 'Pending', 'Proposed', 'Requested')  ORDER BY [Microsoft.VSTS.Common.Rank], [System.WorkItemType], [System.Id]";			
			var output = Wiql.Parser.Process(query);

		}

		[Test]
	[TestCase("select [System.Id], [Microsoft.VSTS.Common.Priority], [System.State], [System.AssignedTo], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'Test Case' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.Priority], [System.State], [System.AssignedTo], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'Test Case' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.Priority], [System.State], [System.AssignedTo], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'Test Case' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.Priority], [System.State], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.AssignedTo] = @me and [System.WorkItemType] = 'Test Case' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.AssignedTo], [System.Reason], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Resolved' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.AssignedTo], [System.Reason], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Resolved' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.AssignedTo], [System.Reason], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Resolved' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.AssignedTo], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Active' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.AssignedTo], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Active' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.AssignedTo], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Active' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.State], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.AssignedTo] = @me and [System.WorkItemType] = 'Bug' and [System.State] <> 'Closed' order by [System.State], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Title], [System.WorkItemType], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.AssignedTo], [System.IterationPath] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Active' and [System.AssignedTo] = '' order by [Microsoft.VSTS.Common.StackRank], [System.IterationPath], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Title], [System.WorkItemType], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.AssignedTo], [System.IterationPath] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Active' and [System.AssignedTo] = '' order by [Microsoft.VSTS.Common.StackRank], [System.IterationPath], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Title], [System.WorkItemType], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.AssignedTo], [System.IterationPath] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'Bug' and [System.State] = 'Active' and [System.AssignedTo] = '' order by [Microsoft.VSTS.Common.StackRank], [System.IterationPath], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'Task' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'Task' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'Task' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.State], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.AssignedTo] = @me and [System.WorkItemType] = 'Task' and [System.State] <> 'Closed' order by [System.State], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Scheduling.StoryPoints], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'User Story' and [System.State] = 'Active' order by [Microsoft.VSTS.Common.StackRank]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Scheduling.StoryPoints], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'User Story' and [System.State] = 'Active' order by [Microsoft.VSTS.Common.StackRank]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Scheduling.StoryPoints], [System.Title] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'User Story' and [System.State] = 'Active' order by [Microsoft.VSTS.Common.StackRank]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Scheduling.StoryPoints], [System.Title], [System.State], [System.IterationPath], [System.AreaPath], [System.WorkItemType] from WorkItems where [System.TeamProject] = @project and [System.AreaPath] under @project and [System.WorkItemType] = 'User Story' and ([System.State] <> 'Closed' or [Microsoft.VSTS.Common.ClosedDate] > @today - 90) order by [Microsoft.VSTS.Common.StackRank]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Scheduling.StoryPoints], [System.Title], [System.State], [System.IterationPath], [System.AreaPath], [System.WorkItemType] from WorkItems where [System.TeamProject] = @project and [System.AreaPath] under @project and [System.WorkItemType] = 'User Story' and [System.State] <> 'Closed' order by [Microsoft.VSTS.Common.StackRank]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [System.Title], [Microsoft.VSTS.Common.Priority], [System.AssignedTo] from WorkItems where [System.TeamProject] = @project and [System.WorkItemType] = 'Issue' and [System.State] <> 'Closed' and [System.IterationPath] under 'AgileProject\\Iteration 1' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [System.Title], [Microsoft.VSTS.Common.Priority], [System.AssignedTo] from WorkItems where [System.TeamProject] = @project and [System.WorkItemType] = 'Issue' and [System.State] <> 'Closed' and [System.IterationPath] under 'AgileProject2\\Iteration 1' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [System.Title], [Microsoft.VSTS.Common.Priority], [System.AssignedTo] from WorkItems where [System.TeamProject] = @project and [System.WorkItemType] = 'Issue' and [System.State] <> 'Closed' and [System.IterationPath] under 'AgileProject3\\Iteration 1' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [System.Title], [System.AssignedTo], [System.Reason] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'User Story' and [System.State] = 'Closed' and [System.Reason] = 'Acceptance tests pass' order by [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [System.Title], [System.AssignedTo], [System.Reason] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'User Story' and [System.State] = 'Closed' and [System.Reason] = 'Acceptance tests pass' order by [System.Id]")]
[TestCase("select [System.Id], [Microsoft.VSTS.Common.StackRank], [System.Title], [System.AssignedTo], [System.Reason] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'User Story' and [System.State] = 'Closed' and [System.Reason] = 'Acceptance tests pass' order by [System.Id]")]
[TestCase("select [System.Id], [System.Links.LinkType], [System.Title] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.WorkItemType] = 'User Story') and Source.[System.IterationPath] under 'AgileProject\\Iteration 1' and [System.Links.LinkType] <> '' and Target.[System.WorkItemType] = 'Test Case' order by [System.Id] mode (DoesNotContain)")]
[TestCase("select [System.Id], [System.Links.LinkType], [System.Title] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.WorkItemType] = 'User Story') and Source.[System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.Links.LinkType] <> '' and Target.[System.WorkItemType] = 'Test Case' order by [System.Id] mode (DoesNotContain)")]
[TestCase("select [System.Id], [System.Links.LinkType], [System.Title] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.WorkItemType] = 'User Story') and Source.[System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.Links.LinkType] <> '' and Target.[System.WorkItemType] = 'Test Case' order by [System.Id] mode (DoesNotContain)")]
[TestCase("select [System.Id], [System.TeamProject], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] from WorkItems where [System.WorkItemType] <> '' and [System.State] <> '' order by [System.Id]")]
[TestCase("select [System.Id], [System.Title], [Microsoft.VSTS.Common.BacklogPriority], [System.AssignedTo], [System.State], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.CMMI.Blocked], [System.WorkItemType] from WorkItemLinks where Source.[System.TeamProject] = @project and Source.[System.IterationPath] under 'ScrumProject\\Release 1\\Sprint 1' and (Source.[System.WorkItemType] = 'Product Backlog Item' or Source.[System.WorkItemType] = 'Bug' or Source.[System.WorkItemType] = 'Task') and Source.[System.State] <> 'Removed' and ([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') and (Target.[System.WorkItemType] = 'Task') and Target.[System.State] <> 'Removed' order by [Microsoft.VSTS.Common.BacklogPriority], [Microsoft.VSTS.Scheduling.Effort], [Microsoft.VSTS.Scheduling.RemainingWork] mode (Recursive)")]
[TestCase("select [System.Id], [System.Title], [Microsoft.VSTS.Common.BacklogPriority], [System.AssignedTo], [System.State], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.CMMI.Blocked], [System.WorkItemType] from WorkItemLinks where Source.[System.TeamProject] = @project and Source.[System.IterationPath] under 'ScrumProject\\Release 1\\Sprint 1' and (Source.[System.WorkItemType] = 'Product Backlog Item' or Source.[System.WorkItemType] = 'Bug' or Source.[System.WorkItemType] = 'Task') and Source.[System.State] <> 'Removed' and Source.[System.State] <> 'Done' and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' and Target.[System.State] <> 'Done' and Target.[System.State] <> 'Removed' order by [Microsoft.VSTS.Common.BacklogPriority], [Microsoft.VSTS.Scheduling.Effort], [Microsoft.VSTS.Scheduling.RemainingWork] mode (Recursive)")]
[TestCase("select [System.Id], [System.Title], [System.AssignedTo], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.OriginalEstimate] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject\\Iteration 1' and [System.WorkItemType] = 'Task' and [System.State] = 'Closed' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [System.Title], [System.AssignedTo], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.OriginalEstimate] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject2\\Iteration 1' and [System.WorkItemType] = 'Task' and [System.State] = 'Closed' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [System.Title], [System.AssignedTo], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.OriginalEstimate] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'AgileProject3\\Iteration 1' and [System.WorkItemType] = 'Task' and [System.State] = 'Closed' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Severity], [System.Id]")]
[TestCase("select [System.Id], [System.WorkItemType], [Microsoft.VSTS.Common.BacklogPriority], [System.Title], [System.AssignedTo], [System.State], [Microsoft.VSTS.Scheduling.Effort], [Microsoft.VSTS.Common.BusinessValue], [System.IterationPath] from WorkItems where [System.TeamProject] = @project and ([System.WorkItemType] = 'Product Backlog Item' or [System.WorkItemType] = 'Bug') and [System.IterationPath] = @project and [System.State] <> 'Done' and [System.State] <> 'Removed' order by [Microsoft.VSTS.Common.BacklogPriority], [Microsoft.VSTS.Scheduling.Effort]")]
[TestCase("select [System.Id], [System.WorkItemType], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [System.Title], [System.AreaPath], [System.IterationPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and (Source.[Microsoft.VSTS.Scheduling.RemainingWork] > 0 or Source.[Microsoft.VSTS.Scheduling.CompletedWork] > 0)) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' order by [System.Links.LinkType] mode (MustContain)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.IterationPath], [Microsoft.VSTS.Scheduling.StartDate], [Microsoft.VSTS.Scheduling.FinishDate] from WorkItems where [System.TeamProject] = @project and [System.WorkItemType] = 'Sprint' order by [Microsoft.VSTS.Scheduling.StartDate] desc, [System.IterationPath]")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [Microsoft.VSTS.Common.BacklogPriority], [System.AssignedTo], [System.State], [Microsoft.VSTS.CMMI.Blocked] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'ScrumProject\\Release 1\\Sprint 1' and [System.WorkItemType] = 'Task' and [Microsoft.VSTS.CMMI.Blocked] = 'Yes' and [System.State] <> 'Removed' order by [Microsoft.VSTS.Common.BacklogPriority]")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State], [Microsoft.VSTS.Common.Priority], [System.CreatedDate] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'ScrumProject\\Release 1\\Sprint 1' and [System.WorkItemType] = 'Impediment' and [System.State] = 'Open' order by [Microsoft.VSTS.Common.Priority], [System.CreatedDate]")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State], [Microsoft.VSTS.Scheduling.RemainingWork] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'ScrumProject\\Release 1\\Sprint 1' and [System.WorkItemType] = 'Task' and [System.State] = 'In Progress' order by [System.AssignedTo], [Microsoft.VSTS.Common.BacklogPriority]")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [Microsoft.VSTS.Common.Priority] from WorkItems where [System.TeamProject] = @project and [System.IterationPath] under 'ScrumProject\\Release 1\\Sprint 1' and [System.WorkItemType] = 'Test Case' order by [Microsoft.VSTS.Common.Priority]")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject\\Iteration 1' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [System.WorkItemType] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject\\Iteration 2' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject\\Iteration 3' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject2\\Iteration 1' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject2\\Iteration 2' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject2\\Iteration 3' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject3\\Iteration 1' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject3\\Iteration 2' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]
[TestCase("select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.StoryPoints], [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority], [Microsoft.VSTS.Common.Activity], [System.IterationPath], [System.AreaPath] from WorkItemLinks where (Source.[System.TeamProject] = @project and Source.[System.AreaPath] under @project and Source.[System.IterationPath] under 'AgileProject3\\Iteration 3' and (Source.[System.WorkItemType] = 'User Story' or Source.[System.WorkItemType] = 'Task')) and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' and Target.[System.WorkItemType] = 'Task' order by [Microsoft.VSTS.Common.StackRank], [Microsoft.VSTS.Common.Priority] mode (Recursive)")]

		public void Test_Parsing(string wiql)
		{

			var output = Wiql.Parser.Process(wiql);
			Assert.IsNotNull(output);
		}


	}
}
