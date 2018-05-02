#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public class DespatchOrchestra : IDespatchOrchestra
    {
        private readonly IDespatchService _despatchService;

        public DespatchOrchestra(IDespatchService despatchService)
        {
            _despatchService = despatchService;
        }

        //public List<DespatchEditModel> GetDespatchModelsForAC()
        //{
        //    return _despatchService.GetAllDespatches().Select(BuildDespatchEditModelAC).ToList();
        //}

        public List<DespatchEditModel> GetDespatchModelsForAC(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _despatchService.GetAllDespatches()
                    .Where(
                        x =>
                            x.DespatchLocationCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase) |
                            x.DespatchLocationName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    .Select(BuildDespatchEditModelAC)
                    .ToList();
        }

        private DespatchEditModel BuildDespatchEditModelAC(DespatchLocation entity)
        {
            return new DespatchEditModel
            {
                DespatchID = entity.DespatchLocationID,
                DespatchCode = entity.DespatchLocationCode,
                DespatchName = entity.DespatchLocationName
            };
        }
    }
}