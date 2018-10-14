using System;

namespace ObjectPatcher.Tests
{
    public class TestObject
    {
        private string privateStringField;

        public string GetPrivateStringField()
        {
            return privateStringField;
        }

        public string GetPrivateStringProperty()
        {
            return PrivateStringProperty;
        }

        public string PropertyWithGetOnly { get; }

        public string StringProperty { get; set; }

        public Guid GuidProperty { get; set; }

        public Guid? NullableGuidProperty { get; set; }

        public string StringField;

        private string PrivateStringProperty { get; set; }
    }
}