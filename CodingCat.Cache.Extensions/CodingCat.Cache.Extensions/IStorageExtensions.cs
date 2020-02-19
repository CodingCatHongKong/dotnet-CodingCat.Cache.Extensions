using CodingCat.Cache.Interfaces;
using CodingCat.Serializers.Impls;
using CodingCat.Serializers.Interfaces;
using System;

namespace CodingCat.Cache.Extensions
{
    public static class IStorageExtensions
    {
        public static IStorage Add<T>(
            this IStorage storage,
            IKeyBuilder key,
            T value,
            ISerializer<T, string> serializer = null
        )
        {
            var serialized = (serializer ?? GetDefaultSerializer<T>())
                .Serialize(value);
            return storage.Add(key, serialized);
        }

        public static T Get<T>(
            this IStorage storage,
            IKeyBuilder key,
            ISerializer<T, string> serializer = null
        )
        {
            var serialized = storage.Get(key);
            return (serializer ?? GetDefaultSerializer<T>())
                .Deserialize(serialized);
        }

        public static T Get<T>(
            this IStorage storage,
            IKeyBuilder key,
            Func<T> callback,
            ISerializer<T, string> serializer = null
        )
        {
            serializer = serializer ?? GetDefaultSerializer<T>();

            var serialized = storage
                .Get(
                    key,
                    () => serializer.Serialize(callback())
                );
            return serializer.Deserialize(serialized);
        }

        private static ISerializer<T, string> GetDefaultSerializer<T>()
        {
            return new JsonSerializer<T>();
        }
    }
}