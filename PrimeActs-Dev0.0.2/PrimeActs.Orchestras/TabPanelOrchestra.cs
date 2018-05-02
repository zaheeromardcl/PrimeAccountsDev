#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrimeActs.Domain.ViewModels.Consignment;

#endregion

namespace PrimeActs.Orchestras
{
    public interface ITabPanelOrchestra
    {
        void Initialize(ApplicationUser principal);
       UserTabPanel GetTabPanel(Guid UserId, string name);
       List<UserTabPanel> GetAllTabPanels(Guid UserId);       
        void CreateTabPanel(UserTabPanel model);
        void UpdateTabPanel(UserTabPanel model);
        void DeleteTabPanel(UserTabPanel model);
        //void RefreshCache();
    }
    public class TabPanelOrchestra : ITabPanelOrchestra
    {
        private readonly ITabPanelService _tabPanelService;
        private ApplicationUser _principal;
        private readonly string _serverCode;
        private IAuditOrchestra _auditOrchestra;

        public TabPanelOrchestra(ITabPanelService tabPanelService, ISetupLocalService setupLocalService, IAuditOrchestra auditOrchestra)
        {
            _tabPanelService = tabPanelService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _auditOrchestra = auditOrchestra;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }


        public UserTabPanel GetTabPanel(Guid UserId, string Name) // the UI will only know the index
        {
            return _tabPanelService.TabPanelById(UserId, Name);
        }

        public List<UserTabPanel> GetAllTabPanels(Guid UserId)
        {
            return _tabPanelService.GetAllTabPanels(UserId);
        }

        public void CreateTabPanel(UserTabPanel model)
        {
            model.PanelID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            model.ObjectState = Infrastructure.BaseEntities.ObjectState.Added;
            
            _tabPanelService.Insert(model);           
        }

        public void UpdateTabPanel(UserTabPanel model)
        {
            model.ObjectState = Infrastructure.BaseEntities.ObjectState.Modified;

            //AuditSelect(model);
            
            _tabPanelService.Update(model);
        }

        public void DeleteTabPanel(UserTabPanel model)
        {
            AuditSelect(model);
            
            model.ObjectState = Infrastructure.BaseEntities.ObjectState.Deleted;
            _tabPanelService.Delete(model);
        }

        public class ConsignmentItem
        {
            public List<object> serverErrors { get; set; }
            public int Id { get; set; }
            public string ConsignmentID { get; set; }
            public string ConsignmentItemID { get; set; }
            public string CountryName { get; set; }
            public string OriginCountryID { get; set; }
            public string BestBeforeDate { get; set; }
            public string Brand { get; set; }
            public string Produce { get; set; }
            public string Department { get; set; }
            public string Rotation { get; set; }
            public string PackType { get; set; }
            public string PackWtUnitID { get; set; }
            public string PackWeight { get; set; }
            public string PackWtUnit { get; set; }
            public string PackSize { get; set; }
            public string PackPall { get; set; }
            public string ExpectedQuantity { get; set; }
            public string ReceivedQuantity { get; set; }
            public string EstimatedPercentageProfit { get; set; }
            public string EstimatedChargeCostPerPack { get; set; }
            public string Returns { get; set; }
            public string EstimatedPurchaseCostPerPack { get; set; }
            public string PorterageID { get; set; }
            public string Porterage { get; set; }
            public string ProduceID { get; set; }
            public string ProduceName { get; set; }
            public string Consignment { get; set; }
            public string NoteID { get; set; }
            public string NoteText { get; set; }
            public bool IsCountry { get; set; }
            public string DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string DepartmentCode { get; set; }
            public bool DepartmentNameFocused { get; set; }
            public List<ConsignmentItemArrival> ConsignmentItemArrivals { get; set; }
            public List<object> errors { get; set; }
            public List<object> Errors { get; set; }
            public bool ShowErrors { get; set; }
            public bool isUK { get; set; }
            public bool hasPackWeight { get; set; }
        }

        public class ConsignmentRootObject
        {
            public string ConsignmentReference { get; set; }
            public string ConsignmentDescription { get; set; }
            public string ConsignmentID { get; set; }
            public string ContractDate { get; set; }
            public string CountryID { get; set; }
            public string CountryName { get; set; }
            public string DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string DepartmentCode { get; set; }
            public string DespatchDate { get; set; }
            public string DespatchID { get; set; }
            public string DespatchName { get; set; }
            public bool DisplayDespatchLocation { get; set; }
            public bool DisplayPort { get; set; }
            public string Handling { get; set; }
            public bool IsSaved { get; set; }
            public string NoteID { get; set; }
            public string NoteText { get; set; }
            public string PortID { get; set; }
            public string PortName { get; set; }
            public string PurchaseTypeDescription { get; set; }
            public string PurchaseTypeID { get; set; }
            public string ReceivedDate { get; set; }
            public string SelectPurchaseType { get; set; }
            public string SupplierCompanyName { get; set; }
            public string SupplierDepartmentID { get; set; }
            public string SupplierDepartmentName { get; set; }
            public string SupplierID { get; set; }
            public string SupplierReference { get; set; }
            public string Vehicle { get; set; }
            public string VehicleDetail { get; set; }
            public string Commission { get; set; }
            public List<ConsignmentItem> ConsignmentItems { get; set; }
            public bool MultipleConsignmentItems { get; set; }
            public string DefaultDepartmentID { get; set; }
            public string DefaultDepartmentName { get; set; }
            public bool tedit { get; set; }
            public bool pedit { get; set; }
            public List<object> serverErrors { get; set; }
            public List<object> Errors { get; set; }
            public bool ShowErrors { get; set; }
            public bool SupplierHasFocus { get; set; }
        }

        protected void AuditConsignment(UserTabPanel model)
        {            
            _auditOrchestra.Initialize(_principal);
            _auditOrchestra.SaveConsignmentAudit(model);
        }

        protected void AuditTicket(UserTabPanel model)
        {
            _auditOrchestra.Initialize(_principal);
            _auditOrchestra.SaveTicketAudit(model);
        }

        protected void AuditPurchaseInvoice(UserTabPanel model)
        {
            _auditOrchestra.Initialize(_principal);
            _auditOrchestra.SavePurchaseInvoiceAudit(model);
        }

        protected void AuditSelect(UserTabPanel model)
        {
            if (model.ContentType == "Consignment" || model.ContentType == "ConsignmentEdit")
            {
                AuditConsignment(model);
            }
            if (model.ContentType == "CreateCreditTicket" || model.ContentType == "CreateCashTicket" || model.ContentType == "CreateReceipt")
            {
                AuditTicket(model);
            }
            if (model.ContentType == "CreatePurchaseInvoice")
            {
                AuditPurchaseInvoice(model);
            }
        }

        protected void AuditExperiments(UserTabPanel model)
        {
            var getalltest = GetAllTabPanels(model.UserID).Where(a => a.ContentType == "Consignment");

            List<ConsignmentRootObject> consList = new List<ConsignmentRootObject>();

            foreach (var tp in getalltest)
            {
                // var jsonToViewModel = JsonConvert.DeserializeObject<ConsignmentRootObject>(model.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var jsonToViewModel = JsonConvert.DeserializeObject<ConsignmentRootObject>(tp.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                // a few tests to determine if it should be saved ? try and look at tblConsignment
                if (jsonToViewModel.IsSaved == true && jsonToViewModel.ConsignmentItems.Count() > 0 && jsonToViewModel.ConsignmentItems[0].ProduceID != "" && jsonToViewModel.ConsignmentItems[0].ConsignmentItemArrivals[0].QuantityReceived > 0)
                {
                    consList.Add(jsonToViewModel);
                }
            }

            // if not Exist Audit Entry Then Create initial record
            if (model.ContentType == "Consignment")
            {
                // model.JsonData.Replace("")
                //int test1 = model.JsonData.IndexOf("ShowVehicleOnInvoice");
                //if (test1 > 0)
                //{
                //    model.JsonData = model.JsonData.Remove(test1 - 2, 26);
                //}

                JObject jsonO = JObject.Parse(model.JsonData); //
                // var jsonToViewModel = JsonConvert.DeserializeObject<ConsignmentViewModel>(model.JsonData);
                // DC Note, using ConsignmentEditModel did not deserialize properly, the Consignment Items were not being populated due to the constructors, easier just to let Newtonsoft create them using a more vanilla version of dependent objects
                //var jsonToViewModel2 = JsonConvert.DeserializeObject<ConsignmentEditModel>(model.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var jsonToViewModel3 = JsonConvert.DeserializeObject<ConsignmentRootObject>(model.JsonData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }

            // If Exists then get get Existing, to get before image

            // Perform Deltas on what has changed

        }

    //    private TabPanel ApplyChanges(TabPanel model)
    //    {
    //        var tabpanel = new TabPanel
    //        {
    //        //    TicketID =
    //        //        Guid.Empty != model.TabPanelID ? model.TabPanelID : PrimeActs.Service.IDGenerator.NewGuid(),
    //        //}
    //        }
    }
}
