#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PrimeActs.Domain.ViewModels.Division;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class PermissionShort
    {
        public string PermissionController { get; set; }
        public string PermissionAction { get; set; }
    }
}
