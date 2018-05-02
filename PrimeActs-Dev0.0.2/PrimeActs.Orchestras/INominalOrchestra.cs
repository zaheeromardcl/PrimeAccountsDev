#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Nominal;
using PrimeActs.Infrastructure.BaseEntities;
using System.Diagnostics;
using System.Configuration;

#endregion
namespace PrimeActs.Orchestras
{
    public interface INominalOrchestra
    {
        void Initialize(ApplicationUser principal);
        List<NominalLedgerEntry> GetNominalLedgerEntriesFilteredByDate(DateTime startDate, DateTime endDate);
    }
}