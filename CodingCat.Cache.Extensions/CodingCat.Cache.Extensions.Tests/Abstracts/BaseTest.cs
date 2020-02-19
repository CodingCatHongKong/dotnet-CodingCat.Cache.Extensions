using CodingCat.Cache.Impls;
using CodingCat.Cache.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using MemoryStorage = CodingCat.Cache.Memory.Storage;
using RedisStorage = CodingCat.Cache.Redis.Storage;

namespace CodingCat.Cache.Extensions.Tests.Abstracts
{
    [TestClass]
    public abstract class BaseTest<T>
    {
        public KeyBuilder KeyBuilder { get; }
        public IStorageManager StorageManager { get; }

        #region Constructor(s)

        public BaseTest()
        {
            var redis = ConnectionMultiplexer
                .Connect(Constants.REDIS_CONFIG);

            this.KeyBuilder = new KeyBuilder<T>(Constants.USING_KEY_PREFIX);
            this.StorageManager = new StorageManager()
                .SetDefault(new MemoryStorage(Constants.Expiry))
                .AddFallback(new RedisStorage(
                    redis.GetDatabase(),
                    Constants.Expiry
                ));
        }

        #endregion Constructor(s)
    }
}