#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace PrimeActs.Domain.ViewModels.Company
{
    public class CompanyEditModel
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string TransactionTaxNo { get; set; }
        public string CompanyNo { get; set; }
        public string Logo { get; set; }
        public string Telephone { get; set; }
        public string FaxNo { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public string InvoiceInfo { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public Guid RelatedCompanyId { get; set; }
        public string RelatedCompanyName { get; set; }
        public Guid RelatedAddressId { get; set; }
        public Guid RelatedRegisteredAddressId { get; set; }

    }

    public class CompanyPagingModel
    {
        public ResultList<CompanyEditModel> CompanyEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class SearchObject
    {
        public string CompanyName { get; set; }
    }
}