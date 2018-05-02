using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Customer
{
    public class CustomerLocationModel
    {
        public Guid CustomerLocationID { get; set; }
        public string x_CustomerLocationID { get; set; }
        public string CustomerLocationName { get; set; }
        public Guid CustomerID { get; set; }
        public string Telephone { get; set; }
        public string FaxNumber { get; set; }
        public Nullable<Guid> NoteID { get; set; }
        public string Notes { get; set; }
        public string NoteDescription { get; set; }
        public Guid AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Postcode { get; set; }
        public string PostalTown { get; set; }
        public string CountyCity { get; set; }
        //---!!!---???
        public AddressModels Address { get; set; }
        // one CustomerDepartmentName for several customerLocation(s)
        public string CustomerDepartmentName { get; set; }
        //---???---!!!
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        // flags
        public bool ItemAdding { get; set; }
        public bool ItemDeleting { get; set; }
    }

    public class CustomerLocationModelList
    {
        public List<CustomerLocationModel> CustomerLocations { get; set; }
    }
}
