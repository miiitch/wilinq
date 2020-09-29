using System;
using WiLinq.LinqProvider;
using WiLinq.ProcessTemplates.Scrum;

namespace WiLinq.Tests
{
    static class CustomFieldsName
    {
        public const string SessionId = "Custom.WiqlTestsTestSessionId";
        public const string DiscoveryDate = "Custom.WiqlTestsTestDiscoveryDate";
    }

    [WorkItemType("Bug")]
    public class TestBug : Bug
    {
        [Field(CustomFieldsName.SessionId)]
        public virtual string TestSessionId
        {
            get => GetRefField<string>(CustomFieldsName.SessionId);
            set => SetRefField(CustomFieldsName.SessionId, value);
        }

        [Field(CustomFieldsName.DiscoveryDate)]
        public virtual DateTime? DiscoveryDate
        {
            get => GetStructField<DateTime>(CustomFieldsName.DiscoveryDate);
            set => SetStructField(CustomFieldsName.DiscoveryDate, value);
        }

    }

    [WorkItemType("Product Backlog Item")]
    public class TestPBI : ProductBacklogItem
    {
        [Field(CustomFieldsName.SessionId)]
        public virtual string TestSessionId
        {
            get => GetRefField<string>(CustomFieldsName.SessionId);
            set => SetRefField(CustomFieldsName.SessionId, value);
        }
    }

}