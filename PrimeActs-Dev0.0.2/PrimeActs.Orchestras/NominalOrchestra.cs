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
using AutoMapper;

#endregion

namespace PrimeActs.Orchestras
{
    public class NominalOrchestra: INominalOrchestra
    {
        private ApplicationUser _principal;
        private readonly INominalLedgerEntryService _nominalLedgerEntryService;
        private readonly IBankStatementItemNominalLedgerEntryService _bankStatementItemNominalLedgerEntryService;

        public NominalOrchestra(INominalLedgerEntryService nominalLedgerEntryService, IBankStatementItemNominalLedgerEntryService bankStatementItemNominalLedgerEntryService)
        {
            _nominalLedgerEntryService = nominalLedgerEntryService;
            _bankStatementItemNominalLedgerEntryService = bankStatementItemNominalLedgerEntryService;
        }

        public void Initialize(Domain.ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<NominalLedgerEntry> GetNominalLedgerEntriesFilteredByDate(DateTime startDate, DateTime endDate)
        {
            Guid filterAccount = Guid.Parse("76000400-0000-0070-9204-000068336078");
            //return _nominalLedgerEntryService.Query(a => a.NominalLedgerEntryDate >= startDate && a.NominalLedgerEntryDate <= endDate && a.NominalAccountID == filterAccount).Select().ToList();
            var rtnList = _nominalLedgerEntryService.Query(a => a.NominalLedgerEntryDate >= startDate && a.NominalLedgerEntryDate <= endDate && a.NominalAccountID == filterAccount).Select().ToList();
            var exceptionList = _bankStatementItemNominalLedgerEntryService.GetAll();
            //var filterlist = rtnList.Where(a => exceptionList.Any(b => b.BankStatementItemNominalLedgerEntryID != a.NominalLedgerEntryID)).ToList();
            var filterlist = rtnList.Where(a => exceptionList.All(b => b.NominalLedgerEntryID != a.NominalLedgerEntryID)).ToList();
            //var count1 = filterlist.Count;
            //var count2 = filterlist2.Count;

            //var testguid = Guid.Parse("76006627-6300-0070-9204-000068336078");

            //var test = rtnList.Where(a => a.NominalLedgerEntryID == testguid);
            //var test2 = filterlist.Where(a => a.NominalLedgerEntryID == testguid);
            //var test3 = exceptionList.Where(a => a.NominalLedgerEntryID == testguid);
            return filterlist;
        }

        public List<ReconciliationTestModel> MapLedgerEntriesToViewModel(ref List<NominalLedgerEntry> nominalLedgerEntries)
        {
             // Auto Mapper
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<NominalLedgerEntry, ReconciliationTestModel>()
                    .ForMember(dest => dest.RecId, opt => opt.MapFrom(src => src.NominalLedgerEntryID))
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.NominalLedgerEntryAmount))
                    .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.NominalLedgerEntryAmount))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.NominalLedgerEntryDate))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.NominalLedgerEntryDescription))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.NominalLedgerEntryReference))
                    .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => false))
                   );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper
            var ledgerViewModel = new List<ReconciliationTestModel>(); // Instantiaze output Statements List

            mapper.Map(nominalLedgerEntries, ledgerViewModel); // Map CSV to Output List
            return ledgerViewModel;
        }
    }
}
