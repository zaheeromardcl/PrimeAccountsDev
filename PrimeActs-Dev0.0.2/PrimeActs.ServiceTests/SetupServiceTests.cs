using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using PrimeActs.Data.Contexts;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.ServiceTests
{
    [TestClass]
    public class SetupServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                 
                IRepositoryAsync<SetupLocal> setupLocalRepository = new Repository<SetupLocal>(context, unitOfWork);
                IRepositoryAsync<SetupGlobal> setupGlobalRepository = new Repository<SetupGlobal>(context, unitOfWork);
                ISetupLocalService localService = new SetupLocalService(setupLocalRepository);
                ISetupGlobalService globalService = new SetupGlobalService(setupGlobalRepository);
                var findTest = localService.Find("DailySalesTemplate");
                Assert.AreEqual(findTest.SetupName , "DailySalesTemplate");
                var smtpServerFindTest = localService.Find("SMTPServerURL");
                var globaltest = globalService.GetAllSetupValuesBySetupName("MaxAccountingPeriod");
                Assert.AreEqual(globaltest[0].SetupValueInt, 12);
               
            }
        }
    }
}
