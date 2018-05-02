#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface ICurrencyOrchestra
    {
      

         
      List<CurrencyEditModel> GetCurrencyModelsForAC(string search);
        //void Initialize(PrimeActs.Infrastructure.Validation.IValidationDictionary validationDictionary);
      
    }
}