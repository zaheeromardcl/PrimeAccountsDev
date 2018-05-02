#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Consignment;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SearchObject = PrimeActs.Domain.ViewModels.Consignment.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class ConsignmentController : ApiController
    {
        private readonly IConsignmentOrchestra _consignmentOrchestra;
        
        private readonly IUnitOfWorkAsync _unitofWork;


        private PrimeActsUserManager _userManager;
        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }


        private PrimeActsRoleManager _roleManager;
        public PrimeActsRoleManager RoleManager
        {
            get { return _roleManager ?? Request.GetOwinContext().Get<PrimeActsRoleManager>(); }
            private set { _roleManager = value; }
        }

        public ConsignmentController(IConsignmentOrchestra consignmentOrchestra, IUnitOfWorkAsync unitofWork)
        {
           
            _consignmentOrchestra = consignmentOrchestra;
            _unitofWork = unitofWork;
            //var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            //_consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    Firstname = user.Firstname,
            //    Lastname = user.Lastname,
            //    Nickname = user.Nickname,
            //    CompanyId = user.CompanyId,
            //    DepartmentId = user.DepartmentId,
            //    DivisionId = user.DivisionId
            //});
        }

        [HttpGet]
        public ResultList<ConsignmentEditModel> Index([FromUri] QueryOptions queryOptions,
            [FromUri] SearchObject searchObject)
        {
            if(string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "LASTMONTH";
            return _consignmentOrchestra.GetConsignments(queryOptions, searchObject);
        }

        [HttpGet]
        public ResultList<CompletedConsignment> IndexSimplified([FromUri] QueryOptions queryOptions,
            [FromUri] SearchObject searchObject)
        {
            if (string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "LASTWEEK";
            searchObject.CompletedConsignmentsOnly = true;
            return _consignmentOrchestra.GetConsignmentsSimplified(queryOptions, searchObject);
        }

        //[HttpGet]
        //public ResultList<ConsignmentEditModel> OrderIndex([FromUri] QueryOptions queryOptions,
        //    [FromUri] OrderSearchObject searchObject)
        //{
        //    if (string.IsNullOrEmpty(searchObject.RecordsInDays))
        //        searchObject.RecordsInDays = "CURRENTMONTH";
        //    return _consignmentOrchestra.GetConsignments(queryOptions, searchObject);
        //}



        public List<Autocomplete> AutoComplete(QueryOptions queryOptions)
        {
            var autoCompleteList = new List<Autocomplete>();


            //GetConsignmentViewModels(queryOptions).ConsignmentEditModels

            //foreach (var consignment in  _consignmentOrchestra.GetConsignmentViewModels(queryOptions).ConsignmentEditModels.Results )
            //{

            //    autoCompleteList.Add(new Autocomplete { Id = consignment.ConsignmentID.ToString(), label = consignment.ConsignmentReference + "-" +  consignment.ConsignmentDescription, value = consignment.ConsignmentID.ToString() });
            //}
            return autoCompleteList;
        }

        //[HttpGet]
        //public ResultList<ConsignmentEditModel> Index([FromUri]QueryOptions queryOptions)
        //{
        //    return _consignmentOrchestra.GetConsignments(queryOptions);
        //}

        //[HttpGet]
        //public ConsignmentViewModel Details()
        //{
        //    return _consignmentOrchestra.GetConsignmentViewModel(Guid.Empty);
        //}
        [HttpGet]
        public ConsignmentViewModel CreateConsignmentTab()
        {

            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });
            var consignmentViewModel = _consignmentOrchestra.GetConsignmentViewModel(Guid.Empty);
            //var consignmentEditModel = new ConsignmentEditModel();

            return consignmentViewModel;
        }

        [HttpPost]
        public ConsignmentEditModel CreateConsignment(ConsignmentEditModel consignmentEditModel)
        {
            //PE 28/04/17 Looks like this one is not called buut the non API one is called. Not sure why
            var user = User.Identity.GetApplicationUser();
            ConsignmentEditModel returnVal;
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });
            try
            {
                if (!_consignmentOrchestra.Validate(consignmentEditModel))
                    return consignmentEditModel;
                
                returnVal = _consignmentOrchestra.CreateConsignment(consignmentEditModel);
                _unitofWork.SaveChanges();
                //return consignmentEditModel;
                return returnVal;
            }
            catch (Exception ex)
            {
                return null; //View(_consignmentOrchestra.Rebuild(consignmentEditModel));
            }
        }


        [HttpPost]
        public ConsignmentViewModel Order(ConsignmentViewModel consignmentViewModel)
        {
            
         
            _consignmentOrchestra.CreateConsignment(consignmentViewModel.ConsignmentEditModel);
            _unitofWork.SaveChanges();
            return consignmentViewModel;
        }

        [HttpPost]
        public ConsignmentEditModel UpdateConsignment(ConsignmentEditModel ConsignmentEditModel)
        {
            try
            {
                if (!_consignmentOrchestra.Validate(ConsignmentEditModel))
                    return ConsignmentEditModel;
                _consignmentOrchestra.UpdateConsignment(ConsignmentEditModel);
                _unitofWork.SaveChanges();
                return ConsignmentEditModel;
            }
            catch
            {
                return null; //View(_ConsignmentOrchestra.Rebuild(ConsignmentEditModel));
            }
        }

        [HttpPost]
        public ConsignmentItemReturns EditReturnsConsignmentItem(ConsignmentItemReturns returns)
        {
            try
            {
                if (returns != null && returns.ConsignmentItemID != Guid.Empty )
                {
                    ConsignmentItemReturns returnReturns = _consignmentOrchestra.EditConsignmentItemPriceReturns(returns, User.Identity.GetUserId());
                    _unitofWork.SaveChanges();
                    return returnReturns;
                }
                
                return returns;
            }
            catch
            {
                return null;
            }
        }
        
        [HttpPost]
        public ConsignmentItemEditModel RemoveConsignmentItem(ConsignmentItemEditModel model)
        {
            _consignmentOrchestra.UpdateConsignmentItem(model);
            _unitofWork.SaveChanges();
            return model;
        }

        [HttpPost]
        public ConsignmentItemEditModel UpdateConsignmentItem(ConsignmentItemEditModel model)
        {
            _consignmentOrchestra.UpdateConsignmentItem(model);
            _unitofWork.SaveChanges();
            return model;
        }

        [HttpPost]
        public List<ConsignmentItemEditModel> CreateConsignmentItem(List<ConsignmentItemEditModel> models)
        {
            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });

            for (int i = 0; i < models.Count; i++)
            {
                _consignmentOrchestra.CreateConsignmentItem(models[i]);
            }
            
            _unitofWork.SaveChanges();

            return models;
        }

        [HttpPost]
        [PrimeActsAuthorizeAttributeAPI(OperationKey = "Consignment-EditConsignmentItem")]
        public List<ConsignmentItemEditModel> EditConsignmentItem(List<ConsignmentItemEditModel> models)
        {
            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });

            var allowedToModifyCost = user.Permissions.Any(p => ("Consignment-EditConsignmentItemCost").Equals(p.PermissionController + "-" + p.PermissionAction));

            for (int i = 0; i < models.Count; i++)
            {
                _consignmentOrchestra.UpdateConsignmentItemForEdit(models[i], allowedToModifyCost);
            }
            
            _unitofWork.SaveChanges();

            return models;
        }

        [HttpGet]
        public ConsignmentItemBasicModel ConsignmentItemBasic(Guid id)
        {
            return _consignmentOrchestra.GetConsignmentItemBasic(id);
        }

//        [HttpPost]
//        public List<ConsignmentItemEditModel> SaveConsignmentItems(List<ConsignmentItemEditModel> models)
//        {
//            if (models == null && !models.Any())
//            {
//                return models;
//            }

//            foreach (var t in models)
//            {
////models[i].ConsignmentItemID = Guid.Parse("8a65e318-5568-4a26-97c2-14eab581a6d1");
//                //models[i].SupplierID = Guid.Parse("68041464-4812-3000-0076-000000000125");
//                if (t.ConsignmentItemID != null && t.ConsignmentItemID != Guid.Empty)
//                    _consignmentOrchestra.UpdateConsignmentItem(t);
//                else
//                    _consignmentOrchestra.CreateConsignmentItem(t);
//            }
//            _unitofWork.SaveChanges();

//            return models;
//        }



    }
}