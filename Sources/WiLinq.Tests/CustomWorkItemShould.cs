using NFluent;
using NUnit.Framework;
using WiLinq.LinqProvider;

namespace WiLinq.Tests
{
    [TestFixture]
    public class CustomWorkItemShould : TestFixtureBase
    {
        [Test]
        [TestCase(null, null, true)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("Foo", null, true)]
        [TestCase(null, "Foo", false)]
        [TestCase("Foo\\Foo2", "Foo", true)]
        [TestCase("Foo\\", "Foo", true)]
        [TestCase("Foo", "Foo\\Foo2", false)]
        public void Compute_Area_Checks(string workItemArea, string reference, bool expected)
        {
            var bug = Project.New<TestBug>(NewWorkItemOptions.FillAreaPath);

            var areaToCheck = bug.Area;

            if (workItemArea != null)
            {
                bug.Area += "\\" + workItemArea;
            }

            if (reference != null)
            {
                areaToCheck += "\\" + reference;
            }

            var actual = bug.IsUnderArea(areaToCheck);

            Check.That(actual).Equals(expected);
        }

        [Test]
        [TestCase(null, null, true)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("Foo", null, true)]
        [TestCase(null, "Foo", false)]
        [TestCase("Foo\\Foo2", "Foo", true)]
        [TestCase("Foo\\", "Foo", true)]
        [TestCase("Foo", "Foo\\Foo2", false)]
        public void Compute_Iteration_Checks(string workItemIteration, string reference, bool expected)
        {
            var bug = Project.New<TestBug>(NewWorkItemOptions.FillIterationPath);

            var iterationToCheck = bug.Iteration;

            if (workItemIteration != null)
            {
                bug.Iteration += "\\" + workItemIteration;
            }

            if (reference != null)
            {
                iterationToCheck += "\\" + reference;
            }

            var actual = bug.IsUnderIteration(iterationToCheck);

            Check.That(actual).Equals(expected);
        }

        [Test]
        public void Expose_The_Right_Area_When_Created_With_The_Fill_Area_Option()
        {
            var bug = Project.New<TestBug>(NewWorkItemOptions.FillAreaPath);

            Check.That(bug).IsNotNull();

            Check.That(bug.Area).IsEqualTo(bug.Project);
        }

        [Test]
        public void Expose_The_Right_Iteration_When_Created_With_The_Fill_Iteration_Option()
        {
            var bug = Project.New<TestBug>(NewWorkItemOptions.FillIterationPath);

            Check.That(bug).IsNotNull();

            Check.That(bug.Iteration).IsEqualTo(bug.Project);
        }

        [Test]
        public void Expose_The_Right_Project_And_Type()
        {
            var bug = Project.New<TestBug>();

            Check.That(bug).IsNotNull();
            Check.That(bug.WorkItemType).IsEqualTo("Bug");
            Check.That(bug.Project).IsEqualTo(Project.Name);
        }
    }
}