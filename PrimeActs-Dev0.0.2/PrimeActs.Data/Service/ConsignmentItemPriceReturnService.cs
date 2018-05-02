using System;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class ConsignmentItemPriceReturnService : Service<ConsignmentItemPriceReturn>, IConsignmentItemPriceReturnService
    {
        private readonly IRepositoryAsync<ConsignmentItemPriceReturn> _repository;

        public ConsignmentItemPriceReturnService(IRepositoryAsync<ConsignmentItemPriceReturn> repository)
        : base(repository)
        {
            _repository = repository;
        }

        public ConsignmentItemPriceReturn ConsignmentItemPriceReturnByID(Guid ID)
        {
            return
                _repository.Query(x => x.ConsignmentItemPriceReturnID == ID)
                    .Select()
                    .FirstOrDefault();
        }
    }
}
