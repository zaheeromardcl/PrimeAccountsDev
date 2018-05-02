#region

using System;

#endregion

namespace PrimeActs.Infrastructure.Cache
{
    public class NullCache : CacheBase
    {
        public override CacheType CacheType
        {
            get { return CacheType.Null; }
        }

        public override void Initialise()
        {
        }

        protected override void SetInternal(string key, object value)
        {
        }

        protected override void SetInternal(string key, object value, DateTime expiresAt)
        {
        }

        protected override void SetInternal(string key, object value, TimeSpan validFor)
        {
        }

        protected override object GetInternal(string key)
        {
            return null;
        }

        protected override void RemoveInternal(string key)
        {
        }

        protected override bool ExistsInternal(string key)
        {
            return false;
        }
    }
}