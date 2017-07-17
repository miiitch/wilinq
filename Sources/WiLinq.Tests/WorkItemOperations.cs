using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using NUnit.Framework;
using WiLinq.LinqProvider;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.Tests
{

    [TestFixture]
    public class WorkItemOperations : TestFixtureBase
    {
        private WorkItemType _featureType;


        [Test]
        public async Task CreateWorkitem()
        {
            var bug = Project.New<Bug>();

            bug.Title = "New bug created during test";

            await Client.Save(bug);

        }


#if false
        private class Feature : WorkItemBase
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