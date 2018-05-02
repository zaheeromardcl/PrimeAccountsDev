using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
  
    public partial class UserTabPanel : PrimeActs.Infrastructure.BaseEntities.IObjectState
   
{
        public UserTabPanel()
        {
            
           
        }

        public System.Guid PanelID { get; set; }
        public System.Guid UserID { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string HoldingDiv { get; set; }
        public bool IsSelected { get; set; }
        public string JsonData { get; set; }
        public string ControllerState { get; set; }
        public string UriParam { get; set; }
        

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
