#region

using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Department;

#endregion

namespace PrimeActs.Orchestras.Tests
{
    [TestFixture]
    public class DepartmentOrchestraTests
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

            var _mockDepartmentService = new Mock<IDepartmentService>();
            var _mockDivisionService = new Mock<IDivisionService>();
            _mockDepartmentService.Setup(x => x.GetAllDepartments()).Returns(() => new List<Department>());
            var DepartmentOrchestra = new DepartmentOrchestra(mockSetupLocalService.Object, _mockDepartmentService.Object, _mockDivisionService.Object);

            //Act
            DepartmentOrchestra.GetDeparments();
            //Assert

            _mockDepartmentService.Verify();
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


            var _mockDepartmentService = new Mock<IDepartmentService>();
            var _mockDivisionService = new Mock<IDivisionService>();
            _mockDepartmentService.Setup(x => x.DepartmentById(It.IsAny<Guid>())).Returns(() => new Department());
            var DepartmentOrchestra = new DepartmentOrchestra(mockSetupLocalService.Object, _mockDepartmentService.Object, _mockDivisionService.Object);

            //Act
            DepartmentOrchestra.GetDepartment(PrimeActs.Service.IDGenerator.NewGuid('L'));
            //Assert

            _mockDepartmentService.Verify();
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

            var _mockDepartmentService = new Mock<IDepartmentService>();
            var _mockDivisionService = new Mock<IDivisionService>();
            _mockDepartmentService.Setup(x => x.Insert(It.IsAny<Department>()));
            var DepartmentOrchestra = new DepartmentOrchestra(mockSetupLocalService.Object, _mockDepartmentService.Object, _mockDivisionService.Object);

            //Act
            DepartmentOrchestra.CreateDepartment(new DepartmentEditModel {DepartmentName = "DepartmentName"});
            //Assert

            _mockDepartmentService.Verify();
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