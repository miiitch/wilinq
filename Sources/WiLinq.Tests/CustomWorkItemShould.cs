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
        [Test]
        public void Expose_The_Right_Area_When_Created_With_The_Fill_Area_Option()
        {
            var bug = Project.New<Bug>(NewWorkItemOptions.FillAreaPath);

            Check.That(bug).IsNotNull();
        
            Check.That(bug.Area).IsEqualTo(bug.Project);
        }

        [Test]
        public void Expose_The_Right_Iteration_When_Created_With_The_Fill_Iteration_Option()
        {
            var bug = Project.New<Bug>(NewWorkItemOptions.FillIterationPath);

            Check.That(bug).IsNotNull();

            Check.That(bug.Iteration).IsEqualTo(bug.Project);
        }

    }
}