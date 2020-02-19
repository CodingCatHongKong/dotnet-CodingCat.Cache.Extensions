using CodingCat.Cache.Enums;
using CodingCat.Cache.Interfaces;
using CodingCat.Serializers.Impls;
using CodingCat.Serializers.Interfaces;
using System;

namespace CodingCat.Cache.Extensions
{
    public static class IStorageManagerExtensions
    {
        public static T Get<T>(
            this IStorageManager storageManager,
            IKeyBuilder key,
            FallbackPolicy fallbackPolicy,
            ISerializer<T, string> serializer = null
        )
        {
            var serialized = storageManager.Get(key, fallbackPolicy);
            return (serializer ?? GetDefaultSerializer<T>())
                .Deserialize(serialized);
        }

        public static T Get<T>(
            this IStorageManager storageManager,
            IKeyBuilder key,
            FallbackPolicy fallbackPolicy,
            Func<T> callback,
            ISerializer<T, string> serializer = null
        )
        {
            serializer = serializer ?? GetDefaultSerializer<T>();

            var item = default(T);
            var serialized = storageManager.Get(key, fallbackPolicy);
            if (serialized == null)
            {
                item = callback();
                serialized = serializer.Serialize(item);
                storageManager.Add(key, serialized);
            }
            else item = serializer.Deserialize(serialized);

            return item;
        }

        private static ISerializer<T, string> GetDefaultSerializer<T>()
        {
            return new JsonSerializer<T>();
        }
    }
}