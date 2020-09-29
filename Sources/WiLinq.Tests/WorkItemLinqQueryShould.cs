using System;
using System.Linq;
using NFluent;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WiLinq.LinqProvider;
using WiLinq.ProcessTemplates.Scrum;
using Task = System.Threading.Tasks.Task;

namespace WiLinq.Tests
{
    [TestFixture]
    public class WorkItemLinqQueryShould : TestFixtureBase
    {
        private int _totalTestSessionTaskCount = 0;
        private int _totalTestSessionBugCount = 0;
        private int _firstId = 0;

        public override async Task SetUp()
        {
            await base.SetUp();

            for (var i = 0; i < 10; i++)
            {
                var bug = Project.New<TestBug>(NewWorkItemOptions.FillAreaAndIterationPath);

                bug.TestSessionId = TestSessionId;
                bug.Title = $"Bug #{i}";
                bug.Priority = i % 4 + 1;
                await Client.CreateOrUpdateWorkItemAsync(bug);
                if (_firstId == 0)
                {
                    _firstId = bug.Id.Value;
                }

            };

            _totalTestSessionTaskCount += 10;


            for (var i = 0; i < 20; i++)
            {
                var pbi = Project.New<TestPBI>(NewWorkItemOptions.FillAreaAndIterationPath);

                pbi.TestSessionId = TestSessionId;
                pbi.Title = $"PBI #{i}";
                pbi.Priority = i % 4 + 1;
                await Client.CreateOrUpdateWorkItemAsync(pbi);

            };
            _totalTestSessionBugCount += 20;
        }


        [Test]
        public void Return_only_Bugs_From_The_Test_Session()
        {
            var q = from workitem in Client.SetOf<TestBug>()
                    where workitem.TestSessionId == TestSessionId
                    select workitem;


            var result = q.ToList();

            Check.That(result.Count).Equals(_totalTestSessionTaskCount);
            foreach (var workitem in result)
            {
                Check.That(workitem.TestSessionId).Equals(TestSessionId);
                Check.That(workitem.WorkItemType).Equals("Bug");
            }
        }

        [Test]
        public void Return_only_Bugs_From_The_Test_Session_With_Is_Operator()
        {
            var q = from workitem in Client.All()
                    where workitem.Is<TestBug>() && workitem.Field<string>(CustomFieldsName.SessionId) == TestSessionId
                    select workitem;


            var result = q.ToList();

            Check.That(result.Count).Equals(_totalTestSessionTaskCount);
            foreach (var workitem in result)
            {
                Check.That(workitem.Field<string>(CustomFieldsName.SessionId)).Equals(TestSessionId);
                Check.That(workitem.Field<string>(SystemField.WorkItemType)).Equals("Bug");
            }
        }

        [Test]
        public void Return_only_PBI_And_Bugs_From_The_Test_Session_With_Is_Operator()
        {
            var q = from workitem in Client.All()
                    where (workitem.Is<TestPBI>() || workitem.Is<TestBug>()) && workitem.Field<string>(CustomFieldsName.SessionId) == TestSessionId
                    select workitem;


            var result = q.ToList();

            Check.That(result.Count).Equals(_totalTestSessionTaskCount + _totalTestSessionBugCount);
            foreach (var workitem in result)
            {
                Check.That(workitem.Field<string>(CustomFieldsName.SessionId)).Equals(TestSessionId);
                Check.That(workitem.Field<string>(SystemField.WorkItemType)).IsOneOfThese("Product Backlog Item", "Bug");
            }
        }

        [Test]
        public void Return_only_PBI_And_Bugs_From_The_Test_Session_With_Is_Operator_With_Specific_Tests()
        {
            var q = from workitem in Client.All()
                    where ((workitem.Is<TestPBI>() && workitem.Field<long>("Microsoft.VSTS.Common.Priority") == 1) ||
                            (workitem.Is<TestBug>() && workitem.Field<long>("Microsoft.VSTS.Common.Priority") == 2))
                            && workitem.Field<string>(CustomFieldsName.SessionId) == TestSessionId
                    select workitem;


            var result = q.ToList();

            foreach (var workitem in result)
            {
                Check.That(workitem.Field<string>(CustomFieldsName.SessionId)).Equals(TestSessionId);
                var type = workitem.Field<string>(SystemField.WorkItemType);
                Check.That(type).IsOneOfThese("Product Backlog Item", "Bug");
                var priority = workitem.Field<long>("Microsoft.VSTS.Common.Priority");
                if (type == "Product Backlog Item")
                {
                    Check.That(priority).Equals(1);

                }
                else
                {
                    Check.That(priority).Equals(2);
                }


            }
        }

        [Test]
        public void Return_only_Tasks_And_Bugs_From_The_Test_Session_With_Tests_Involving_Not_Operator_At_Field_Operation_Level()
        {
            var q = from workitem in Client.All()
                        // ReSharper disable once NegativeEqualityExpression
                    where ((workitem.Is<TestPBI>() && !(workitem.Field<long>("Microsoft.VSTS.Common.Priority") == 1)) ||
                           (workitem.Is<TestBug>() && workitem.Field<long>("Microsoft.VSTS.Common.Priority") == 2))
                          && workitem.Field<string>(CustomFieldsName.SessionId) == TestSessionId
                    select workitem;


            var result = q.ToList();

            foreach (var workitem in result)
            {
                Check.That(workitem.Field<string>(CustomFieldsName.SessionId)).Equals(TestSessionId);
                var type = workitem.Field<string>(SystemField.WorkItemType);
                Check.That(type).IsOneOfThese("Product Backlog Item", "Bug");
                var priority = workitem.Field<long>("Microsoft.VSTS.Common.Priority");
                if (type == "Product Backlog Item")
                {
                    Check.That(priority).Not.Equals(1);

                }
                else
                {
                    Check.That(priority).Equals(2);
                }


            }
        }

        [Test]
        public void Return_only_Tasks_And_Bugs_From_The_Test_Session_With_Tests_Involving_Not_Operator_At_Boolean_Operation_Level()
        {
            var q = from workitem in Client.All()
                        // ReSharper disable once NegativeEqualityExpression
                    where !((workitem.Is<TestPBI>() && (workitem.Field<long>("Microsoft.VSTS.Common.Priority") == 1)) ||
                           (workitem.Is<TestBug>() && workitem.Field<long>("Microsoft.VSTS.Common.Priority") == 2))
                          && workitem.Field<string>(CustomFieldsName.SessionId) == TestSessionId
                    select workitem;


            var result = q.ToList();

            foreach (var workitem in result)
            {
                Check.That(workitem.Field<string>(CustomFieldsName.SessionId)).Equals(TestSessionId);
                var type = workitem.Field<string>(SystemField.WorkItemType);
                Check.That(type).IsOneOfThese("Product Backlog Item", "Bug");
                var priority = workitem.Field<long>("Microsoft.VSTS.Common.Priority");
                if (type == "Product Backlog Item")
                {
                    Check.That(priority).Not.Equals(1);

                }
                else
                {
                    Check.That(priority).Not.Equals(2);
                }


            }
        }



        [Test]
        public void Return_Only_One_Element_With_The_Right_Id()
        {
            var id1 = _firstId;
            var id2 = _firstId + 5;
            //all workitems;
            var q = from workitem in Client.All()
                    where workitem.Id == id1 || workitem.Id == id2
                    orderby workitem.Id descending
                    select workitem;

            // ReSharper disable once UnusedVariable
            var result = q.ToList();

            Check.That(result).HasSize(2);
            Check.That(result[1].Id).Equals(id1);
            Check.That(result[0].Id).Equals(id2);
        }

        [Test]
        public async Task Return_Only_One_Element_With_The_Right_Id_Async()
        {
            //all workitems;
            var q = from workitem in Client.All()
                    where workitem.Id == _firstId
                    select workitem;

            var result = await q.ToListAsync();

            Check.That(result).HasSize(1);
            Check.That(result[0].Id).Equals(_firstId);
        }

        [Test]
        public async Task Return_Only_One_Element_Of_Type_Bug_With_The_Right_Id()
        {
            //all workitems;
            var q = from workitem in Client.SetOf<TestBug>(Project)
                    where workitem.Id == _firstId
                    select workitem;

            // ReSharper disable once UnusedVariable
            var result = await q.ToListAsync();

            Check.That(result).HasSize(1);
            Check.That(result[0].Id).Equals(_firstId);
        }

        [Test]
        public async Task Return_No_Element_Of_Type_PBI_With_The_Id_Of_Bug()
        {
            //all workitems;
            var q = from workitem in Client.SetOf<TestBug>(Project)
                    where workitem.Id == _firstId+21303030
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
                                 where workitem.Id < 200
                                 select workitem;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();
        }

        [Test]
        public async Task Return_All_Collection_Workitem_Ids()
        {
            //all workitems;
            var projectWiQuery = from workitem in Client.All()
                                 where workitem.Id < 200
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
            var projectWiQuery = from workitem in Client.SetOf<TestBug>(Project)
                                 select workitem;
            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            Check.That(result).ContainsOnlyElementsThatMatch(bug => bug.WorkItemType == "Bug");
        }

        [Test]
        public async Task Return_Bugs_Of_The_Project_Created_After_A_Specific_Date()
        {
            var minCreationDate = new DateTime(2017, 7, 1);
            var projectWiQuery = from bug in Client.SetOf<TestBug>(Project)
                                 where bug.CreatedDate > minCreationDate
                                 select bug;

            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            Check.That(result).ContainsOnlyElementsThatMatch(bug => bug.WorkItemType == "Bug" && bug.CreatedDate > minCreationDate);
        }

        [Test]
        public async Task Return_bugs_Under_A_Specific_Iteration()
        {
            var iteration = $@"{Project.Name}\Sprint 1";

            var projectWiQuery = from bug in Client.SetOf<TestBug>(Project)
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
            var projectWiQuery = from bug in Client.SetOf<TestBug>()
                                 where bug.CreatedBy == bug.ChangedBy
                                 select bug;
            // ReSharper disable once UnusedVariable
            var result = await projectWiQuery.ToListAsync();

            Check.That(result).Not.IsEmpty();
            foreach (var bug in result)
            {
                Check.That(bug.CreatedBy.Id).IsEqualTo(bug.ChangedBy.Id);
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
                Check.That(bug.CreatedBy.Id).IsEqualTo(bug.ChangedBy.Id);
            }
        }

        [Test]
        public async Task Return_TypedWorkitems_According_The_Process_Template()
        {
            //all workitems;
            var q = from workitem in Client.FromTemplate<ScrumTemplate>()
                    select workitem;

            var result = await q.ToListAsync();

            foreach (var workItem in result.OfType<Bug>())
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