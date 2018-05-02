#region

using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface INominalLedgerEntryService : IService<NominalLedgerEntry>
    {
    }

    public class NominalLedgerEntryService : Service<NominalLedgerEntry>, INominalLedgerEntryService
    {
        private readonly IRepositoryAsync<NominalLedgerEntry> _repository;
        private readonly string _type;


        public NominalLedgerEntryService(IRepositoryAsync<NominalLedgerEntry> repository)
            : base(repository)
        {
            _repository = repository;
            _type = typeof (Note).FullName;
        }
    }
}