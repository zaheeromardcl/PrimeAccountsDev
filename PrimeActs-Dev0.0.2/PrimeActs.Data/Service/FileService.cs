#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IFileService : IService<File>
    {
        File FileByName(string fileName);
        File FileById(Guid Id);
        List<File> GetAllFiles();
    }

    public class FileService : Service<File>, IFileService
    {
        private readonly IRepositoryAsync<File> _repository;

        public FileService(IRepositoryAsync<File> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public File FileByName(string fileName)
        {
            return _repository.Query().Select().Where(inc => inc.FileName == fileName).FirstOrDefault();
        }

        public File FileById(Guid Id)
        {
            return _repository.Query().Select().Where(inc => inc.FileID == Id).FirstOrDefault();
        }

        public List<File> GetAllFiles()
        {
            return _repository.Query().Select().ToList();
        }
    }
}