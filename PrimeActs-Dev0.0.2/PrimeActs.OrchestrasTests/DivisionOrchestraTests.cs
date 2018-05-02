#region

using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Division;

#endregion

namespace PrimeActs.Orchestras.Tests
{
    [TestFixture]
    public class DivisionOrchestraTests
    {
        [Test]
        public void ApplyChangesTest()
        {
            Assert.Fail();
        }

        [Test]
        public void CreateFromTest()
        {
            Assert.Fail();
        }

        [Test]
        public void GetAllModelsTest()
        {
            var mockSetupLocalService = new Mock<ISetupLocalService>();
            mockSetupLocalService.Setup(x => x.Find("ServerCode")).Returns(() => new SetupLocal
                {
                    SetupName = "ServerCode",
                    SetupValueNvarchar = "L"
                });

            var _mockDivisionService = new Mock<IDivisionService>();
            _mockDivisionService.Setup(x => x.GetAllDivisions()).Returns(() => new List<Division>());
            var divisionOrchestra = new DivisionOrchestra(mockSetupLocalService.Object, _mockDivisionService.Object);

            //Act
            divisionOrchestra.GetDivisions();

            //Assert
            _mockDivisionService.Verify();
        }

        [Test]
        public void GetModelTest()
        {
            //Arrange 
            var mockSetupLocalService = new Mock<ISetupLocalService>();
            mockSetupLocalService.Setup(x => x.Find("ServerCode")).Returns(() => new SetupLocal
            {
                SetupName = "ServerCode",
                SetupValueNvarchar = "L"
            });

            var _mockDivisionService = new Mock<IDivisionService>();
            _mockDivisionService.Setup(x => x.DivisionById(It.IsAny<Guid>())).Returns(() => new Division());

            var divisionOrchestra = new DivisionOrchestra(mockSetupLocalService.Object, _mockDivisionService.Object);

            //Act
            divisionOrchestra.GetDivisions();
            //Assert

            _mockDivisionService.Verify();
        }


        [Test]
        public void InsertTest()
        {
            var mockSetupLocalService = new Mock<ISetupLocalService>();
            mockSetupLocalService.Setup(x => x.Find("ServerCode")).Returns(() => new SetupLocal
            {
                SetupName = "ServerCode",
                SetupValueNvarchar = "L"
            });

            var _mockDivisionService = new Mock<IDivisionService>();
            _mockDivisionService.Setup(x => x.Insert(It.IsAny<Division>()));

            var divisionOrchestra = new DivisionOrchestra(mockSetupLocalService.Object, _mockDivisionService.Object);

            //Act
            divisionOrchestra.CreateDivision(new DivisionEditModel {DivisionName = "DivisionName"});
            //Assert

            _mockDivisionService.Verify();
        }

        [Test]
        public void RebuildTest()
        {
            Assert.Fail();
        }

        [Test]
        public void RefreshCacheTest()
        {
            Assert.Fail();
        }

        [Test]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [Test]
        public void ValidateTest()
        {
            Assert.Fail();
        }
    }
}