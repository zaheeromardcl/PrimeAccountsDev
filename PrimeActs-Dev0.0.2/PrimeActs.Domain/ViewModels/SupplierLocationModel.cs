
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels
{
    public class SupplierLocationModel
    {
        public Guid SupplierLocationID { get; set; }
        public string x_SupplierLocationID { get; set; }
        public string SupplierLocationName { get; set; }
        public Guid SupplierID { get; set; }
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
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool ItemAdding { get; set; }
        public bool ItemDeleting { get; set; }
    }

    public class SupplierLocationEditModelList
    {
        public List<SupplierLocationModel> SupplierLocations { get; set; }
    }
}
