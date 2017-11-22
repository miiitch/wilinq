using System;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;
using WiLinq.LinqProvider;

namespace WiLinq.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class WorkitemClientShould: TestFixtureBase
    {
        [Test]
        public async Task Create_A_Workitem_Then_Save_It()
        {
            var bug = Project.New<TestBug>(NewWorkItemOptions.FillAreaAndIterationPath);

            bug.Title = "New bug created during test";

            await Client.CreateOrUpdateWorkItemAsync(bug);

            Check.That(bug.Id).HasAValue();
        }

        [Test]
        public async Task Create_A_Workitem_Then_Save_It_And_Get_It()
        {
            var bug = Project.New<TestBug>(NewWorkItemOptions.FillAreaAndIterationPath);

            bug.Title = "New bug created during test";

            await Client.CreateOrUpdateWorkItemAsync(bug);

            var sameBug = await Client.GetWorkItemAsync<TestBug>(bug.Id.Value);

            Check.That(sameBug.Id).HasAValue();
            Check.That(sameBug.Id.Value).Equals(bug.Id.Value);
            Check.That(sameBug.Title).Equals(bug.Title);
            Check.That(sameBug.Area).Equals(bug.Area);
            Check.That(sameBug.Iteration).Equals(bug.Iteration);


        }

        [Test]
        public async Task Create_A_Workitem_With_Iteration_And_Area_Then_Save_It()
        {
            var bug = Project.New<TestBug>(NewWorkItemOptions.FillAreaAndIterationPath);

            bug.Title = "New bug created during test with area and iteration path";

            await Client.CreateOrUpdateWorkItemAsync(bug);

            Check.That(bug.Id).HasAValue();
        }


        [Test]
        public async Task Create_A_Workitem_And_Save_It_And_Update_It()
        {
            var bug = Project.New<TestBug>();

            bug.Title = "New bug created during test";

            await Client.CreateOrUpdateWorkItemAsync(bug);

            bug.Title = "Title Changed";

            await Client.CreateOrUpdateWorkItemAsync(bug);
        }

        [Test]
        public async Task Create_A_Workitem_And_Check_The_Floating_Point_Value()
        {
            const double expectedRemainingWork = 45.6;
            var task = Project.New<TestTask>();

            task.Title = "New task created during test";

            task.RemainingWork = expectedRemainingWork;
            await Client.CreateOrUpdateWorkItemAsync(task);
            Check.That(task.RemainingWork).HasAValue();
            Check.That(task.RemainingWork.Value).Equals(expectedRemainingWork);


        }

    

        [Test]
      
        public async Task Create_A_Workitem_And_Update_It_And_Check_The_Floating_Point_Value()
        {           
            const double expectedRemainingWork = 45.6;
            var task = Project.New<TestTask>();

            task.Title = "New task created during test";
            task.RemainingWork = 60000000.9;

            await Client.CreateOrUpdateWorkItemAsync(task);
            task.RemainingWork = expectedRemainingWork;
            await Client.CreateOrUpdateWorkItemAsync(task);
            Check.That(task.RemainingWork).HasAValue();
            Check.That(task.RemainingWork.Value).Equals(expectedRemainingWork);
        }

        [Test]

        public async Task Create_A_Workitem_And_Check_The_Date()
        {
          
            var bug = Project.New<TestBug>();

            bug.Title = "New bug created during test";
            var bugDiscoveryDate = new DateTime(2010,3,5);

            bug.DiscoveryDate = bugDiscoveryDate;

            await Client.CreateOrUpdateWorkItemAsync(bug);

            Check.That(bug.DiscoveryDate.HasValue).IsTrue();
            Check.That(bug.DiscoveryDate.Value).Equals(bugDiscoveryDate);

            
        }

        [Test]
        public async Task Create_A_Workitem_And_Update_It_and_Check_The_Date()
        {

            var bug = Project.New<TestBug>();

            bug.Title = "New bug created during test";
            var bugDiscoveryDate = new DateTime(2010, 3, 5);

            bug.DiscoveryDate = bugDiscoveryDate.AddDays(48);

            await Client.CreateOrUpdateWorkItemAsync(bug);
            bug.DiscoveryDate = bugDiscoveryDate;

            await Client.CreateOrUpdateWorkItemAsync(bug);

            Check.That(bug.DiscoveryDate.HasValue).IsTrue();
            Check.That(bug.DiscoveryDate.Value).Equals(bugDiscoveryDate);
        }


#if false
        private class Feature : GenericWorkItem
        {


            [Field("Microsoft.VSTS.Scheduling.TargetDate")]
            public DateTime? TargetDate
            {
                get => this.WorkItem.Field<DateTime?>("Microsoft.VSTS.Scheduling.TargetDate");
                set => this.WorkItem.SetField<DateTime?>("Microsoft.VSTS.Scheduling.TargetDate", value);
            }

            [Field("Microsoft.VSTS.Common.BusinessValue")]
            public int? BusinessValue
            {
                get => this.WorkItem.Field<int?>("Microsoft.VSTS.Common.BusinessValue");
                set => this.WorkItem.SetField<int?>("Microsoft.VSTS.Common.BusinessValue", value);
            }



        }


        public override Task SetUp()
        {
            return base.SetUp();

           // _featureType = Project.WorkItemTypes.Cast<WorkItemType>().First(_ => _.Name == "Feature");
        }

        [Test]
        public void DateTime_Read_Write()
        {
            _featureType.NewWorkItem();
            DateTime? expectedValue = DateTime.Now;
            var wi = new Feature()
            {
                WorkItem = _featureType.NewWorkItem()
            };

            wi.TargetDate = expectedValue;



            Assert.AreEqual(expectedValue, wi.TargetDate);


        }


        [Test]
        public void Integer_Read_Write()
        {
            _featureType.NewWorkItem();
            int? expectedValue = 100;
            var wi = new Feature()
            {
                WorkItem = _featureType.NewWorkItem()
            };

            wi.BusinessValue = expectedValue;



            Assert.AreEqual(expectedValue, wi.BusinessValue);


        }
#endif
    }
}