using WiLinq.LinqProvider.Process;

namespace WiLinq.ProcessTemplates.Agile
{
    public class AgileTemplate : ProcessTemplate
    {
        public AgileTemplate()
        {
            AddWorkItemType<Epic>();
            AddWorkItemType<Feature>();
            AddWorkItemType<UserStory>();
            AddWorkItemType<Bug>();
            AddWorkItemType<Task>();
            AddWorkItemType<Issue>();
            AddWorkItemType<FeedbackRequest>();
            AddWorkItemType<FeedbackResponse>();
            AddWorkItemType<TestSuite>();
            AddWorkItemType<TestPlan>();
            AddWorkItemType<TestCase>();
            AddWorkItemType<SharedSteps>();
            AddWorkItemType<SharedParameter>();
            AddWorkItemType<CodeReviewRequest>();
            AddWorkItemType<CodeReviewResponse>();
        }
    }
}