using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectPatcher.Tests
{
    [TestFixture]
    public class PatcherTests
    {
        [Test]
        [TestCase("stringProperty")]
        [TestCase("StringProperty")]
        [TestCase("StRingProperTy")]
        public void CanPatchStringPropertyWithDifferentCases(string key)
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {key, newValue}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.StringProperty.Should().Be(newValue);
        }

        [Test]
        [TestCase("stringField")]
        [TestCase("StringField")]
        [TestCase("StRingFieLd")]
        public void CanPatchStringFieldWithDifferentCases(string key)
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {key, newValue}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.StringField.Should().Be(newValue);
        }

        [Test]
        public void PrivateFieldCantBePatched()
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var oldValue = testObject.GetPrivateStringField();
            var patchDictionary = new Dictionary<string, object>
            {
                {"privateStringField", newValue}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GetPrivateStringField().Should().Be(oldValue);
        }
    }
}