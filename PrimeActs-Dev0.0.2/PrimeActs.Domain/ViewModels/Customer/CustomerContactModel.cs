using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Customer
{
    public class CustomerContactModel
    {
        public Guid CustomerContactID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ContactID { get; set; }
        public Nullable<System.Guid> SortOrder { get; set; } // --- !!!-???
        // Contact fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ContactType { get; set; }
        public string ContactReference { get; set; }
        public string EmailAddress { get; set; }
        public string DDITelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public Guid? NoteID { get; set; }
        public string Notes { get; set; }
        public string NoteDescription { get; set; }
        // common entity fields
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedByUserID { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedByUserID { get; set; }
        public List<string> SelectedLocationIds { get; set; }
        public List<string> SelectedDepartmentIds { get; set; }
        public List<LbxViewModel> LbxLocationOptions { get; set; }
        public List<LbxViewModel> LbxDepartmentOptions { get; set; }
        public bool ItemAdding { get; set; }
        public bool ItemDeleting { get; set; }
    }

    public class SupplierContactEditModelList
    {
        public List<SupplierContactEditModel> CustomerContacts { get; set; }
    }
}
