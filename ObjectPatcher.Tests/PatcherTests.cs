using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
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
        public void CanPatchStringPropertyCaseInsensitive(string key)
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
        [TestCase("guidProperty")]
        [TestCase("GuidProperty")]
        [TestCase("GuIdProperTy")]
        public void CanPatchGuidPropertyCaseInsensitive(string key)
        {
            var guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {key, guid}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GuidProperty.Should().Be(guid);
        }

        [Test]
        public void CanPatchGuidPropertyFromJsonString()
        {
            var guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {"GuidProperty", JsonConvert.SerializeObject(guid)}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GuidProperty.Should().Be(guid);
        }

        [Test]
        [TestCase("nullableGuidProperty")]
        [TestCase("NullableGuidProperty")]
        [TestCase("NuLlableGuIdProperTy")]
        public void CanPatchNullableGuidPropertyCaseInsensitive(string key)
        {
            Guid? guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {key, guid}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableGuidProperty.Should().Be(guid);
        }

        [Test]
        public void CanPatchNullableGuidPropertyWithNullValue()
        {
            var testObject = new TestObject
            {
                NullableGuidProperty = Guid.NewGuid()
            };
            var patchDictionary = new Dictionary<string, object>
            {
                {"NullableGuidProperty", null}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableGuidProperty.Should().BeNull();
        }

        [Test]
        public void CanPatchNullableGuidPropertyFromJsonString()
        {
            Guid? guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {"NullableGuidProperty", JsonConvert.SerializeObject(guid)}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableGuidProperty.Should().Be(guid);
        }

        [Test]
        [TestCase("dateTimeProperty")]
        [TestCase("DateTimeProperty")]
        [TestCase("DaTeTiMeProperTy")]
        public void CanPatchDateTimePropertyCaseInsensitive(string key)
        {
            var dateTime = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {key, dateTime}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.DateTimeProperty.Should().Be(dateTime);
        }

        [Test]
        public void CanPatchDateTimePropertyFromJsonString()
        {
            var dateTime = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {"DateTimeProperty", JsonConvert.SerializeObject(dateTime)}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.DateTimeProperty.Should().Be(dateTime);
        }

        [Test]
        [TestCase("nullableDateTimeProperty")]
        [TestCase("NullableDateTimeProperty")]
        [TestCase("NuLlableDateTimeProperTy")]
        public void CanPatchNullableDateTimePropertyCaseInsensitive(string key)
        {
            DateTime? guid = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {key, guid}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableDateTimeProperty.Should().Be(guid);
        }

        [Test]
        public void CanPatchNullableDateTimePropertyWithNullValue()
        {
            var testObject = new TestObject
            {
                NullableDateTimeProperty = DateTime.UtcNow
            };
            var patchDictionary = new Dictionary<string, object>
            {
                {"NullableDateTimeProperty", null}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableDateTimeProperty.Should().BeNull();
        }

        [Test]
        public void CanPatchNullableDateTimePropertyFromJsonString()
        {
            DateTime? dateTime = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                {"NullableDateTimeProperty", JsonConvert.SerializeObject(dateTime)}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableDateTimeProperty.Should().Be(dateTime);
        }

        [Test]
        [TestCase("stringField")]
        [TestCase("StringField")]
        [TestCase("StRingFieLd")]
        public void CanPatchStringFieldCaseInsensitive(string key)
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

        [Test]
        public void GetOnlyPropertyCantBePatched()
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var oldValue = testObject.PropertyWithGetOnly;
            var patchDictionary = new Dictionary<string, object>
            {
                {"PropertyWithGetOnly", newValue}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.PropertyWithGetOnly.Should().Be(oldValue);
        }

        [Test]
        public void PrivatePropertyCantBePatched()
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var oldValue = testObject.GetPrivateStringProperty();
            var patchDictionary = new Dictionary<string, object>
            {
                {"PrivateStringProperty", newValue}
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GetPrivateStringProperty().Should().Be(oldValue);
        }
    }
}