using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Service;
using NUnit.Framework;
using PrimeActs.Data.Contexts;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Data.Service;
using PrimeActs.Infrastructure.BaseEntities;
namespace PrimeActs.Service.Tests
{
    [TestFixture()]
    public class ProduceServiceTests
    {

        [Test()]
        public void ProduceByNameTest()
        {
            //Arrange
            string produceName = string.Empty;
            using (IDataContextAsync context = new PrimeActs.Data.Contexts.PAndIContext() as IDataContextAsync )
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
               
                IRepositoryAsync<Produce> produceRepository = new Repository<Produce>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IProduceService produceService = new ProduceService(produceRepository, cache);
                
                var produce = CreateProduce();
                produceName = produce.ProduceName;
                produceService.Insert(produce);
                unitOfWork.SaveChanges();
                produceService.RefreshCache();
            }
            //Act

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Produce> produceRepository = new Repository<Produce>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IProduceService produceService = new ProduceService(produceRepository, cache);
                var produce = produceService.ProduceByName(produceName);
                //Assert
                Assert.AreEqual(produce.ProduceName, produceName);
            }               
        }

        [Test()]
        public void ProduceByIdTest()
        {
            //Arrange
            string produceId = string.Empty;
            using (IDataContextAsync context = new PrimeActs.Data.Contexts.PAndIContext() as IDataContextAsync)
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Produce> produceRepository = new Repository<Produce>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IProduceService produceService = new ProduceService(produceRepository, cache);

                var produce = CreateProduce();
                produceId = produce.ProduceID.ToString();
                produceService.Insert(produce);
                unitOfWork.SaveChanges();
                produceService.RefreshCache();
            }
            //Act

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Produce> produceRepository = new Repository<Produce>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IProduceService produceService = new ProduceService(produceRepository, cache);
                var produce = produceService.ProduceById(Guid.Parse(produceId));
                //Assert
                Assert.AreEqual(produce.ProduceID.ToString(), produceId);
            }


        }

        [Test()]
        public void CreateProduceTest()
        {
            string guid = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Produce> produceRepository = new Repository<Produce>(context, unitOfWork);

                var produce = CreateProduce();
                guid = produce.ProduceID.ToString();
                produceRepository.Insert(produce);
                unitOfWork.SaveChanges();

            }

           
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Produce> produceRepository = new Repository<Produce>(context, unitOfWork);
                var produce = produceRepository.Find(Guid.Parse(guid));
                Assert.AreEqual(produce.ProduceName, guid.Replace("-", ""));
                Assert.AreEqual(produce.ProduceID.ToString(), guid);
            }
        }

        [Test()]
        public void GetAllProduceTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Produce> produceRepository = new Repository<Produce>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IProduceService produceService = new ProduceService(produceRepository, cache);
                var produceList = produceService.GetAllProduces();
                Assert.Greater(produceList.Count,0);
            }


        }

        private static Produce CreateProduce()
        {
            var guid = Guid.NewGuid();
            var produce = new Produce
            {
                ProduceID = guid,
                ProduceName = guid.ToString().Replace("-", ""),
                ProduceCode="PRODCOD",
                CreatedBy = "Test",
                CreatedDate = DateTime.Now,
                IsActive = true,
                ObjectState = ObjectState.Added,
                ProduceGroupID = Guid.Parse("24BA7F38-F86E-474C-BA0E-34BE0CB72303"),
                MasterGroupID = Guid.Parse("24BA7F38-F86E-474C-BA0E-34BE0CB72302")
            };
            return produce;
        }
    }
}
