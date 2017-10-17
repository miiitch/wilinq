using System;
using System.Linq;
using NFluent;
using NUnit.Framework;
using WiLinq.LinqProvider;
using WiLinq.ProcessTemplates.Scrum;
using Task = System.Threading.Tasks.Task;

namespace WiLinq.Tests
{
    [TestFixture]
    public class WorkItemLinqQueryShould : TestFixtureBase
    {
        [Test]
        public void Return_Only_One_Element_With_The_Right_Id()
        {
            //all workitems;
            var q = from workitem in Client.All()
                where workitem.Id == 3
                select workitem;

            // ReSharper disable once UnusedVariable
            var result = q.ToList();

            Check.That(result).HasSize(1);
            Check.That(result[0].Id).Equals(3);
        }

        [Test]
        public async Task Return_Only_One_Element_With_The_Right_Id_Async()
        {
            //all workitems;
            var q = from workitem in Client.All()
                where workitem.Id == 3
                select workitem;

            var result = await q.ToListAsync();

            Check.That(result).HasSize(1);
            Check.That(result[0].Id).Equals(3);
        }

        [Test]
        public async Task Return_Only_One_Element_Of_Type_Bug_With_The_Right_Id()
        {
            //all workitems;
            var q = from workitem in Client.SetOf<Bug>(Project)
                where workitem.Id == 174
                select workitem;

            // ReSharper disable once UnusedVariable
            var result = await q.ToListAsync();

            Check.That(result).HasSize(1);
            Check.That(result[0].Id).Equals(174);
        }

        [Test]
        public async Task Return_No_Element_Of_Type_PBI_With_The_Id_Of_Bug()
        {
            //all workitems;
            var q = from workitem in Client.SetOf<Bug>(Project)
                where workitem.Id == 173
                select workitem;

            // ReSharper disable once UnusedVariable
            var result = await q.ToListAsync();

            Check.That(result).IsEmpty();
        }


        [Test]
        public async Task Return_All_Project_Workitems()
        {
            //all workitems;
            var projectWiQuery = from workitem in Client.All(Project)
                select workitem;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
        }

        [Test]
        public async Task Return_All_Collection_Workitems()
        {
            //all workitems;
            var projectWiQuery = from workitem in Client.All()
                select workitem;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
        }

        [Test]
        public async Task Return_All_Collection_Workitem_Ids()
        {
            //all workitems;
            var projectWiQuery = from workitem in Client.All()
                select workitem;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
            var idsFromFullQuery = result.Select(w => w.Id).ToList();
            var ids = await projectWiQuery.ToIdListAsync();

            Check.That(ids).ContainsExactly(idsFromFullQuery);
        }

        [Test]
        public async Task Return_All_Workitems_Assigned_To_Me()
        {
            var projectWiQuery = from workitem in Client.All()
                where workitem.Field<string>("System.AssignedTo") == QueryConstant.Me
                select workitem;
            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
        }

        [Test]
        public async Task Return_All_Worktitem_Created_After_A_Specific_Date()
        {
            var minCreationDate = new DateTime(2017, 7, 1);
            var projectWiQuery = from workitem in Client.All()
                where workitem.Field<DateTime>(SystemField.CreatedDate) > minCreationDate
                select workitem;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            foreach (var workitem in result)
            {
                var createdDate = workitem.Field<DateTime?>(SystemField.CreatedDate);
                Check.That(createdDate).IsNotNull();
                // ReSharper disable once PossibleInvalidOperationException
                Check.That(createdDate.Value).IsAfter(minCreationDate);
            }
        }


        [Test]
        public async Task Return_All_Bugs_Of_The_Project()
        {
            var projectWiQuery = from workitem in Client.SetOf<Bug>(Project)
                select workitem;
            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            Check.That(result).Not.HasElementThatMatches(bug => bug.WorkItemType != "Bug");
        }

        [Test]
        public async Task Return_Bugs_Of_The_Project_Created_After_A_Specific_Date()
        {
            var minCreationDate = new DateTime(2017, 7, 1);
            var projectWiQuery = from bug in Client.SetOf<Bug>(Project)
                where bug.CreatedDate > minCreationDate
                select bug;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            Check.That(result).Not
                .HasElementThatMatches(bug => bug.WorkItemType != "Bug" || bug.CreatedDate <= minCreationDate);
        }

        [Test]
        public async Task Return_bugs_Under_A_Specific_Iteration()
        {
            var iteration = $@"{Project.Name}\Sprint 1";

            var projectWiQuery = from bug in Client.SetOf<Bug>(Project)
                where bug.IsUnderIteration(iteration)
                select bug;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            foreach (var bug in result)
            {
                Check.That(bug.IsUnderIteration(iteration)).IsTrue();
            }
        }

        [Test]
        public async Task Return_Bug_With_Created_Is_Equal_To_ChangedBy()
        {
            var projectWiQuery = from bug in Client.SetOf<Bug>()
                where bug.CreatedBy == bug.ChangedBy
                select bug;
            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            foreach (var bug in result)
            {
                Check.That(bug.CreatedBy).Equals(bug.ChangedBy);
            }
        }

        [Test]
        public async Task Return_All_Workitems_With_Created_Is_Equal_To_ChangedBy()
        {
            var projectWiQuery = from bug in Client.SetOf<GenericWorkItem>()
                where bug.CreatedBy == bug.ChangedBy
                select bug;
            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            foreach (var bug in result)
            {
                Check.That(bug.CreatedBy).Equals(bug.ChangedBy);
            }
        }

        [Test]
        public async Task Return_TypedWorkitems_According_The_Process_Template()
        {
            //all workitems;
            var q = from workitem in Client.FromTemplate<ScrumTemplate>()
                select workitem;

            var result = await q.ToListAsync();

            foreach (var workItem in result.OfType<ProcessTemplates.Scrum.Bug>())
            {
                Check.That(workItem.WorkItemType).Equals("Bug");
                var prio = workItem.Priority;
            }
        }


#if false


      

        [Test]
        public void Query_With_Field_Value_As_AWI_Field_With_Field_Method()
        {
            var projectWiQuery = from workitem in Project.WorkItemSet()
                                 where workitem.CreatedBy == workitem.Field<string>("System.AssignedTo")
                                 select workitem;
            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }

        [Test]
        public void Query_With_Field_Method()
        {
            var projectWiQuery = from workitem in Project.WorkItemSet()
                                 where workitem.Title.Contains("Build")
                                 && workitem.Field<string>("System.AssignedTo") == "John Doe"
                                 select workitem;
            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }


 


#endif
    }
}