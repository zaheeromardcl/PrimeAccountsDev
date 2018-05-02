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
    internal class ConsignmentItemServiceTests
    {
        private static ConsignmentItem CreateConsignmentItem()
        {
            var guid = PrimeActs.Service.IDGenerator.NewGuid('L');
            var consignmentItem = new ConsignmentItem
            {
                ConsignmentItemID = guid,
                ConsignmentID = Guid.Parse("BBD967D8-3200-47DB-8352-E04362C5B394"),
                BestBeforeDate = DateTime.Today,
                ProduceID = Guid.Parse("68041464-4850-0000-0076-000000000125"),
                Brand = "BrandApples",
                PackSize = "Medium",
                PackPall = 12,
                PackWeight = 10.00m,
                //Pack = "12",
                PackWtUnitID = Guid.Parse("9D1D0C0D-EEC6-442A-B390-AB2BE29ED3F0"),
                //ExpectedQuantity = 12.00m,
                //ReceivedQuantity = 12.00m,
                EstimatedProfit = 54,
                EstimatedChargeCost = 34,
                //Returns = 1.20m,
                EstimatedPurchaseCost = 10,
                //ItemStatus = 0,
                PorterageID = Guid.Parse("68041464-4851-3000-0076-000000000422"),
                CreatedBy = "Test",
                CreatedDate = DateTime.Now,
                ObjectState = ObjectState.Added
            };
            return consignmentItem;
        }

        [Test]
        public void ConsignmentItemByConsignmentIDTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<ConsignmentItem> consignmentItemRepository = new Repository<ConsignmentItem>(context,
                    unitOfWork);
                IConsignmentItemService consignmentItemService = new ConsignmentItemService(consignmentItemRepository);
                var guid = Guid.Parse("BBD967D8-3200-47DB-8352-E04362C5B394");
                var consignmentItems = consignmentItemService.ConsignmentItemsByConsignmentID(guid);
                Assert.AreEqual(consignmentItems.Count, 2);
            }
        }


        [Test]
        public void ConsignmentItemByIdTest()
        {
            //Arrange
            var consignmentItemID = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<ConsignmentItem> consignmentItemRepository = new Repository<ConsignmentItem>(context,
                    unitOfWork);
                IConsignmentItemService consignmentItemService = new ConsignmentItemService(consignmentItemRepository);
                var consignmentItem = CreateConsignmentItem();
                consignmentItemID = consignmentItem.ConsignmentItemID.ToString();
                consignmentItemService.Insert(consignmentItem);
                unitOfWork.SaveChanges();
            }
        }


        [Test]
        public void CreateConsignmentItemTest()
        {
            var guid = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<ConsignmentItem> consignmentItemRepository = new Repository<ConsignmentItem>(context,
                    unitOfWork);

                var consignmentItem = CreateConsignmentItem();
                guid = consignmentItem.ConsignmentItemID.ToString();
                consignmentItemRepository.Insert(consignmentItem);
                unitOfWork.SaveChanges();
            }

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<ConsignmentItem> consignmentItemRepository = new Repository<ConsignmentItem>(context,
                    unitOfWork);
                var consignmentItem = consignmentItemRepository.Find(Guid.Parse(guid));
                Assert.AreEqual(consignmentItem.ConsignmentItemID.ToString(), guid);
            }
        }


        [Test]
        public void GetAllConsignmentItemsTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<ConsignmentItem> consignmentItemRepository = new Repository<ConsignmentItem>(context,
                    unitOfWork);
                IConsignmentItemService consignmentItemService = new ConsignmentItemService(consignmentItemRepository);
                var consignmentItemList = consignmentItemService.GetAllConsignmentItems();
                Assert.Greater(consignmentItemList.Count, 0);
            }
        }

        //    {
        //    var consignmentItem = new ConsignmentItem
        //    var guid = PrimeActs.Service.IDGenerator.NewGuid('L');
        //{


        //private static ConsignmentItem CreateConsignmentItem()
        //        ConsignmentItemID = guid,
        //        ConsignmentID = Guid.Parse("BBD967D8-3200-47DB-8352-E04362C5B394"),
        //        BestBeforeDate = DateTime.Today,
        //        ProduceID = Guid.Parse("68041464-4850-0000-0076-000000000125"),
        //        Brand = "BrandApples",
        //        PackSize  ="Medium",
        //        PackPall = 12,
        //        PackWeight = 10.00m,
        //        //Pack = "12",
        //        PackWtUnitID = Guid.Parse("9D1D0C0D-EEC6-442A-B390-AB2BE29ED3F0"),
        //        ExpectedQuantity = 12.00m,
        //        ReceivedQuantity = 12.00m,
        //        EstimatedProfit = 54,
        //        EstimatedChargeCost = 34,
        //        //Returns = 1.20m,
        //        EstimatedPurchaseCost = 10,
        //        Status = 0,
        //        PorterageID = Guid.Parse("68041464-4851-3000-0076-000000000422"),
        //        CreatedBy = "Test",
        //        CreatedDate = DateTime.Now,
        //        IsActive = true,
        //        ObjectState = ObjectState.Added,


        //    };
        //    return consignmentItem;
        //}
    }
}