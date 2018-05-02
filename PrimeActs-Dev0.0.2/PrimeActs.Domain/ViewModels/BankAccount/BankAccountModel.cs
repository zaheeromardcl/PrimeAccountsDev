#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels.BankAccount
{
    public class BankAccountModel
    {
        public Guid BankAccountID { get; set; }

        public Guid SupplierDepartmentId { get; set; }

        public string AccountName { get; set; }

        public string BankCode { get; set; }
        
        public string AccountNumber { get; set; }
        public Guid CountryID { get; set; }
      
    }

    public class BankAccountPagingModel
    {
        public ResultList<BankAccountModel> BankAccountEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class SearchObject
    {
        public string AccountName { get; set; }
        public string SupplierDepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        private string _recordsInDays { get; set; }
        public string RecordsInDays
        {
            get { return _recordsInDays; }
            set
            {
                _recordsInDays = value;
                if (!String.IsNullOrEmpty(value))
                {
                    switch (value)
                    {
                        case "CURRENTMONTH":
                            this.FromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                            this.ToDate = DateTime.Parse(FromDate.Value.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                        case "PREVIOUSMONTH":
                            DateTime tempDate = DateTime.Today.AddMonths(-1);
                            this.FromDate = new DateTime(tempDate.Year, tempDate.Month, 1);
                            this.ToDate = DateTime.Parse(FromDate.Value.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                        case "CURRENTYEAR":
                            this.FromDate = new DateTime(DateTime.Today.Year, 1, 1);
                            this.ToDate = DateTime.Parse(new DateTime(DateTime.Today.Year, 12, 31).ToString("yyyy-MM-dd 23:59:59.000"));

                            break;
                        case "LASTMONTH":
                            this.FromDate = DateTime.Today.AddMonths(-1);
                            this.ToDate = DateTime.Today;
                            break;
                        case "PREVIOUSYEAR":
                            this.FromDate = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1);
                            this.ToDate = DateTime.Parse(new DateTime(FromDate.Value.Year, 12, 31).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                        default:
                            this.FromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                            this.ToDate = DateTime.Parse(FromDate.Value.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                    }
                }

            }
        }

    }
}