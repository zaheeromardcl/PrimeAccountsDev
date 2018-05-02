#region

using System;
using NUnit.Framework;
using PrimeActs.Data.Contexts;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.ServiceTests
{
    [TestFixture]
    internal class TicketServiceTests
    {
        private static Ticket CreateTicket()
        {
            var guid = PrimeActs.Service.IDGenerator.NewGuid('L');
            var ticket = new Ticket
            {
                TicketID = guid,
                TicketReference = "blahRef", //guid.ToString().Replace("-", ""),
                //CreatedBy = 
                CreatedDate = DateTime.Now,
                //IsActive = true,
                ObjectState = ObjectState.Added,
                PONumber = "TestDesc",
                CustomerDepartmentID = Guid.Parse("F1FD5520-2811-4348-A61A-FA6C6688AB4D"),
                NoteID = Guid.Parse("999C645D-DABB-4382-B647-BE659B7DA343"),
                TicketDate = DateTime.Today,
                IsCashSale = true,
               // UpdatedBy = "test",
                UpdatedDate = DateTime.Today,
                CurrencyID = Guid.Parse("49EAD349-7BB9-4EEE-9175-961C0836D906"),
                CurrencyRate = 10,
                SalesPersonUserID = Guid.Parse("D4EF3B5A-85D2-4315-AE79-EF42B627C5E9")
            };
            return ticket;
        }


        [Test]
        public void CreateTicketTest()
        {
            var guid = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Ticket> ticketRepository = new Repository<Ticket>(context, unitOfWork);

                var ticket = CreateTicket();
                guid = ticket.TicketID.ToString();
                ticketRepository.Insert(ticket);
                unitOfWork.SaveChanges();
            }

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Ticket> ticketRepository = new Repository<Ticket>(context, unitOfWork);
                var ticket = ticketRepository.Find(Guid.Parse(guid));
                //Assert.AreEqual(ticket.TicketReference, guid.Replace("-", ""));
                Assert.AreEqual(ticket.TicketID.ToString(), guid);
            }
        }


        [Test]
        public void GetAllTicketsTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Ticket> ticketRepository = new Repository<Ticket>(context, unitOfWork);
                ITicketService ticketService = new TicketService(ticketRepository);
                var ticketList = ticketService.GetAllTickets();
                Assert.Greater(ticketList.Count, 0);
            }
        }


        [Test]
        public void TicketByIdTest()
        {
            //Arrange
            var ticketID = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Ticket> ticketRepository = new Repository<Ticket>(context, unitOfWork);
                ITicketService ticketService = new TicketService(ticketRepository);
                var ticket = CreateTicket();
                ticketID = ticket.TicketID.ToString();
                ticketService.Insert(ticket);
                unitOfWork.SaveChanges();
            }
        }

        [Test]
        public void TicketByRefTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Ticket> ticketRepository = new Repository<Ticket>(context, unitOfWork);
                ITicketService ticketService = new TicketService(ticketRepository);
                ;
                var ticket = ticketService.TicketByRef("AS45454");
                Assert.AreEqual(ticket.TicketReference, "AS45454");
            }
        }
    }
}