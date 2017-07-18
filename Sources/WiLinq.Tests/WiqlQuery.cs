using System;
using System.Linq;
using NFluent;
using NUnit.Framework;
using WiLinq.LinqProvider;
using WiLinq.LinqProvider.Extensions;
using System.Threading.Tasks;

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
        public async Task ProjectQueryAllWorkitems()
        {
            //all workitems;
            var projectWiQuery = from workitem in Client.All(Project)
                                 select workitem;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
        }

        [Test]
        public async Task AllWorkitems()
        {
            //all workitems;
            var projectWiQuery = from workitem in Client.All()
                                 select workitem;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
        }

        [Test]
        public async Task Query_With_QueryConstant_Me()
        {
            var projectWiQuery = from workitem in Client.All()
                                 where workitem.Field<string>("System.AssignedTo") == QueryConstant.Me
                                 select workitem;
            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
        }
        [Test]
        public async Task Query_With_Date()
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
                Check.That(createdDate.Value).IsAfter(minCreationDate);
            }
        }


#if false


        [Test]
        public void Query_With_Field_Value_As_A_WI_Field()
        {
            var projectWiQuery = from workitem in Project.WorkItemSet()
                                 where workitem.CreatedBy == workitem.ChangedBy
                                 select workitem;
            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }

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

    [TestFixture]
    public class CustomWorkItemShould : TestFixtureBase
    {
        [Test]
        public void Expose_The_Right_Project_And_Type()
        {
            var bug = Project.New<Bug>();

            Check.That(bug).IsNotNull();
            Check.That(bug.WorkItemType).IsEqualTo("Bug");
            Check.That(bug.Project).IsEqualTo(Project.Name);


        }
    }

    [WorkItemType("Bug")]
    public class Bug : WorkItemBase
    {
        public Bug()
        {

        }
    }

    [WorkItemType("Product Backlog Item")]
    public class PBI : WorkItemBase
    {
        public PBI()
        {

        }
    }
}
