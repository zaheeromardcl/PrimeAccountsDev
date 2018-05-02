#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public class ConsignmentItemArrivalService : Service<ConsignmentItemArrival>, IConsignmentItemArrivalService
    {
        private readonly IRepositoryAsync<ConsignmentItemArrival> _repository;

        public ConsignmentItemArrivalService(IRepositoryAsync<ConsignmentItemArrival> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public List<ConsignmentItemArrival> ConsignmentItemArrivalsByConsignmentItemID(Guid ID)
        {
            return
                _repository.Query()                    
                    .Include(inc => inc.Note)
                    .Include(inc => inc.ConsignmentItem)                    
                    .Select()
                    .Where(wh => wh.ConsignmentItemID == ID)
                    .ToList();
        }

        public List<ConsignmentItemArrival> ConsignmentItemArrivalsOnlyByConsignmentItemID(Guid ID)
        {
            return
                _repository.Query()
                    .Select()
                    .Where(wh => wh.ConsignmentItemID == ID)
                    .ToList();
        }

        // Performance optimised to use raw SQL, required for Disection Report
        public List<ConsignmentItemArrival> ConsignmentItemArrivalsOnlyByConsignmentItemIDSQL(Guid ID)
        {
            var qry = " select ConsignmentItemArrivalID, NoteID, ConsignmentItemID,ConsignmentItemArrivalDate,QuantityReceived,StockLocationID,UpdatedDate,CreatedDate,CreatedByUserID as CreatedBy,UpdatedByUserID as UpdatedBy from tblConsignmentItemArrival where ConsignmentItemID = @p0";
            var @p0 = ID.ToString();
            var consignmentItemArrivalItems =
                _repository.SelectQuery(qry, p0);
            return consignmentItemArrivalItems.ToList();

        }

        public ConsignmentItemArrival ConsignmentItemArrivalByID(Guid ID)
        {
            return
                _repository.Query()                    
                    .Include(inc => inc.Note)
                    .Include(inc => inc.ConsignmentItem)                    
                    .Select()
                    .Where(wh => wh.ConsignmentItemArrivalID == ID)
                    .FirstOrDefault();
        }

        public ConsignmentItemArrival ConsignmentItemArrivalByIDSimple(Guid ID)
        {
            return
                _repository.Query()
                    .Select()
                    .Where(wh => wh.ConsignmentItemArrivalID == ID)
                    .FirstOrDefault();
        }

        public List<ConsignmentItemArrival> GetAllConsignmentItemArrivals()
        {
            return
                _repository.Query()                   
                    .Include(inc => inc.Note)
                    .Include(inc => inc.ConsignmentItem)                    
                    .Select()
                    .ToList();
        }
    }
}
