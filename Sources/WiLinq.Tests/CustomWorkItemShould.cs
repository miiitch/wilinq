using NFluent;
using NUnit.Framework;
using WiLinq.LinqProvider;

namespace WiLinq.Tests
{
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
}