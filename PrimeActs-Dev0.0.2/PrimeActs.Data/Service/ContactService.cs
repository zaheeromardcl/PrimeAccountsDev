using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class ContactService : Service<Contact>, IContactService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Contact> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;

        public ContactService(IRepositoryAsync<Contact> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
        }

        public List<Contact> GetAllCustomerDepartmentContacts(Guid customerDepartmentID)
        {
            var contacts = _repository.Query(c => c.CustomerDepartments.Any(d => d.CustomerDepartmentID == customerDepartmentID))
                .Select().ToList();
            return contacts;
        }

        public Contact GetContactById(Guid contactId)
        {
            var contact = _repository.Query(c => c.ContactID == contactId).Select().FirstOrDefault();
            return contact;
        }

        public void RefreshCache()
        {
            _cache.Remove(type);
        }

        private void CheckCache()
        {

        }
    }
}
