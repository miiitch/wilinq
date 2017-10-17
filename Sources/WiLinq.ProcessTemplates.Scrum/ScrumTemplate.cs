using WiLinq.LinqProvider.Process;

namespace WiLinq.ProcessTemplates.Scrum
{
    public class ScrumTemplate : ProcessTemplate
    {
        public ScrumTemplate()
        {
            AddWorkItemType<Epic>();
            AddWorkItemType<Feature>();
            AddWorkItemType<ProductBacklogItem>();
            AddWorkItemType<Bug>();
            AddWorkItemType<Task>();
            AddWorkItemType<Impediment>();
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