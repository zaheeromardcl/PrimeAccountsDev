#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IConsignmentFileService : IService<ConsignmentFile>
    {
        ConsignmentFile ConsignmentFileById(Guid Id);
        List<ConsignmentFile> GetAllConsignmentFiles();
    }

    public class ConsignmentFileService : Service<ConsignmentFile>, IConsignmentFileService
    {
        private readonly IRepositoryAsync<ConsignmentFile> _repository;

        public ConsignmentFileService(IRepositoryAsync<ConsignmentFile> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public ConsignmentFile ConsignmentFileById(Guid Id)
        {
            return _repository.Query().Select().Where(inc => inc.FileID == Id).FirstOrDefault();
        }

        public List<ConsignmentFile> GetAllConsignmentFiles()
        {
            return _repository.Query().Include(x => x.File).Select().ToList();
        }
    }
}