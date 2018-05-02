#region

using System;
using sys = System.Runtime.Caching;

#endregion

namespace PrimeActs.Infrastructure.Cache
{
    /// <summary>
    ///     <see cref="ICache" /> implementation using .NET MemoryCache as the backing cache
    /// </summary>
    /// <remarks>
    ///     Uses CacheConfiguration setting "defaultCacheName" to determine the cache name.
    ///     Defaults to "Sixeyed.Core.Cache" if not set
    /// </remarks>
    public class MemoryCache : CacheBase
    {
        private static sys.MemoryCache _cache;

        static MemoryCache()
        {
            _cache = new sys.MemoryCache("PAndI");
        }

        public DateTime AbsoluteExpiry { get; set; }
        public TimeSpan SlidingExpiry { get; set; }

        /// <summary>
        ///     Returns the cache type
        /// </summary>
        public override CacheType CacheType
        {
            get { return CacheType.Memory; }
        }

        protected override void SetInternal(string key, object value)
        {
            var policy = new sys.CacheItemPolicy();
            Set(key, value, policy);
        }

        /// <summary>
        ///     Insert or update a cache value with a fixed lifetime
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="lifespan"></param>
        protected override void SetInternal(string key, object value, TimeSpan lifespan)
        {
            var policy = new sys.CacheItemPolicy {SlidingExpiration = lifespan};
            Set(key, value, policy);
        }

        /// <summary>
        ///     Insert or update a cache value with an expiry date
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresAt"></param>
        protected override void SetInternal(string key, object value, DateTime expiresAt)
        {
            var policy = new sys.CacheItemPolicy {AbsoluteExpiration = expiresAt};
            Set(key, value, policy);
        }

        private void Set(string key, object value, sys.CacheItemPolicy policy)
        {
            _cache.Set(key, value, policy);
        }

        /// <summary>
        ///     Retrieve a value from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Cached value or null</returns>
        protected override object GetInternal(string key)
        {
            return _cache[key];
        }

        protected override bool ExistsInternal(string key)
        {
            return _cache.Contains(key);
        }

        protected override void RemoveInternal(string key)
        {
            if (Exists(key))
            {
                _cache.Remove(key);
            }
        }

        public override void Initialise()
        {
            if (_cache == null)
            {
                //Log.Debug("MemoryCache.Initialise - initialising with cacheName: {0}", CacheConfiguration.Current.DefaultCacheName);
                // _cache = new sys.MemoryCache(CacheConfiguration.Current.DefaultCacheName);
                _cache = sys.MemoryCache.Default;
            }
        }
    }
}