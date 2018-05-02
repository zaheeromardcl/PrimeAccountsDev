#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PrimeActs.Domain.ViewModels.Division;

#endregion

namespace PrimeActs.Domain.ViewModels.Company
{
    public class CompanyEditModel
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        
        public string CompanyNo { get; set; }
        public string Logo { get; set; }
        public string Telephone { get; set; }
        public string FaxNo { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public string InvoiceInfo { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public Guid RelatedCompanyId { get; set; }
        public string RelatedCompanyName { get; set; }
        public Guid RelatedAddressId { get; set; }
        public Guid RelatedRegisteredAddressId { get; set; }
    }

    public class CompanyModel : CompanyEditModel
    {
        public List<DivisionModel> Divisions { get; set; }

        public CompanyModel()
        {
            Divisions = new List<DivisionModel>();
        }
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