using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Orchestras;
using NUnit.Framework;
using PrimeActs.Data.Service;
using Moq;
using PrimeActs.Domain;
namespace PrimeActs.Orchestras.Tests
{
    [TestFixture()]
    public class ProduceOrchestraTests
    {

        [Test()]
        public void GetModelTest()
        {
            //Arrange 

            var _mockProduceService = new Mock<IProduceService>();
            var _mockMasterGroupService = new Mock<IMasterGroupService>();
            var _mockProduceGroupService = new Mock<IProduceGroupService>();
            _mockProduceService.Setup(x => x.ProduceById(It.IsAny<Guid>())).Returns(() => new Produce());
            var produceOrchestra = new ProduceOrchestra(_mockProduceService.Object, _mockMasterGroupService.Object,_mockProduceGroupService.Object);
            
            //Act
            produceOrchestra.GetModel(Guid.NewGuid());
            //Assert

            _mockProduceService.Verify();
        }

        [Test()]
        public void GetAllModelsTest()
        {
            var _mockProduceService = new Mock<IProduceService>();
            var _mockMasterGroupService = new Mock<IMasterGroupService>();
            var _mockProduceGroupService = new Mock<IProduceGroupService>();
            _mockProduceService.Setup(x => x.GetAllProduces()).Returns(() => new List<Produce>());
            var produceOrchestra = new ProduceOrchestra(_mockProduceService.Object, _mockMasterGroupService.Object, _mockProduceGroupService.Object);


            //Act
            produceOrchestra.GetAllModels();
            //Assert

            _mockProduceService.Verify();
        }


        [Test()]
        public void InsertTest()
        {
            var _mockProduceService = new Mock<IProduceService>();
            var _mockMasterGroupService = new Mock<IMasterGroupService>();
            var _mockProduceGroupService = new Mock<IProduceGroupService>();
            _mockProduceService.Setup(x => x.Insert(It.IsAny<Produce>()));
            var produceOrchestra = new ProduceOrchestra(_mockProduceService.Object, _mockMasterGroupService.Object, _mockProduceGroupService.Object);


            //Act
            produceOrchestra.Insert(new Domain.ViewModels.ProduceEditModel { ProduceName="ProduceName"});
            //Assert

            _mockProduceService.Verify();
        }

        [Test()]
        public void ValidateTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateFromTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RebuildTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ApplyChangesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RefreshCacheTest()
        {
            Assert.Fail();
        }
    }
}
