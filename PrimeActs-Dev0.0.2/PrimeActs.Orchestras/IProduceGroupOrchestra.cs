using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Produce;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Validation;

namespace PrimeActs.Orchestras
{
    public interface IProduceGroupOrchestra
    {
        List<ProduceGroupEditModel> GetProduceGroupModelsForAC(string search);
        List<ProduceGroupEditModel> GetProduceGroupModelsForACSSelect(string search);
        void Initialize1(ApplicationUser principal);
        void Initialize(IValidationDictionary validationDictionary);
    }
}
