using System.Linq;
using NFluent;
using NUnit.Framework;
using WiLinq.LinqProvider;

namespace WiLinq.Tests
{
    [TestFixture]
    public class WiqlQuery : TestFixtureBase
    {
        [Test]
        // ReSharper disable once InconsistentNaming
        public void TPCQuery()
        {
            //all workitems;
            var allWiQuery = from workitem in Client.WorkItemSet()
                             where workitem.Id == 3
                             select workitem;

            // ReSharper disable once UnusedVariable
            var result = allWiQuery.ToList();

            Check.That(result).HasSize(1);
            Check.That(result[0].Id).Equals(3);
        }
#if false
        [Test]
        public void ProjectQueryAllWorkitems()
        {
            //all workitems;
            var projectWiQuery = from workitem in Project.WorkItemSet()
                                 select workitem;

            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }

        [Test]
        public void ProjectQuery()
        {
            //all workitems;
            var projectWiQuery = from workitem in Project.WorkItemSet()
                                 where workitem.Id == 3
                                 select workitem;

            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }

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


        [Test]
        public void Query_With_QueryConstant_Me()
        {
            var projectWiQuery = from workitem in Project.WorkItemSet()
                                 where workitem.CreatedBy == QueryConstant.Me
                                 select workitem;
            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }


#endif
    }
}
