#region

using System;

#endregion

namespace PrimeActs.Orchestras
{
    public sealed class ManageEventOrchestra
    {
        private static readonly Lazy<ManageEventOrchestra> lazy =
            new Lazy<ManageEventOrchestra>(() => new ManageEventOrchestra());

        private ManageEventOrchestra()
        {
        }

        public static ManageEventOrchestra Instance
        {
            get { return lazy.Value; }
        }

        public static bool IsManageEventOrchestraCreated()
        {
            return lazy.IsValueCreated;
        }
    }
}