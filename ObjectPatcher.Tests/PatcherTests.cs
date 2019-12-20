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
        public void Can_Patch_StringProperty_CaseInsensitive(string key)
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { key, newValue }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.StringProperty.Should().Be(newValue);
        }

        [Test]
        [TestCase("guidProperty")]
        [TestCase("GuidProperty")]
        [TestCase("GuIdProperTy")]
        public void Can_Patch_GuidProperty_CaseInsensitive(string key)
        {
            var guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { key, guid }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GuidProperty.Should().Be(guid);
        }

        [Test]
        public void Can_Patch_GuidProperty_From_JsonString()
        {
            var guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { "GuidProperty", JsonConvert.SerializeObject(guid) }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GuidProperty.Should().Be(guid);
        }

        [Test]
        [TestCase("nullableGuidProperty")]
        [TestCase("NullableGuidProperty")]
        [TestCase("NuLlableGuIdProperTy")]
        public void Can_Patch_NullableGuidProperty_CaseInsensitive(string key)
        {
            Guid? guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { key, guid }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableGuidProperty.Should().Be(guid);
        }

        [Test]
        public void Can_Patch_NullableGuidProperty_WithNullValue()
        {
            var testObject = new TestObject
            {
                NullableGuidProperty = Guid.NewGuid()
            };
            var patchDictionary = new Dictionary<string, object>
            {
                { "NullableGuidProperty", null }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableGuidProperty.Should().BeNull();
        }

        [Test]
        public void Can_Patch_NullableGuidProperty_From_JsonString()
        {
            Guid? guid = Guid.NewGuid();
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { "NullableGuidProperty", JsonConvert.SerializeObject(guid) }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableGuidProperty.Should().Be(guid);
        }

        [Test]
        [TestCase("dateTimeProperty")]
        [TestCase("DateTimeProperty")]
        [TestCase("DaTeTiMeProperTy")]
        public void Can_Patch_DateTimeProperty_CaseInsensitive(string key)
        {
            var dateTime = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { key, dateTime }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.DateTimeProperty.Should().Be(dateTime);
        }

        [Test]
        public void Can_Patch_DateTimeProperty_From_JsonString()
        {
            var dateTime = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { "DateTimeProperty", JsonConvert.SerializeObject(dateTime) }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.DateTimeProperty.Should().Be(dateTime);
        }

        [Test]
        [TestCase("nullableDateTimeProperty")]
        [TestCase("NullableDateTimeProperty")]
        [TestCase("NuLlableDateTimeProperTy")]
        public void Can_Patch_NullableDateTimeProperty_CaseInsensitive(string key)
        {
            DateTime? guid = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { key, guid }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableDateTimeProperty.Should().Be(guid);
        }

        [Test]
        public void Can_Patch_NullableDateTimeProperty_WithNullValue()
        {
            var testObject = new TestObject
            {
                NullableDateTimeProperty = DateTime.UtcNow
            };
            var patchDictionary = new Dictionary<string, object>
            {
                { "NullableDateTimeProperty", null }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableDateTimeProperty.Should().BeNull();
        }

        [Test]
        public void Can_Patch_NullableDateTimeProperty_From_JsonString()
        {
            DateTime? dateTime = DateTime.UtcNow;
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { "NullableDateTimeProperty", JsonConvert.SerializeObject(dateTime) }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.NullableDateTimeProperty.Should().Be(dateTime);
        }

        [Test]
        [TestCase("stringField")]
        [TestCase("StringField")]
        [TestCase("StRingFieLd")]
        public void Can_Patch_StringField_CaseInsensitive(string key)
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { key, newValue }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.StringField.Should().Be(newValue);
        }

        [Test]
        public void Private_Field_CanNot_BePatched()
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var oldValue = testObject.GetPrivateStringField();
            var patchDictionary = new Dictionary<string, object>
            {
                { "privateStringField", newValue }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GetPrivateStringField().Should().Be(oldValue);
        }

        [Test]
        public void GetOnlyProperty_CanNot_BePatched()
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var oldValue = testObject.PropertyWithGetOnly;
            var patchDictionary = new Dictionary<string, object>
            {
                { "PropertyWithGetOnly", newValue }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.PropertyWithGetOnly.Should().Be(oldValue);
        }

        [Test]
        public void PrivateProperty_CanNot_BePatched()
        {
            const string newValue = "new value";
            var testObject = new TestObject();
            var oldValue = testObject.GetPrivateStringProperty();
            var patchDictionary = new Dictionary<string, object>
            {
                { "PrivateStringProperty", newValue }
            };
            Patcher.Apply(testObject, patchDictionary);
            testObject.GetPrivateStringProperty().Should().Be(oldValue);
        }

        [Test]
        public void Not_Exists_Field_Patching_Does_Not_Throws_Exception()
        {
            const string notExistsField = "ImNotExists";
            var testObject = new TestObject();
            var patchDictionary = new Dictionary<string, object>
            {
                { notExistsField, "some value" }
            };
            Action applyAction = () => Patcher.Apply(testObject, patchDictionary);
            applyAction.Should().NotThrow();
        }
    }
}