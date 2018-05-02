#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Data.Contexts;


#endregion

namespace PrimeActs.Data.Service
{
    public class ConsignmentItemService : Service<ConsignmentItem>, IConsignmentItemService
    {
        private readonly IRepositoryAsync<ConsignmentItem> _repository;

        public ConsignmentItemService(IRepositoryAsync<ConsignmentItem> repository)
        : base(repository)
        {
            _repository = repository;
        }

        public List<ConsignmentItem> ConsignmentItemsByConsignmentID(Guid id)
        {
            return
                _repository.Query(x => x.ConsignmentID == id)
                    .Include(inc => inc.Produce)
                    .Include(inc => inc.Note)
                    .Include(inc => inc.Consignment)
                    .Include(inc => inc.Porterage)
                    .Select()
                    .ToList();
        }


        public ConsignmentItem ConsignmentItemByID(Guid id)
        {
            return
                _repository.Query(x => x.ConsignmentItemID == id)
                    .Include(inc => inc.Produce)
                    .Include(inc => inc.Note)
                    .Include(inc => inc.Consignment)
                    .Include(inc => inc.Porterage)
                    .Select()
                    .FirstOrDefault();
        }

        public ConsignmentItem ConsignmentItemByIDSimple(Guid id)
        {
            var consignmentItemByIDSimple = _repository.Query(x => x.ConsignmentItemID == id)
                .Include(inc => inc.Consignment)
                .Select()
                .FirstOrDefault();
            return consignmentItemByIDSimple;
        }

        public List<ConsignmentItem> GetAllConsignmentItems()
        {
            return
                _repository.Query()
                    .Include(inc => inc.Produce)
                    .Include(inc => inc.Note)
                    .Include(inc => inc.Consignment)
                    .Include(inc => inc.Porterage)
                    .Select()
                    .ToList();
        }

        public int ConsignmentItemsReceivedQuantity(Guid ConsignmentItemID)
        {
            //string qry = "select ConsignmentItemID, TotalReceived from vwConsignmentReceived where ConsignmentItemID = @p0";
            //var @p0 = ConsignmentItemID;
            
            //var totalReceived =
            //    _repository.SelectQuery(qry, p0).ToList();
            int rtnVal = 0;
            using (var context = new PrimeActs.Data.Contexts.PAndIContext())
            {
                string qry = "select TotalReceived from vwConsignmentReceived where ConsignmentItemID = @p0";
                var @p0 = ConsignmentItemID;
                var recordsReturned = context.Database.SqlQuery<int>(qry, @p0).FirstOrDefault();
                rtnVal = recordsReturned;
            }

            return rtnVal;
        }

        public List<ConsignmentItem> ConsignmentItemsByProduceForDateRange(Guid ProduceID, Guid optDepartment = default(Guid))
        {
            DateTime testdate = DateTime.Today;
            DateTime dateMinusSeven = DateTime.Today.AddDays(-1);

            //var consignmentItems =
            //    _repository.Query()
            //        .Include(inc => inc.Consignment)
            //        .Select()
            //        .Where(a => a.CreatedDate > dateMinusSeven)
            //        .Where(a => a.ProduceID == ProduceID)
            //        .ToList();
            //string qry = "select ConsignmentItemID,ConsignmentID,DepartmentID,BestBeforeDate,ProduceID,Brand,Rotation,PackType,PackWtUnitID,PackWeight,PackSize,PackPall,EstimatedProfit,EstimatedChargeCost,RetReduce,EstimatedPurchaseCost,ItemStatus,PorterageID,NoteID,FK1,FK2,Bit1,Bit2,String1,String2,Numeric1,Numeric2,Int1,Int2,UpdatedDate,OriginCountryID,CreatedDate,CreatedByUserID as CreatedBy,UpdatedByUserID as UpdatedBy,QuantityExpected from tblConsignmentItem where CreatedDate > @p0 and ProduceID = @p1";
            //string qry = "select ConsignmentItemID,ConsignmentID,DepartmentID,BestBeforeDate,ProduceID,Brand,Rotation,PackType,PackWtUnitID,PackWeight,PackSize,PackPall,EstimatedProfit,EstimatedChargeCost,RetReduce,EstimatedPurchaseCost,ItemStatus,PorterageID,NoteID,FK1,FK2,Bit1,Bit2,String1,String2,Numeric1,Numeric2,Int1,Int2,UpdatedDate,OriginCountryID,CreatedDate,CreatedByUserID as CreatedBy,UpdatedByUserID as UpdatedBy,QuantityExpected from vwCurrentConsignmentItem where ProduceID = @p1 and CreatedDate > @p0";
            string qry = "select ConsignmentItemID,ConsignmentID,DepartmentID,BestBeforeDate,ProduceID,Brand,Rotation,PackType,PackWtUnitID,PackWeight,PackSize,PackPall,EstimatedProfit,EstimatedChargeCost,RetReduce,EstimatedPurchaseCost,ItemStatus,PorterageID,NoteID,FK1,FK2,Bit1,Bit2,String1,String2,Numeric1,Numeric2,Int1,Int2,UpdatedDate,OriginCountryID,CreatedDate,CreatedByUserID as CreatedBy,UpdatedByUserID as UpdatedBy,QuantityExpected from vwCurrentConsignmentItem where ProduceID = @p1 ";
            string qryWithDepartment = "select ConsignmentItemID,ConsignmentID,DepartmentID,BestBeforeDate,ProduceID,Brand,Rotation,PackType,PackWtUnitID,PackWeight,PackSize,PackPall,EstimatedProfit,EstimatedChargeCost,RetReduce,EstimatedPurchaseCost,ItemStatus,PorterageID,NoteID,FK1,FK2,Bit1,Bit2,String1,String2,Numeric1,Numeric2,Int1,Int2,UpdatedDate,OriginCountryID,CreatedDate,CreatedByUserID as CreatedBy,UpdatedByUserID as UpdatedBy,QuantityExpected from vwCurrentConsignmentItem where ProduceID = @p1 and  DepartmentID = @p2";
            //string qry2 = "select ConsignmentItemID, ConsignmentID, DepartmentID from tblConsignmentItem where CreatedDate > @p0 and ProduceID = @p1";
            var @p0 = String.Format("{0:yyyy/MM/dd}", dateMinusSeven);
            var @p1 = ProduceID.ToString();
            var @p2 = optDepartment.ToString();

            if (optDepartment != default(Guid))
            {
                qry = qryWithDepartment;
           
            } // if optional DepartmentFilterSet

            // note: date range logic removed, was used while developing as test data meant too many rows were being returned
            var consignmentItems =
                _repository.SelectQuery(qry,p0,p1,p2);
                   // .Include(inc => inc.Consignment)
                    //.Select()
                   // .Where(a => a.CreatedDate > dateMinusSeven && a.ProduceID == ProduceID).ToList();
            //var testqry = _repository.SelectQuery(qry2, p0, p1);

            //var listtest = testqry.ToList();
            //List<ConsignmentItem> concreteItems = new List<ConsignmentItem>();
            //foreach (var item in consignmentItems)
            //{
            //    concreteItems.Add(item);
            //}
            //List<ConsignmentItem> concreteItems = consignmentItems.ToList();    
            var testbreak = consignmentItems.Count();
            return consignmentItems.ToList();
        }

        public List<ConsignmentItem> ConsignmentItemsReport(Guid ProduceID)
        {
            DateTime testdate = DateTime.Today;
            DateTime dateMinusSeven = DateTime.Today.AddDays(-7);

            var consignmentItems =
                _repository.Query()
                // .Include(inc => inc.Consignment)
                    .Select()
                    .Where(a => a.CreatedDate > dateMinusSeven && a.ProduceID == ProduceID).ToList();
            return consignmentItems;
        }
    }
}