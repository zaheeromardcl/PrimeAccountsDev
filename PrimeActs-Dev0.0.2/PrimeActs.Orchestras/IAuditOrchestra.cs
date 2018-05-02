using PrimeActs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Orchestras
{
    public interface IAuditOrchestra
    {
        void SaveConsignmentAudit(UserTabPanel model);
        void SaveTicketAudit(UserTabPanel model);
        void SavePurchaseInvoiceAudit(UserTabPanel model);
        void Initialize(ApplicationUser principal);
    }
}
