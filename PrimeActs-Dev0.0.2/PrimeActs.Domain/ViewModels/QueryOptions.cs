namespace PrimeActs.Domain.ViewModels
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            CurrentPage = 1;
            PageSize = 15;

            SortField = "CreatedDate";
            SortOrder = ViewModels.SortOrder.DESC.ToString();
        }

        //[JsonProperty(PropertyName="currentPage")]
        public int CurrentPage { get; set; }

        //[JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; }

        //[JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        //[JsonProperty(PropertyName = "sortField")]
        public string SortField { get; set; }

        //[JsonProperty(PropertyName = "sortOrder")]
        public string SortOrder { get; set; }

        //[JsonIgnore]
        public string Sort
        {
            get
            {
                return string.Format("{0} {1}",
                    SortField, SortOrder);
            }
        }
    }
}