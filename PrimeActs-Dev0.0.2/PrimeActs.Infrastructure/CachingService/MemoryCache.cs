using System;
using PrimeActs.Infrastructure.Caching;

using sys = System.Runtime.Caching;

namespace PrimeActs.Infrastructure.Caching
{
    public class MemoryCache : CacheBase
    {
        private sys.MemoryCache _cache;
        public DateTime AbsoluteExpiry { get; set;}
        public TimeSpan SlidingExpiry { get; set; }

        public override CacheType CacheType
        {
            get { return CacheType.Memory; }
        }

        protected override void SetInternal(string key, object value)
        {
            var policy = new sys.CacheItemPolicy();
            Set(key, value, policy);
        }

        protected override void SetInternal(string key, object value, TimeSpan lifespan)
        {
            var policy = new sys.CacheItemPolicy {SlidingExpiration = lifespan};
            Set(key, value, policy);
        }

        protected override void SetInternal(string key, object value, DateTime expiresAt)
        {
            var policy = new sys.CacheItemPolicy {AbsoluteExpiration = expiresAt};
            Set(key, value, policy);
        }

        private void Set(string key, object value, sys.CacheItemPolicy policy)
        {
            _cache.Set(key, value, policy);
        }

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
                _cache = sys.MemoryCache.Default;
            }
        }
    }
}
