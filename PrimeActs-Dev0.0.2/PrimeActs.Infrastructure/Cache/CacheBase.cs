#region

using System;

#endregion

namespace PrimeActs.Infrastructure.Cache
{
    public abstract class CacheBase : ICache
    {
        private static NullCache _nullCache = new NullCache();
        private CacheBase _current;

        private CacheBase Current
        {
            get
            {
                //return CacheConfiguration.Current.Enabled ? this : _nullCache;
                return this;
            }
        }

        public abstract CacheType CacheType { get; }
        public abstract void Initialise();

        //public void Initialise()
        //{
        //    try
        //    {
        //        InitialiseInternal();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Error("CacheBase.Initialise - failed, NullCache will be used. CacheName: {0}, Message: {1}", CacheConfiguration.Current.DefaultCacheName, ex.Message);
        //        _current = _nullCache;
        //    }
        //}

        public void Set(string key, object value)
        {
            Current.SetInternal(key, value);
        }

        public void Set(string key, object value, DateTime expiresAt)
        {
            Current.SetInternal(key, value, expiresAt);
        }

        public void Set(string key, object value, TimeSpan validFor)
        {
            Current.SetInternal(key, value, validFor);
        }

        public object Get(string key)
        {
            return Current.GetInternal(key);
        }

        public void Remove(string key)
        {
            Current.RemoveInternal(key);
        }

        public bool Exists(string key)
        {
            return Current.ExistsInternal(key);
        }

        protected abstract void SetInternal(string key, object value);
        protected abstract void SetInternal(string key, object value, DateTime expiresAt);
        protected abstract void SetInternal(string key, object value, TimeSpan validFor);
        protected abstract object GetInternal(string key);
        protected abstract void RemoveInternal(string key);
        protected abstract bool ExistsInternal(string key);
    }
}