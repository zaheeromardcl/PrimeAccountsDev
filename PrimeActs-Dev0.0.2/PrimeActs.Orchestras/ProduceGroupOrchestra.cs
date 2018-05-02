using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using System.Security.Principal;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Produce;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;

namespace PrimeActs.Orchestras
{
    public class ProduceGroupOrchestra : IProduceGroupOrchestra
    {
        private readonly IProduceGroupService _produceGroupService;
        private ApplicationUser _principal;
        private IValidationDictionary _validationDictionary;
        private readonly string _serverCode;

         public ProduceGroupOrchestra(ISetupLocalService setupLocalService, IProduceGroupService produceGroupService)
            // ,IProduceIntraStatService produceIntraStatService)
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _produceGroupService = produceGroupService;
        }
         public void Initialize1(ApplicationUser principal)
         {
             _principal = principal;
         }
         public void Initialize(IValidationDictionary validationDictionary)
         {
             _validationDictionary = validationDictionary;
         }

         //public List<ProduceGroupEditModel> GetProduceModelsForAC(string search)
         //{
             
         //}

         private ProduceGroupEditModel BuildProduceGroupEditModelAC(ProduceGroup entity)
         {
             return new ProduceGroupEditModel
             {
                 ProduceGroupID = entity.ProduceGroupID,
                 ProduceGroupCode = entity.ProduceGroupCode,
                 ProduceGroupName = entity.ProduceGroupName + " [" + entity.ProduceGroupCode + "] ",

             };
         }

         private ProduceGroupEditModel BuildProduceGroupEditModelACSelect(ProduceGroup entity)
         {
             return new ProduceGroupEditModel
             {
                 ProduceGroupID = entity.ProduceGroupID,
                 ProduceGroupCode = entity.ProduceGroupCode,
                 ProduceGroupName = entity.ProduceGroupName ,

             };
         }

         public List<ProduceGroupEditModel> GetProduceGroupModelsForAC(string search)
         {
             return string.IsNullOrEmpty(search)
                 ? null
                 : _produceGroupService.GetAllProduceGroups()
                     .Where(x => (x.ProduceGroupCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)
                         | x.ProduceGroupName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)))
                     .Select(entity => BuildProduceGroupEditModelAC(entity))
                     .ToList();
         }

         public List<ProduceGroupEditModel> GetProduceGroupModelsForACSSelect(string search)
         {
             return string.IsNullOrEmpty(search)
                 ? null
                 : _produceGroupService.GetAllProduceGroups()
                     .Where(x => (x.ProduceGroupCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)
                         | x.ProduceGroupName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)))
                     .Select(entity => BuildProduceGroupEditModelACSelect(entity))
                     .ToList();
         }
    }
}
