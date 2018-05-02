using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Service
{
    public class PaymentTypeService : Service<PaymentType>, IPaymentTypeService
    {

        private readonly IRepositoryAsync<PaymentType> _repository;

        public PaymentTypeService(IRepositoryAsync<PaymentType> repository)
            : base(repository)
        {
            _repository = repository;

        }

        public PaymentType GetByPaymentTypeId(Guid paymentTypeId)
        {

            PaymentType varPaymentType = _repository.Query(x => x.PaymentTypeID == paymentTypeId).Select().FirstOrDefault();
            return varPaymentType;
        }

        public PaymentType GetByPaymentTypeCode(string paymentTypeCode)
        {

            PaymentType varPaymentType = _repository.Query(x => x.PaymentTypeCode == paymentTypeCode).Select().FirstOrDefault();
            return varPaymentType;
        }

        public List<PaymentType> GetAllPaymentTypes()
        {
            List<PaymentType> AllPaymentTypes = _repository.Query().Select().ToList();
            return AllPaymentTypes;
        }

        public List<PaymentType> GetAllActivePaymentTypes()
        {
            List<PaymentType> AllActivePaymentTypes = _repository.Query(x => x.IsActive == true).Select().ToList();
            return AllActivePaymentTypes;
        }


    }
}
