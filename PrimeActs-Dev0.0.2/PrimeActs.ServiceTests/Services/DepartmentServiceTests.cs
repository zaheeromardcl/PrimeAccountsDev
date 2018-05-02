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
    public class DepartmentServiceTests
    {

        [Test()]
        public void DepartmentByNameTest()
        {
            //Arrange
            string departmentName = string.Empty;
            using (IDataContextAsync context = new PrimeActs.Data.Contexts.PAndIContext() as IDataContextAsync )
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
               
                IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IDepartmentService departmentService = new DepartmentService(departmentRepository, cache);
                
                var department = CreateDepartment();
                departmentName = department.DepartmentName;
                departmentService.Insert(department);
                unitOfWork.SaveChanges();
                departmentService.RefreshCache();
            }
            //Act

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IDepartmentService departmentService = new DepartmentService(departmentRepository, cache);
                var department = departmentService.DepartmentByName(departmentName);
                //Assert
                Assert.AreEqual(department.DepartmentName, departmentName);
            }               
        }

        [Test()]
        public void DepartmentByIdTest()
        {
            //Arrange
            string departmentId = string.Empty;
            using (IDataContextAsync context = new PrimeActs.Data.Contexts.PAndIContext() as IDataContextAsync)
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IDepartmentService departmentService = new DepartmentService(departmentRepository, cache);

                var department = CreateDepartment();
                departmentId = department.DepartmentID.ToString();
                departmentService.Insert(department);
                unitOfWork.SaveChanges();
                departmentService.RefreshCache();
            }
            //Act

            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IDepartmentService departmentService = new DepartmentService(departmentRepository, cache);
                var department = departmentService.DepartmentById(Guid.Parse(departmentId));
                //Assert
                Assert.AreEqual(department.DepartmentID.ToString(), departmentId);
            }


        }

        [Test()]
        public void CreateDepartmentTest()
        {
            string guid = string.Empty;
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);

                var department = CreateDepartment();
                guid = department.DepartmentID.ToString();
                departmentRepository.Insert(department);
                unitOfWork.SaveChanges();

            }

           
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);
                var department = departmentRepository.Find(Guid.Parse(guid));
                Assert.AreEqual(department.DepartmentName, guid.Replace("-", ""));
                Assert.AreEqual(department.DepartmentID.ToString(), guid);
            }
        }

        [Test()]
        public void GetAllDepartmentTest()
        {
            using (IDataContextAsync context = new PAndIContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);
                ICache cache = Cache.Get(CacheType.Memory);
                IDepartmentService departmentService = new DepartmentService(departmentRepository, cache);
                var departmentList = departmentService.GetAllDepartments();
                Assert.Greater(departmentList.Count,0);
            }


        }

        private static Department CreateDepartment()
        {
            var guid = Guid.NewGuid();
            var department = new Department
            {
                DepartmentID = guid,
                DepartmentName = guid.ToString().Replace("-", ""),
                CreatedBy = "Test",
                CreatedDate = DateTime.Now,
                IsActive = true,
                ObjectState = ObjectState.Added,
                DivisionID = Guid.Parse("D592E05A-0EC3-49F3-8CDF-0BEF872CC140")
            };
            return department;
        }
    }
}
