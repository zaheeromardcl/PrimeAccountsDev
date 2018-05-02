#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IBatchNumberLogService : IService<BatchNumberLog>
    {
        BatchNumberLog GiveLatestBatchNumber(BatchNumberLog batchNumberLog);
        Guid GetBatchNumberLogIDByDivisionID(Guid divisionID);
    }

    public class BatchNumberLogService : Service<BatchNumberLog>, IBatchNumberLogService
    {
        private readonly IRepositoryAsync<BatchNumberLog> _repository;

        private Dictionary<Guid, Guid> _divisionIDBatchLogIDCache; 
        
        private readonly object lockboject = new object();

        public BatchNumberLogService(IRepositoryAsync<BatchNumberLog> repository)
            : base(repository)
        {
            _repository = repository;
            _divisionIDBatchLogIDCache = new Dictionary<Guid, Guid>();
        }

        // TODO this method is an old method of getting the bactchlogID and it is still used in InvoiceOrchestra but it should not be used anymore
        // we introduced the new method GetBatchNumberLogIDByDivisionID to create salesledgerentries for tickets and this is the right way to get the batchLogID
        public BatchNumberLog GiveLatestBatchNumber(BatchNumberLog batchNumberLog)
        {
            lock (lockboject)
            {
                var totalCount = 0;
                var latestBatchNumberLog =
                    _repository.Query(x => x.ServerPrefix == batchNumberLog.ServerPrefix)
                        .OrderBy(ord => ord.OrderByDescending(od => od.CreatedDate))
                        .SelectPage(1, 1, out totalCount).SingleOrDefault().BatchNumber;
                batchNumberLog.BatchNumber = latestBatchNumberLog + 1;
                Insert(batchNumberLog);
            }
            return batchNumberLog;
        }

        public Guid GetBatchNumberLogIDByDivisionID(Guid divisionID)
        {
            if (_divisionIDBatchLogIDCache.ContainsKey(divisionID))
            {
                return _divisionIDBatchLogIDCache[divisionID];
            }
            else
            {
                _divisionIDBatchLogIDCache = new Dictionary<Guid, Guid>();

                var log =
                    _repository.Query(x => x.DivisionID == divisionID && x.BatchNumber < 0)
                        .Select()
                        .FirstOrDefault();
                _divisionIDBatchLogIDCache.Add(divisionID, log.BatchNumberLogID);
                return log.BatchNumberLogID;
            }
        }
    }
}