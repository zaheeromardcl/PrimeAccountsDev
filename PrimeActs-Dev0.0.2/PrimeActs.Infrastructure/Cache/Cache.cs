#region

using System;

#endregion

namespace PrimeActs.Infrastructure.Cache
{
    public static class Cache
    {
        public static ICache Default
        {
            get
            {
                //return Get(CacheConfiguration.Current.DefaultCacheType);
                return Get(CacheType.Memory);
            }
        }

        public static ICache Memory
        {
            get { return Get(CacheType.Memory); }
        }

        public static ICache Get(CacheType cacheType)
        {
            ICache cache;
            try
            {
                switch (cacheType)
                {
                    case CacheType.Memory:
                        cache = new MemoryCache();
                        break;
                    default:
                        cache = new NullCache();
                        break;
                }
                cache.Initialise();
            }
            catch (Exception ex)
            {
                //Log.Warn("Failed to instantiate cache of type: {0}, using null cache. Exception: {1}", cacheType, ex);
                return new NullCache();
            }
            return cache;
        }
    }
}