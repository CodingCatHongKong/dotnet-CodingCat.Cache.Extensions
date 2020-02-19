using CodingCat.Cache.Extensions.Tests.Abstracts;
using CodingCat.Cache.Extensions.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingCat.Cache.Extensions.Tests
{
    [TestClass]
    public class TestStorage : BaseTest<TestStorage>
    {
        [TestMethod]
        public void Test_AddThenGet_Item_Ok()
        {
            // Arrange
            var expected = new Item()
            {
                Name = nameof(Test_AddThenGet_Item_Ok)
            };
            var usingKey = this.KeyBuilder
                .UseKey(nameof(Test_AddThenGet_Item_Ok));

            // Act
            var actual = this.StorageManager
                .Delete(usingKey)
                .Add(usingKey, expected)
                .Get<Item>(usingKey);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void Test_GetOrAdd_Item_Ok()
        {
            // Arrange
            var expected = new Item()
            {
                Name = nameof(Test_GetOrAdd_Item_Ok)
            };
            var usingKey = this.KeyBuilder
                .UseKey(nameof(Test_GetOrAdd_Item_Ok));

            // Act
            var actual = this.StorageManager
                .Delete(usingKey)
                .Get(usingKey, () => expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
        }
    }
}