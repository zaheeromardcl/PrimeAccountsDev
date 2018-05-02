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
    internal class ConsignmentServiceTests
    {
        private static Consignment CreateConsignment()
        {
            var guid = PrimeActs.Service.IDGenerator.NewGuid('L');
            var consignment = new Consignment
            {
                ConsignmentID = guid,
                ConsignmentReference = "blahRef", //guid.ToString().Replace("-", ""),
                //CreatedBy = "Test",
                CreatedDate = DateTime.Now,
                //IsActive = true,
                ObjectState = ObjectState.Added,
                ConsignmentDescription = "TestDesc",
                ContractDate = DateTime.Today,
                VehicleDetail = "blah blah",
                //ReceivedDate = DateTime.Today,
                //DepartmentID = Guid.Parse("1732DADB-150B-416D-9972-035F16A972A9"),
                PortID = Guid.Parse("68041464-4812-3000-0076-000000000125"),
                //SupplierID = Guid.Parse("68041464-4812-3000-0076-000000000125"),
                DespatchDate = DateTime.Today,
                Handling = 0,
                Commission = 1,
                //OriginCountryID = Guid.Parse("E9EF69C1-B775-4918-B5A1-197D8231F0FE"),
                DespatchLocationID = Guid.Parse("68041464-4851-0000-0076-000000000309"),
                Vehicle = "blah",
                ShowVehicleOnInvoice = true,
                PurchaseTypeID = Guid.Parse("A3ADEBD8-A70C-43C2-A0A0-4299772810E1"),
                SupplierReference = "blahAgain"
            };
            return consignment;
        }


        [Test]
        public void ConsignmentByIdTest()
        {
            //Arrange
            var consignmentID = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Consignment> consignmentRepository = new Repository<Consignment>(context, unitOfWork);
                IConsignmentService consignmentService = new ConsignmentService(consignmentRepository);
                var consignment = CreateConsignment();
                consignmentID = consignment.ConsignmentID.ToString();
                consignmentService.Insert(consignment);
                unitOfWork.SaveChanges();
            }
        }

        [Test]
        public void ConsignmentByRefTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Consignment> consignmentRepository = new Repository<Consignment>(context, unitOfWork);
                IConsignmentService consignmentService = new ConsignmentService(consignmentRepository);
                ;
                var consignment = consignmentService.ConsignmentByRef("R123456");
                Assert.AreEqual(consignment.ConsignmentReference, "R123456");
            }
        }


        [Test]
        public void CreateConsignmentTest()
        {
            var guid = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Consignment> consignmentRepository = new Repository<Consignment>(context, unitOfWork);

                var consignment = CreateConsignment();
                guid = consignment.ConsignmentID.ToString();
                consignmentRepository.Insert(consignment);
                unitOfWork.SaveChanges();
            }

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Consignment> consignmentRepository = new Repository<Consignment>(context, unitOfWork);
                var consignment = consignmentRepository.Find(Guid.Parse(guid));
                //Assert.AreEqual(consignment.ConsignmentReference, guid.Replace("-", ""));
                Assert.AreEqual(consignment.ConsignmentID.ToString(), guid);
            }
        }


        [Test]
        public void GetAllConsignmentsTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Consignment> consignmentRepository = new Repository<Consignment>(context, unitOfWork);
                IConsignmentService consignmentService = new ConsignmentService(consignmentRepository);
                var consignmentList = consignmentService.GetAllConsignments();
                Assert.Greater(consignmentList.Count, 0);
            }
        }
    }
}