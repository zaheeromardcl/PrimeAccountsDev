using NUnit.Framework;
using PrimeActs.Data.Contexts;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.ServiceTests
{
    [TestFixture]
    public class ContactServiceTests
    {
        [TestCase("00760000-0000-0483-0006-817528074450")]
        [Test]
        public void GetAllCustomerDepartmentContactsTest(string customerDepartmentId)
        {
            using (IDataContextAsync context = new PAndIContext())
            {
                using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
                {
                    var cache = new MemoryCache();
                    IRepositoryAsync<Contact> contactsRepository = new Repository<Contact>(context, unitOfWork);
                    IContactService contactsService = new ContactService(contactsRepository, cache);

                    var contacts = contactsService.GetAllCustomerDepartmentContacts(Guid.Parse(customerDepartmentId));
                    Assert.That(contacts.Count, Is.GreaterThan(0));
                }
            }
        }
    }
}
