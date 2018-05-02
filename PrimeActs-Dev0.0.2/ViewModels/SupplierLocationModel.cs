﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels
{
    public class SupplierLocationModel
    {
        public Guid SupplierLocationId { get; set; }
        public string SupplierLocationName { get; set; }
        
        public AddressModels Address { get; set; }

        public string Telephone { get; set; }
        public string FaxNumber { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
