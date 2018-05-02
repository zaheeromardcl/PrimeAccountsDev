#region

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System;
using System.Web;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Produce;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class ProduceController : ApiController
    {
        private readonly IProduceOrchestra _produceOrchestra;
        private IUnitOfWorkAsync _unitofWork;
        private PrimeActsUserManager _userManager;


        public ProduceController(IProduceOrchestra produceOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _produceOrchestra = produceOrchestra;
            _unitofWork = unitofWork;
        }

        private Guid? UserDivisionID
        {
            get { return GetLoggedInUsersSelectedDivisionID(); }
        }

        [HttpGet]
        public ResultList<ProduceEditModel> Index([FromUri] QueryOptions queryOptions,
            [FromUri] PrimeActs.Domain.ViewModels.Produce.SearchObject searchObject)
        {
            return _produceOrchestra.GetProduces(queryOptions, searchObject);
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            PopulateUser();
            return string.IsNullOrEmpty(search)
                ? null
                : _produceOrchestra.GetProduceModelsForAC(search)
                    .Select(
                        produce =>
                            new Autocomplete
                            {
                                Id = produce.ProduceID.ToString(),
                                label = produce.ProduceName,
                                value = produce.ProduceCode
                            })
                    .ToList();
        }

        /*********************************************************/
        //Work in progress - filter produce by selected departmentid
        /*********************************************************/
        //[AcceptVerbs("GET", "POST")]
        //[HttpGet]
        //public List<Autocomplete> AutoComplete(string search)
        //{
        //    PopulateUser();
        //    List<Autocomplete> produceoutput;
        //    Guid UserDepartmentID = User.Identity.GetApplicationUser().SelectedDepartmentId;

        //    produceoutput=string.IsNullOrEmpty(search)
        //        ? null
        //        : _produceOrchestra.GetProduceModelsForACByConsignmentItemDepartmentID(search, UserDepartmentID)
        //            .Select(
        //                produce =>
        //                    new Autocomplete
        //                    {
        //                        Id = produce.ProduceID.ToString(),
        //                        label = produce.ProduceName,
        //                        value = produce.ProduceCode
        //                    })
        //            .ToList();

        //    return produceoutput;
        //}
        /*********************************************************/



        //[AcceptVerbs("GET", "POST")]
        //[HttpGet]
        //public List<Autocomplete> AutoCompletePC(string search)
        //{
        //    return string.IsNullOrEmpty(search)
        //        ? null
        //        : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search)
        //            .Select(
        //                produce =>
        //                    new Autocomplete
        //                    {
        //                        Id = produce.ProduceCode.ToString(),
        //                        label =
        //                            produce.ProduceCode + " - " + produce.ProduceName + "-" + produce.RemainingQuantity
        //                    })
        //            .ToList();
        //}

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoCompletePC(string search)
        {
            PopulateUser();
          
            List<Autocomplete> redoutput;
            //FOR DEBUG PURPOSES
            redoutput = string.IsNullOrEmpty(search)
                ? null
                : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search, UserDivisionID).Where(a => a.RemainingQuantity > 0 || a.RemainingQuantity < 0)
                    .Select(
                        BuildAutocomplete)
                    .ToList();
            return redoutput;
            // return string.IsNullOrEmpty(search) ? null : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search).Select(produce => new Autocomplete { Id = produce.ProduceCode.ToString(), label = produce.ProduceCode + " - " + produce.ProduceName + "-" + produce.RemainingQuantity }).ToList();
        }
        
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoCompletePCZero(string search)
        {
            PopulateUser();

            List<Autocomplete> redoutput;
           
            //FOR DEBUG PURPOSES
            redoutput = string.IsNullOrEmpty(search)
                ? null
                : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search, UserDivisionID)//
                    .Select(BuildAutocomplete)
                    .ToList();
           
            return redoutput;
            // return string.IsNullOrEmpty(search) ? null : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search).Select(produce => new Autocomplete { Id = produce.ProduceCode.ToString(), label = produce.ProduceCode + " - " + produce.ProduceName + "-" + produce.RemainingQuantity }).ToList();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoCompletePurchaseInvoicePC(string search)
        {
            PopulateUser();

            List<Autocomplete> redoutput;
            //FOR DEBUG PURPOSES
            redoutput = string.IsNullOrEmpty(search)
                ? null
                : _produceOrchestra.GetProduceForAcForProduceQuantityForPurchaseInvoice(search, UserDivisionID).Where(a => a.RemainingQuantity > 0 || a.RemainingQuantity < 0)
                    .Select(
                        BuildAutocompleteForPurchaseInvoice)
                    .ToList();
            return redoutput;
            // return string.IsNullOrEmpty(search) ? null : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search).Select(produce => new Autocomplete { Id = produce.ProduceCode.ToString(), label = produce.ProduceCode + " - " + produce.ProduceName + "-" + produce.RemainingQuantity }).ToList();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoCompletePurchaseInvoicePCZero(string search)
        {
            PopulateUser();

            List<Autocomplete> redoutput;

            //FOR DEBUG PURPOSES
            redoutput = string.IsNullOrEmpty(search)
                ? null
                : _produceOrchestra.GetProduceForAcForProduceQuantityForPurchaseInvoice(search, UserDivisionID)//.Where( a => a.RemainingQuantity > 0)
                    .Select(BuildAutocompleteForPurchaseInvoice)
                    .ToList();

            return redoutput;
            // return string.IsNullOrEmpty(search) ? null : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search).Select(produce => new Autocomplete { Id = produce.ProduceCode.ToString(), label = produce.ProduceCode + " - " + produce.ProduceName + "-" + produce.RemainingQuantity }).ToList();
        }

        private static Autocomplete BuildAutocomplete(ProduceQuantityForTicket produce)
        {
            return new Autocomplete
            {
                Id = produce.ProduceCode.ToString() + "-" + produce.ConsignmentItemID,
                label = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                    produce.ConsignmentReference,
                    produce.SupplierCode,
                    produce.ProduceCode,
                    produce.RemainingQuantity,
                    produce.ItemBrand,
                    produce.ItemPackType,
                    produce.ItemPackSize,
                    produce.AvgUnitPrice,
                    produce.consDepartmentCode,
                    produce.itemReceived,
                    produce.Despatchdate),
                    //value = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                value = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                    produce.ConsignmentItemID.ToString(),
                    produce.PorterageID.ToString(),
                    produce.PorterageUnitAmount,
                    produce.PorterageMinimumAmount,
                     produce.ProduceCode,
                   // produce.ProduceCode, added 
                    // produce.ItemBrand, produce.ItemPackType, produce.ItemPackSize),
                    produce.ItemBrand, produce.ItemPackType, produce.ItemPackSize,produce.SupplierCode),
                Template = ""
            };
        }

        private static Autocomplete BuildAutocompleteForPurchaseInvoice(ProduceQuantityForTicket produce)
        {
            return new Autocomplete
            {
                Id = produce.ProduceCode.ToString() + "-" + produce.ConsignmentItemID,
                label = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                    produce.ConsignmentReference,
                    produce.SupplierCode,
                    produce.ProduceCode,
                    produce.RemainingQuantity,
                    produce.ItemBrand,
                    produce.ItemPackType,
                    produce.ItemPackSize,
                    produce.AvgUnitPrice,
                    produce.consDepartmentCode,
                    produce.itemReceived,
                    produce.Despatchdate
                    ),
                value = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    produce.ConsignmentItemID.ToString(),
                    produce.PorterageID.ToString(),
                    produce.PorterageUnitAmount,
                    produce.PorterageMinimumAmount,
                    produce.ItemBrand, produce.ItemPackType, produce.ItemPackSize, produce.Invoiced
                    ),
                Template = ""
            };
        }

        [HttpGet]
        public List<Autocomplete> AutoCompleteByCodeOrConsignmentReference(string search)
        {
            PopulateUser();

            var autoCompleteList = new List<Autocomplete>();
            if (!string.IsNullOrEmpty(search))
            {
                var produceList = _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search, Guid.Empty);
                autoCompleteList = produceList.Select(produce =>
                    new Autocomplete
                    {
                        Id = string.Format("{0} - {1}", produce.ProduceCode, produce.ConsignmentItemID),
                        label = string.Format("{0} - {1} ({2}) [ {3} ] {4} {5} {6} {7} {8}", produce.ConsignmentReference.Trim(), produce.ProduceName.Trim(), produce.ProduceCode.Trim(), produce.RemainingQuantity, produce.ItemBrand, produce.ItemPackType, produce.ItemPackSize, produce.AvgUnitPrice),
                        value = string.Format("{0},{1}", produce.ConsignmentItemID.ToString(), produce.ConsignmentReference)
                    })
                    .ToList();
            }

            return autoCompleteList;
        }

        public void PopulateUser()
        {
            var user = User.Identity.GetApplicationUser();
            _produceOrchestra.Initialize1(new PrimeActs.Domain.ApplicationUser
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
        }

        private Guid? GetLoggedInUsersSelectedDivisionID()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.Identity.GetApplicationUser().SelectedDivisionId;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}