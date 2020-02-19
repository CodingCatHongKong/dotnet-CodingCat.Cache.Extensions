using CodingCat.Cache.Enums;
using CodingCat.Cache.Extensions.Tests.Abstracts;
using CodingCat.Cache.Extensions.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingCat.Cache.Extensions.Tests
{
    [TestClass]
    public class TestStorageManager : BaseTest<TestStorageManager>
    {
        [TestMethod]
        public void Test_Get_Ok()
        {
            // Arrange
            var expected = new Item()
            {
                Name = nameof(Test_Get_Ok)
            };
            var usingKey = this.KeyBuilder
                .UseKey(nameof(Test_Get_Ok));

            // Act
            this.StorageManager
                .Delete(usingKey)
                .Add(usingKey, expected);
            var actual = this.StorageManager
                .Get<Item>(usingKey, FallbackPolicy.SaveFromFallback);

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
            this.StorageManager
                .Delete(usingKey);
            var actual = this.StorageManager.Get(
                usingKey,
                FallbackPolicy.SaveFromFallback,
                () => expected
            );

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
        }
    }
}