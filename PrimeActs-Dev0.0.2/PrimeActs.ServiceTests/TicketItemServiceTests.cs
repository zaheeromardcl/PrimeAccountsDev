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
    internal class TicketItemServiceTests
    {
        private static TicketItem CreateTicketItem()
        {
            var guid = PrimeActs.Service.IDGenerator.NewGuid('L');
            var ticketItem = new TicketItem
            {
                TicketItemID = guid,
                TicketID = Guid.Parse("A399E7E7-8250-4544-9C31-DF582D8707A7"),
                TicketItemDescription = "Desc1",
                TicketItemQuantity = 5,
                //TicketItemBrand = "Brand1",
                //TicketItemSize = 6,
                TicketItemTotalPrice = 30,
               // CreatedBy = "Test",
                CreatedDate = DateTime.Now,
                //IsActive = true,
                ObjectState = ObjectState.Added
            };
            return ticketItem;
        }


        [Test]
        public void CreateTicketItemTest()
        {
            var guid = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<TicketItem> ticketItemRepository = new Repository<TicketItem>(context, unitOfWork);

                var ticketItem = CreateTicketItem();
                guid = ticketItem.TicketItemID.ToString();
                ticketItemRepository.Insert(ticketItem);
                unitOfWork.SaveChanges();
            }

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<TicketItem> ticketItemRepository = new Repository<TicketItem>(context, unitOfWork);
                var ticketItem = ticketItemRepository.Find(Guid.Parse(guid));
                Assert.AreEqual(ticketItem.TicketItemID.ToString(), guid);
            }
        }


        //[Test]
        //public void GetAllTicketItemsTest()
        //{
        //    using (IDataContextAsync context = new PAndIContext())
        //    using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
        //    {
        //        IRepositoryAsync<TicketItem> ticketItemRepository = new Repository<TicketItem>(context, unitOfWork);
        //        ITicketItemService ticketItemService = new TicketItemService(ticketItemRepository);
        //        var ticketItemList = ticketItemService.GetAllTicketItems();
        //        Assert.Greater(ticketItemList.Count, 0);
        //    }
        //}


        //[Test]
        //public void TicketItemByIdTest()
        //{
        //    //Arrange
        //    var ticketItemID = string.Empty;
        //    using (IDataContextAsync context = new PAndIContext())
        //    using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
        //    {
        //        IRepositoryAsync<TicketItem> ticketItemRepository = new Repository<TicketItem>(context, unitOfWork);
        //        ITicketItemService ticketItemService = new TicketItemService(ticketItemRepository);
        //        var ticketItem = CreateTicketItem();
        //        ticketItemID = ticketItem.TicketItemID.ToString();
        //        ticketItemService.Insert(ticketItem);
        //        unitOfWork.SaveChanges();
        //    }
        //}

        //[Test]
        //public void TicketItemByTicketIDTest()
        //{
        //    using (IDataContextAsync context = new PAndIContext())
        //    using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
        //    {
        //        IRepositoryAsync<TicketItem> ticketItemRepository = new Repository<TicketItem>(context, unitOfWork);
        //        ITicketItemService ticketItemService = new TicketItemService(ticketItemRepository);
        //        var guid = Guid.Parse("A399E7E7-8250-4544-9C31-DF582D8707A7");
        //        var ticketItems = ticketItemService.TicketItemsByTicketID(guid);
        //        //Currently there are 14 ticketItems in the database
        //        Assert.AreEqual(ticketItems.Count, 14);
        //    }
        //}
    }
}