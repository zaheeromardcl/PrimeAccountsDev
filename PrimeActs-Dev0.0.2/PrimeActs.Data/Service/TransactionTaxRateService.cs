#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ITransactionTaxRateService : IService<TransactionTaxRate>
    {

        List<TransactionTaxRate> TransactionTaxRateByTaxCodeID(Guid Id);
      
    }

    public class TransactionTaxRateService : Service<TransactionTaxRate>, ITransactionTaxRateService
    {
        
        private readonly IRepositoryAsync<TransactionTaxRate> _repository;


       public TransactionTaxRateService(IRepositoryAsync<TransactionTaxRate> repository)
            : base(repository)
        {
            _repository = repository;
            
        }


       public List<TransactionTaxRate> TransactionTaxRateByTaxCodeID(Guid TransactionTaxCodeID)
        {
           var sqlquery = "SELECT * FROM tlkpTransactionTaxRate INNER JOIN (SELECT MAX(StartDate) AS StartDate FROM tlkpTransactionTaxRate WHERE TransactionTaxCodeID = @pTaxCodeID AND StartDate <= GETDATE()) AS al ON tlkpTransactionTaxRate.StartDate = al.StartDate AND TransactionTaxCodeID = @pTaxCodeID"; 
           using (var context = new PrimeActs.Data.Contexts.PAndIContext())
            {
                var TaxCodeIDParameter = new System.Data.SqlClient.SqlParameter("@pTaxCodeID", TransactionTaxCodeID);
               
                var recordsReturned = context.Database.SqlQuery<TransactionTaxRate>(sqlquery, TaxCodeIDParameter).ToList();
                return recordsReturned;
            }
            //TransactionTaxRate varTransactionTaxCode_Rate =
            //         _repository.Query()("SELECT * FROM tlkpTransactionTaxRate INNER JOIN (SELECT MAX(StartDate) AS StartDate FROM tlkpTransactionTaxRate WHERE TransactionTaxCodeID = @p0 AND StartDate <= GETDATE()) AS al ON tlkpTransactionTaxRate.StartDate = al.StartDate AND TransactionTaxCodeID = @p0",TransactionTaxCodeID)
            //         .SingleOrDefault();

           
        }
      




    }
}
