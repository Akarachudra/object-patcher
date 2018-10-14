namespace ObjectPatcher.Tests
{
    public class TestObject
    {
        private string privateStringField;

        public string GetPrivateStringField()
        {
            return privateStringField;
        }

        public string StringProperty { get; set; }

        public string StringField;
    }
}