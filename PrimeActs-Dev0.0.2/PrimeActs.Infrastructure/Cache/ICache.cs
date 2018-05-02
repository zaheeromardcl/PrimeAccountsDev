#region

using System;

#endregion

namespace PrimeActs.Infrastructure.Cache
{
    public interface ICache
    {
        CacheType CacheType { get; }
        void Initialise();
        void Set(string key, object value);
        void Set(string key, object value, DateTime expiresAt);
        void Set(string key, object value, TimeSpan validFor);
        object Get(string key);
        void Remove(string key);
        bool Exists(string key);
    }
}