#region

using System.Collections.Generic;

#endregion

namespace PrimeActs.UI.Models
{
    public class MenuModel
    {
        public MenuModel()
        {
            Permissions = new List<string>();
        }

        public virtual ICollection<string> Permissions { get; set; }
    }
}