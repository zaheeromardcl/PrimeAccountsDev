#region

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using PrimeActs.Domain.ViewModels.Users;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class TransferCreateViewModel
    {
        public Guid TicketID { get; set; }
        public string TicketReference { get; set; }
        public string TicketDate { get; set; }
        public Guid CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public Guid CustomerDepartmentID { get; set; }
        public Guid SalesPersonUserID { get; set; }
        public string SalesPersonName { get; set; }
        public Guid? NoteID { get; set; }
        public string Notes { get; set; }

        public List<TicketItemEditModel> TicketItems { get; set; }

        public TransferCreateViewModel()
        {
            TicketItems = new List<TicketItemEditModel>();
        }
    }

    public class CreateTransferPageViewModel
    {
        public List<TransferType> TransferTypeList { get; set; }
        public TransferCreateViewModel TransferCreateModel { get; set; }
        public UserContextModel UserContextModel { get; set; }
    }
}