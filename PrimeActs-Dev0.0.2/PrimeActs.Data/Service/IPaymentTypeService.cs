using PrimeActs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeActs.Data.Service
{
    public interface IPaymentTypeService : IService<PaymentType>
    {
        PaymentType GetByPaymentTypeId(Guid id);
        PaymentType GetByPaymentTypeCode(string paymentTypeCode);
        List<PaymentType> GetAllPaymentTypes();
        List<PaymentType> GetAllActivePaymentTypes();
     
    }
}
