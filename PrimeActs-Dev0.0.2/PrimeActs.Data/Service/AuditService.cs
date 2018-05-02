using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class AuditService : Service<Audit>, IAuditService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Audit> _repository;

        public AuditService(IRepositoryAsync<Audit> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public Audit AuditByReference(string Reference)
        {
            Audit varAudit = new Audit();
            varAudit = _repository.Query().Select().Where(t => t.Reference == Reference).OrderByDescending(a => a.EditDate).FirstOrDefault();
            return varAudit;
        }

        //public Audit AuditByConsignmentReference(string ConsignmentReference)
        //{
        //    Audit varAudit = new Audit();            
        //    varAudit = _repository.Query().Select().Where(t => t.ConsignmentReference == ConsignmentReference).OrderByDescending(a => a.EditDate).FirstOrDefault();           
        //    return varAudit;
        //}

        //public Audit AuditByTicketReference(string TicketReference)
        //{
        //    Audit varAudit = new Audit();
        //    varAudit = _repository.Query().Select().Where(t => t.TicketReference == TicketReference).OrderByDescending(a => a.EditDate).FirstOrDefault();
        //    return varAudit;
        //}
        //public Audit AuditByPurchaseInvoiceNumber(string PurchaseInvoiceNumber)
        //{
        //    Audit varAudit = new Audit();
        //    varAudit = _repository.Query().Select().Where(t => t.PurchaseInvoiceNumber == PurchaseInvoiceNumber).OrderByDescending(a => a.EditDate).FirstOrDefault();
        //    return varAudit;
        //}

        public Audit AuditById(Guid AuditId)
        {
            Audit varAudit = new Audit();
            varAudit = _repository.Query().Select().Where(t => t.AuditID == AuditId).SingleOrDefault();
            return varAudit;
        }

        public List<Audit> GetAllAudits()
        {
            List<Audit> varAudit = new List<Audit>();
            varAudit = _repository.Query().Select().ToList(); 
            return varAudit;
        }

        public void RefreshCache()
        {
            //throw new NotImplementedException();
        }
    }
}
