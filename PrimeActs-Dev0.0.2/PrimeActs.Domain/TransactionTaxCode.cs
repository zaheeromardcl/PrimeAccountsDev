using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrimeActs.Domain
{
    public partial class TransactionTaxCode : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TransactionTaxCode()
        {
            
    
          
        }

        public System.Guid TransactionTaxCodeID { get; set; }
        public string TransactionTaxCodeValue { get; set; }
        public string TransactionTaxCodeDescription { get; set; }
        public System.Guid TransactionTaxLocationID { get; set; }
        public bool RateSetBySaleDate { get; set; }
        
        
       
    }
}
