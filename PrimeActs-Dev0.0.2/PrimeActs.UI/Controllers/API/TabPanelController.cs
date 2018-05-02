using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Web.Script.Serialization;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.UI.Controllers.API
{
    public class TabPanelController : ApiController
    {
        private readonly ITabPanelOrchestra _tabPanelOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public TabPanelController(ITabPanelOrchestra tabPanelOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _tabPanelOrchestra = tabPanelOrchestra;
            _unitofWork = unitofWork;
        }

        public class TabPanel
        {
            public TabPanel() { }
            public string name { get; set; }
            public string contentType { get; set; }
            public string holdingDiv { get; set; }
            public bool isSelected { get; set; }
            public string controllerState { get; set; }
            public string UriParam { get; set; }
        }
        
        // GET: api/TabPanel Return Json list of Panels for user
        public string Get()
        {            
            return "Not implemented";
        }

        // GET: api/TabPanel/5 get individual Panel saved data

        public string Get(int id, string name)
        {
            var user = User.Identity.GetApplicationUser();
            var userid = user.Id;
          
            string tabPanelJson ="";
         
            try
            {
                var tabPanel = _tabPanelOrchestra.GetTabPanel(userid,id.ToString());
                if (tabPanel != null)
                {
                    tabPanelJson = tabPanel.JsonData;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex); 
            }
            return tabPanelJson;
        }

        // POST: api/TabPanel
        public bool Post([FromBody]FormDataCollection formbody)
        {

            var panelId = formbody.GetValues("id")[0];
            var jsonData = formbody.GetValues("jsondata")[0];
            var panelName = formbody.GetValues("panelname")[0];
            var controllerState = formbody.GetValues("controller_state")[0];
            var contentType = formbody.GetValues("content_type")[0];

            var uriParam = "";
            var uriParamValues = formbody.GetValues("uriParam");
            if (uriParamValues != null && uriParamValues.Length > 0)
            {
                uriParam = uriParamValues[0];
            }

            var firstPageInit = formbody.GetValues("initial")[0];

            var user = User.Identity.GetApplicationUser();
            var userid = user.Id;

            PrimeActs.Domain.UserTabPanel existingPanel = _tabPanelOrchestra.GetTabPanel(userid, panelId);
           
            if (firstPageInit == "false" && existingPanel != null)
            {
                existingPanel.JsonData = jsonData;
                existingPanel.ControllerState = controllerState;
                _tabPanelOrchestra.Initialize(user);
                _tabPanelOrchestra.UpdateTabPanel(existingPanel);
                _unitofWork.SaveChanges();
            }
            if(firstPageInit == "true" && existingPanel == null) // create and prevent async double writes
            {
                PrimeActs.Domain.UserTabPanel newPanel = new PrimeActs.Domain.UserTabPanel();
                newPanel.ContentType = contentType;
                newPanel.ControllerState = controllerState;
                newPanel.HoldingDiv = panelName;
                newPanel.Name = panelId;
                newPanel.UserID = userid;
                newPanel.JsonData = jsonData;
                newPanel.UriParam = uriParam;
                _tabPanelOrchestra.Initialize(user);
                _tabPanelOrchestra.CreateTabPanel(newPanel);
                _unitofWork.SaveChanges();
            }

            return true;
        }
         [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("Create)")]
        public string Create(int id)
        {
            var test = id;
            return new JavaScriptSerializer().Serialize(test); ;
        }

        // PUT: api/TabPanel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TabPanel/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var user = User.Identity.GetApplicationUser();
            var userid = user.Id;
            PrimeActs.Domain.UserTabPanel existingPanel = _tabPanelOrchestra.GetTabPanel(userid, id.ToString());
            if (existingPanel != null)
            {
                _tabPanelOrchestra.Initialize(user);
                _tabPanelOrchestra.DeleteTabPanel(existingPanel);
                _unitofWork.SaveChanges();
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }       
    }
}
