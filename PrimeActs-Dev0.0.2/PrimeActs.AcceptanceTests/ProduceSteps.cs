using System;
using TechTalk.SpecFlow;
using PrimeActs.UI.Controllers;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Data.Service;
using PrimeActs.Orchestras;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using System.Collections.Generic;

namespace PrimeActs.AcceptanceTests
{
    [Binding]
    public class ProduceSteps
    {
         private ProduceController _produceController;
        private ActionResult result;
        private IDataContextAsync context;
        private IUnitOfWorkAsync unitOfWork;
        private ProduceEditModel produceEditModel;
        public ProduceSteps()
        {
            context = new PrimeActs.Data.Contexts.PAndIContext() as IDataContextAsync;
            unitOfWork = new UnitOfWork(context);
        }
       

        private IProduceOrchestra Context()
        {
            IRepositoryAsync<Produce> divisionRepository = new Repository<Produce>(context, unitOfWork);
            IRepositoryAsync<MasterGroup> masterGroupRepository = new Repository<MasterGroup>(context, unitOfWork);
            IRepositoryAsync<ProduceGroup> produceGroupRepository = new Repository<ProduceGroup>(context, unitOfWork);
            ICache cache = Cache.Get(CacheType.Memory);
            IProduceService produceService = new ProduceService(divisionRepository, cache);
            IMasterGroupService masterGroupService = new MasterGroupService(masterGroupRepository, cache);
            IProduceGroupService produceGroupService = new ProduceGroupService(produceGroupRepository, cache);

            IProduceOrchestra produceOrchestra = new ProduceOrchestra(produceService, masterGroupService, produceGroupService);
            return produceOrchestra;
        }
        [Given(@"the user has logged in as a member with SuperAdmin role")]
        public void GivenTheUserHasLoggedInAsAMemberWithSuperAdminRole()
        {
            IProduceOrchestra produceOrchestra = Context();
            _produceController = new ProduceController(produceOrchestra, unitOfWork);
            _produceController.ControllerContext = MockWebContext.AuthenticatedContext("santosh", new[] { "SuperAdmin" }, true);

        }

        [When(@"the produce link is clicked")]
        public void WhenTheProduceLinkIsClicked()
        {
            result = _produceController.Index();
        }

        [Then(@"the produce screen should be displayed\.")]
        public void ThenTheProduceScreenShouldBeDisplayed_()
        {
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsEmpty(((ViewResult)result).ViewName);
        }

        [When(@"the Create Produce link is clicked")]
        public void WhenTheCreateProduceLinkIsClicked()
        {
            result = _produceController.Create();
        }

        [Then(@"the Create Produce form should be displayed\.")]
        public void ThenTheCreateProduceFormShouldBeDisplayed_()
        {
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsEmpty(((ViewResult)result).ViewName);
        }

        [Given(@"valid produce details are entered in the Create Produce form")]
        public void GivenValidProduceDetailsAreEnteredInTheCreateProduceForm()
        {
            produceEditModel = new ProduceEditModel { ProduceName = Guid.NewGuid().ToString().Replace("-", ""),ProduceCode="PRODCOD",IsActive=true,SelectedProduceGroup = "24BA7F38-F86E-474C-BA0E-34BE0CB72303" ,SelectedMasterGroup = "24BA7F38-F86E-474C-BA0E-34BE0CB72302" };
        }

        [When(@"the Create Produce button is pressed")]
        public void WhenTheCreateProduceButtonIsPressed()
        {
            result = _produceController.Create(produceEditModel);
        }

        [Then(@"the user is redirected to details page with produce creation confirmation")]
        public void ThenTheUserIsRedirectedToDetailsPageWithProduceCreationConfirmation()
        {
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsEmpty(((ViewResult)result).ViewName);
        }

        [Given(@"invalid produce details are entered in the Create Produce form")]
        public void GivenInvalidProduceDetailsAreEnteredInTheCreateProduceForm()
        {
            produceEditModel = new ProduceEditModel { ProduceName = "",ProduceCode="PRODCOD",IsActive=true,SelectedProduceGroup = "24BA7F38-F86E-474C-BA0E-34BE0CB72303", SelectedMasterGroup = "24BA7F38-F86E-474C-BA0E-34BE0CB72302" };
        }

        [Then(@"a validation error message Please enter valid details should be displayed\.")]
        public void ThenAValidationErrorMessagePleaseEnterValidDetailsShouldBeDisplayed_()
        {

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            Assert.IsTrue(_produceController.ViewData.ModelState.IsValid == false);


        }

    }
}
