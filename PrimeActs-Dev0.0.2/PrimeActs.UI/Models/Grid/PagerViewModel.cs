namespace PrimeActs.UI.Models.Grid
{
    public class PagerViewModel
    {
        #region Constructor

        public PagerViewModel()
        {
            Init();
        }

        #endregion

        #region Init Method

        public void Init()
        {
            Pager = new PDSAPager();

            SetPagerObject(11);
        }

        #endregion

        #region SetPagerObject Method

        private void SetPagerObject(int totalRecords)
        {
            // Set Pager Information
            Pager.TotalRecords = totalRecords;
            Pager.PageSize = 5;
            Pager.SetPagerProperties(string.Empty);

            // Build paging collection
            Pages = new PDSAPagerItemCollection(
                Pager.TotalRecords,
                Pager.PageSize,
                Pager.PageIndex);

            // Set total pages
            Pager.TotalPages = Pages.PageCount;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Get/Set the Pager object
        /// </summary>
        public PDSAPager Pager { get; set; }

        /// <summary>
        ///     Get/Set the page collection
        /// </summary>
        public PDSAPagerItemCollection Pages { get; set; }

        #endregion
    }
}