#region

using System;
using System.Security.Principal;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface ILookupOrchestra
    {
        void Initialize(ApplicationUser principal);
        LookupLists GetLookupLists();
    }
}
