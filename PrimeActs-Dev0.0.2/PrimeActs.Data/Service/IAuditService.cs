using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface IAuditService : IService<Audit>
    {
        Audit AuditByReference(string Reference);
        //Audit AuditByConsignmentReference(string ConsignmentReference);
        //Audit AuditByTicketReference(string TicketReference);
        //Audit AuditByPurchaseInvoiceNumber(string PurchaseInvoiceNumber);
        Audit AuditById(Guid AuditId);
        List<Audit> GetAllAudits();
        void RefreshCache();
    }
}
