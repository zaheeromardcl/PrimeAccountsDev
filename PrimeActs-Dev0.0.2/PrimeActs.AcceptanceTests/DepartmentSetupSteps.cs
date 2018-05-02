#region

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using PrimeActs.Data.Contexts;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Controllers;
using TechTalk.SpecFlow;
using Department = PrimeActs.Domain.Department;

#endregion

namespace PrimeActs.AcceptanceTests
{
    [Binding]
    [Scope(Feature = "myDepartment")]
    public class DepartmentSetupSteps
    {
        private readonly IDataContextAsync context;
        private readonly IUnitOfWorkAsync unitOfWork;
        private DepartmentController _departmentController;
        private DepartmentEditModel departmentEditModel;
        private ActionResult result;

        public DepartmentSetupSteps()
        {
            context = new PAndIContext();
            unitOfWork = new UnitOfWork(context);
        }


        private IDepartmentOrchestra Context()
        {
            IRepositoryAsync<SetupLocal> setupLocalRepository = new Repository<SetupLocal>(context, unitOfWork);
            IRepositoryAsync<Department> departmentRepository = new Repository<Department>(context, unitOfWork);
            IRepositoryAsync<Division> divisionRepository = new Repository<Division>(context, unitOfWork);
            var cache = Cache.Get(CacheType.Memory);
            ISetupLocalService setupLocalService = new SetupLocalService(setupLocalRepository);
            IDepartmentService departmentService = new DepartmentService(departmentRepository, cache);
            IDivisionService divisionService = new DivisionService(divisionRepository, cache);

            IDepartmentOrchestra departmentOrchestra = new DepartmentOrchestra(setupLocalService, departmentService, divisionService);
            return departmentOrchestra;
        }

        [Given(@"user is registered as SuperAdmin User")]
        public void GivenUserIsRegisteredAsSuperAdminUser()
        {
            var departmentOrchestra = Context();
            _departmentController = new DepartmentController(departmentOrchestra, unitOfWork);
            _departmentController.ControllerContext = MockWebContext.AuthenticatedContext("santosh",
                new[] {"SuperAdmin"}, true);
        }

        [When(@"the user goes to the create department screen")]
        public void WhenTheUserGoesToTheCreateDepartmentScreen()
        {
            result = _departmentController.Create();
        }

        [Then(@"the result should be that the Create Department Form screen is displayed")]
        public void ThenTheResultShouldBeThatTheCreateDepartmentFormScreenIsDisplayed()
        {
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsEmpty(((ViewResult) result).ViewName);
        }

        [Then(@"a default '(.*)' is selected in the Division dropdown\.")]
        public void ThenADefaultIsSelectedInTheDivisionDropdown_(string p0)
        {
            //var dropdownModel = ((DepartmentViewModel) _departmentController.ViewData.Model).DivisionList as List<dropdownlistModel>;
            //Assert.IsTrue(dropdownModel[0].Name == p0);
        }


        [Given(@"the user hasn't entered department name")]
        public void GivenTheUserHasnTEnteredDepartmentName()
        {
            departmentEditModel = new DepartmentEditModel {DepartmentName = ""};
        }


        //[Then(@"User should be shown the error message '(.*)' for '(.*)'")]
        //[Scope(Tag = "myDepartment")]
        //public void ThenUserShouldBeShownTheErrorMessageFor(string p0, string p1)
        //{
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<ViewResult>(result);

        //    Assert.IsTrue(_departmentController.ViewData.ModelState.ContainsKey(p1));

        //    Assert.AreEqual(p0, _departmentController.ViewData.ModelState[p1].Errors[0].ErrorMessage);
        //}


        [When(@"the user goes to the create department with missing department name")]
        public void WhenTheUserGoesToTheCreateDepartmentWithMissingDepartmentName()
        {
            result = _departmentController.Create(departmentEditModel);
        }

        [Given(@"the user hasn't selected division")]
        public void GivenTheUserHasnTSelectedDivision()
        {
            departmentEditModel = new DepartmentEditModel
            {
                DepartmentName = PrimeActs.Service.IDGenerator.NewGuid('L').ToString().Replace("-", ""),
                //SelectedDivision = ""
            };
        }

        [When(@"the user goes to the create department with missing department")]
        public void WhenTheUserGoesToTheCreateDepartmentWithMissingDepartment()
        {
            result = _departmentController.Create(departmentEditModel);
        }

        [Given(@"the user enteres valid department details\.")]
        public void GivenTheUserEnteresValidDepartmentDetails_()
        {
            departmentEditModel = new DepartmentEditModel
            {
                DepartmentName = PrimeActs.Service.IDGenerator.NewGuid('L').ToString().Replace("-", ""),
                //SelectedDivision = ""
            };
        }

        [When(@"the user goes to the create department")]
        public void WhenTheUserGoesToTheCreateDepartment()
        {
            result = _departmentController.Create(departmentEditModel);
        }

        [Then(@"User should be shown redirected to department index")]
        public void ThenUserShouldBeShownRedirectedToDepartmentIndex()
        {
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Given(@"user is registered as Admin User")]
        public void GivenUserIsRegisteredAsAdminUser()
        {
            var departmentOrchestra = Context();
            _departmentController = new DepartmentController(departmentOrchestra, unitOfWork);
            _departmentController.ControllerContext = MockWebContext.AuthenticatedContext("santosh", new[] {"Admin"},
                true);
        }

        [When(@"the user tries to access create department page")]
        public void WhenTheUserTriesToAccessCreateDepartmentPage()
        {
            result = _departmentController.Create(new DepartmentEditModel());
        }

        [Then(@"User should be shown redirected to not authorized page\.")]
        public void ThenUserShouldBeShownRedirectedToNotAuthorizedPage_()
        {
            ScenarioContext.Current.Pending();
            //Assert.IsNotNull(result);
            //Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}