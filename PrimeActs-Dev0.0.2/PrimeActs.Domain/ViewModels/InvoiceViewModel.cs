#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class SalesInvoiceEditModel
    {
        public Guid SalesInvoiceID { get; set; }

        [Display(Name = "Related Customer")]
        public CustomerEditModel RelatedCustomer { get; set; }

        [Display(Name = "Related Salesperson")]
        public string SelectedSalesperson { get; set; }

        [Display(Name = "Related ProduceGroup")]
        public ProduceGroupEditModel RelatedProduceGroup { get; set; }

        [Display(Name = "Produce Group")]
        public string SelectedProduceGroup { get; set; }

        [Display(Name = "Related ProduceIntraStat")]
        public ProduceIntraStatEditModel RelatedProduceIntraStat { get; set; }

        public string SelectedProduceIntraStat { get; set; }
        public string SelectedVAT { get; set; }

        [Required]
        public string ProduceName { get; set; }

        [Required]
        public string ProduceCode { get; set; }

      //  public bool IsActive { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }

    public class SalesInvoiceViewModel : SalesInvoiceEditModel
    {
        [Display(Name = "ProduceGroup")]
        public ICollection<dropdownlistModel> ProduceGroupList { get; set; }

        [Display(Name = "MasterGroup")]
        public ICollection<dropdownlistModel> MasterGroupList { get; set; }

        [Display(Name = "ProduceIntraStat")]
        public ICollection<dropdownlistModel> ProduceIntraStatList { get; set; }

        [Display(Name = "VAT")]
        public ICollection<dropdownlistModel> TransactionTaxList { get; set; }
    }
}