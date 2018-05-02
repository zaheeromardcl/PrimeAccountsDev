#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Validation;

#endregion

namespace PrimeActs.Orchestras
{
    public interface ICountryOrchestra
    {
        List<CountryEditModel> GetCountryModelsForAC(string search);
        void Initialize1(ApplicationUser principal);
    }
}