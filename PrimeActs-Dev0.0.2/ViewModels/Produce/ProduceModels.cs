#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace PrimeActs.Domain.ViewModels.Produce
{
    public class ProduceEditModel
    {
        public Guid ProduceID { get; set; }

        [Display(Name = "Related Master Group")]
        public MasterGroupEditModel RelatedMasterGroup { get; set; }

        [Display(Name = "Master Group")]
        public string SelectedMasterGroup { get; set; }

        [Display(Name = "Related ProduceGroup")]
        public ProduceGroupEditModel RelatedProduceGroup { get; set; }

        [Display(Name = "Produce Group")]
        public string SelectedProduceGroup { get; set; }

        [Display(Name = "Related ProduceIntraStat")]
        public ProduceIntraStatEditModel RelatedProduceIntraStat { get; set; }

        public string SelectedProduceIntraStat { get; set; }

        public string SelectedTransactionTax { get; set; }

        [Required]
        public string ProduceName { get; set; }

        [Required]
        public string ProduceCode { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ProduceViewModel : ProduceEditModel
    {
        [Display(Name = "ProduceGroup")]
        public ICollection<dropdownlistModel> ProduceGroupList { get; set; }

        [Display(Name = "MasterGroup")]
        public ICollection<dropdownlistModel> MasterGroupList { get; set; }

        [Display(Name = "ProduceIntraStat")]
        public ICollection<dropdownlistModel> ProduceIntraStatList { get; set; }

        [Display(Name = "VAT")]
        public ICollection<dropdownlistModel> TransactionTaxCodeList { get; set; }
    }

    public class ProducePagingModel
    {
        public ResultList<ProduceEditModel> ProduceEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class SearchObject
    {
        public string ProduceCode { get; set; }
        public string ProduceName { get; set; }
        public string ProduceNameOrCode { get; set; }
    }
}