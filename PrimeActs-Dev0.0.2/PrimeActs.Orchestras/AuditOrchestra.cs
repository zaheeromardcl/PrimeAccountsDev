using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrimeActs.Domain.ViewModels.Consignment;
using PrimeActs.Domain;
using PrimeActs.Data.Service;
using PrimeActs.Domain.AuditJson;

namespace PrimeActs.Orchestras
{
    public class AuditOrchestra : IAuditOrchestra
    {
        private readonly ITabPanelService _tabPanelService;
        private ApplicationUser _principal;
        private readonly string _serverCode;
        private readonly IAuditService _auditService;
        private IConsignmentService _consignmentService;
        
        public AuditOrchestra(AuditService auditService, ISetupLocalService setupLocalService, IConsignmentService consignmentService)
        {
            _auditService = auditService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _consignmentService = consignmentService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public void SaveConsignmentAudit(UserTabPanel model)
        {
            AuditConsignment(model);
        }

        public void SaveTicketAudit(UserTabPanel model)
        {
            AuditTicket(model);
        }

        public void SavePurchaseInvoiceAudit(UserTabPanel model)
        {
            AuditPurchaseInvoice(model);
        }

        protected Audit GetAuditObject(string ContentType, string JsonBefore, string JsonAfter, string Reference, Guid ID)
        {
            Audit auditEntry = new Audit
            {
                AuditID = Guid.NewGuid(),
                JsonDataBefore = JsonBefore,
                JsonDataAfter = JsonAfter,
                UserID = _principal.Id,
                ContentType = ContentType,
                DepartmentID = _principal.SelectedDepartmentId,
                EditDate = DateTime.Now,
                DivisionID = _principal.SelectedDivisionId,
                CompanyID = _principal.SelectedCompanyId,
                Reference = Reference,
                ReferenceID = ID
            };

            //if (ContentType == "Consignment")
            //{
            //    auditEntry.ConsignmentID = ID;
            //    auditEntry.ConsignmentReference = Reference;
            //}
            //if (ContentType == "CreateCreditTicket" || ContentType == "CreateCashTicket")
            //{
            //    auditEntry.TicketID = ID;
            //    auditEntry.TicketReference = Reference;
            //}
            //if (ContentType == "CreatePurchaseInvoice")
            //{
            //    auditEntry.PurchaseInvoiceID = ID;
            //    auditEntry.PurchaseInvoiceNumber = Reference;
            //}

            return auditEntry;
        }
        
        protected void AuditConsignment(UserTabPanel model)
        {
            var jsonToViewModel = JsonConvert.DeserializeObject<ConsignmentRootObject>(model.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //var auditEntry = _auditService.AuditByConsignmentReference(jsonToViewModel.ConsignmentReference);
            var auditEntry = _auditService.AuditByReference(jsonToViewModel.ConsignmentReference);
            //var divisionID = Guid.Parse(jsonToViewModel.DivisionID);
           
            if (auditEntry == null)
            {                
                var auditConsignment = GetAuditObject(model.ContentType, model.JsonData, model.JsonData, jsonToViewModel.ConsignmentReference, Guid.Parse(jsonToViewModel.ConsignmentID));
                _auditService.Insert(auditConsignment);
            }
            else
            {
                string oldAuditEntry = auditEntry.JsonDataAfter;
                if (string.IsNullOrEmpty(auditEntry.JsonDataAfter))
                {
                    oldAuditEntry = auditEntry.JsonDataBefore;
                }// if previous was first added
                
                // only write if something changed
                if (model.JsonData != oldAuditEntry)
                {                    
                    var auditConsignment = GetAuditObject(model.ContentType, oldAuditEntry, model.JsonData, jsonToViewModel.ConsignmentReference, Guid.Parse(jsonToViewModel.ConsignmentID));
                    _auditService.Insert(auditConsignment);
                }
            }            
        }

        protected void AuditTicket(UserTabPanel model)
        {
            var jsonToViewModel = JsonConvert.DeserializeObject<TicketRootObject>(model.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //var auditEntry = _auditService.AuditByTicketReference(jsonToViewModel.TicketReference);
            var auditEntry = _auditService.AuditByReference(jsonToViewModel.TicketReference);
            
            //Note Dynamic did not allow access to the child objects, tbc possibly as a strategy in future
            //dynamic dynoModel = JsonConvert.DeserializeObject<object>(model.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
           
            if (auditEntry == null) // first time audited
            {                
                var auditTicket = GetAuditObject(model.ContentType, model.JsonData, model.JsonData, jsonToViewModel.TicketReference, Guid.Parse(jsonToViewModel.TicketID));
                _auditService.Insert(auditTicket);
            }
            else // update/edit
            {
                string oldAuditEntry = auditEntry.JsonDataAfter;
                if (string.IsNullOrEmpty(auditEntry.JsonDataAfter))
                {
                    oldAuditEntry = auditEntry.JsonDataBefore;
                }// if previous was first added

                // only write if something changed
                if (model.JsonData != oldAuditEntry)
                {                   
                    var auditTicket = GetAuditObject(model.ContentType, oldAuditEntry, model.JsonData, jsonToViewModel.TicketReference, Guid.Parse(jsonToViewModel.TicketID));
                    _auditService.Insert(auditTicket);
                }
            }
        }

        protected void AuditPurchaseInvoice(UserTabPanel model)
        {
            var jsonToViewModel = JsonConvert.DeserializeObject<PurchaseInvoiceRootObject>(model.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //var auditEntry = _auditService.AuditByPurchaseInvoiceNumber(jsonToViewModel.PurchaseInvoiceNumber);
            var auditEntry = _auditService.AuditByReference(jsonToViewModel.PurchaseInvoiceReference);
          
            if (auditEntry == null) // first time audited
            {
                Guid guid;
                Guid.TryParse(jsonToViewModel.PurchaseInvoiceId, out guid);
                var auditTicket = GetAuditObject(model.ContentType, model.JsonData, model.JsonData, jsonToViewModel.PurchaseInvoiceReference, guid);
                _auditService.Insert(auditTicket);
            }
            else // update/edit
            {
                string oldAuditEntry = auditEntry.JsonDataAfter;
                if (string.IsNullOrEmpty(auditEntry.JsonDataAfter))
                {
                    oldAuditEntry = auditEntry.JsonDataBefore;
                }// if previous was first added

                // only write if something changed
                if (model.JsonData != oldAuditEntry)
                {
                    Guid guid;
                    Guid.TryParse(jsonToViewModel.PurchaseInvoiceId, out guid);
                    var auditTicket = GetAuditObject(model.ContentType, oldAuditEntry, model.JsonData, jsonToViewModel.PurchaseInvoiceReference, guid);
                    _auditService.Insert(auditTicket);
                }
            }
        }
    }
}
