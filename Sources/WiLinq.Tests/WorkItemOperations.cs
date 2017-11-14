using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;
using WiLinq.LinqProvider;

namespace WiLinq.Tests
{
    [TestFixture]
    public class WorkitemClientShould : TestFixtureBase
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