using System.ComponentModel;
using PrimeActs.Infrastructure.Caching;
using System;

namespace PrimeActs.Infrastructure.Caching
{
    public static class Cache
    {
        public static ICache Get(CacheType cacheType)
        {
            ICache cache;
            try
            {
                switch (cacheType)
                {
                    case CacheType.Memory:
                        cache = new PrimeActs.Infrastructure.Caching.MemoryCache();
                        break;
                    default:
                        cache = new PrimeActs.Infrastructure.Caching.NullCache();
                        break;

                }
                cache.Initialise();
            }
            catch (Exception ex)
            {                
                return new NullCache();
            }
            return cache;
        }

        public static ICache Memory
        {
            get
            {
                return Get(CacheType.Memory);
            }
        }       
    }
}
