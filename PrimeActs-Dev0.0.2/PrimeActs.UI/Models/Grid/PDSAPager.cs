#region

using System;

#endregion

namespace PrimeActs.UI.Models.Grid
{
    public class PDSAPager
    {
        #region Init Method

        public void Init()
        {
            PageSize = 5;
            PageIndex = 0;
            StartingRow = 1;
            TotalPages = 0;
            TotalRecords = 0;
        }

        #endregion

        #region CalculateTotalPages Method

        public void CalculateTotalPages()
        {
            if (PageSize > 0)
            {
                TotalPages = Convert.ToInt32(
                    Math.Ceiling(
                        Convert.ToDecimal(TotalRecords)/
                        Convert.ToDecimal(PageSize)));
            }
        }

        #endregion

        #region SetPagerProperties Method

        public void SetPagerProperties(string argument)
        {
            var page = -1;

            if (int.TryParse(argument, out page))
            {
                PageIndex = page;
            }
            else
            {
                switch (argument)
                {
                    case PDSAPagerCommands.First:
                        PageIndex = 0;
                        break;

                    case PDSAPagerCommands.Next:
                        if (PageIndex < TotalPages)
                        {
                            PageIndex++;
                        }
                        break;

                    case PDSAPagerCommands.Previous:
                        if (PageIndex != 0)
                        {
                            PageIndex--;
                        }
                        break;

                    case PDSAPagerCommands.Last:
                        PageIndex = TotalPages - 1;
                        break;
                }
            }

            StartingRow = (PageIndex*PageSize);
        }

        #endregion

        #region Constructor

        public PDSAPager()
        {
            Init();
        }

        public PDSAPager(int pageSize)
        {
            Init();

            PageSize = pageSize;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Get/Set the page size selected
        /// </summary>
        private int _PageSize;

        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = value;
                CalculateTotalPages();
            }
        }

        /// <summary>
        ///     Get/Set the Current Page Index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     Get/set the row to start at
        /// </summary>
        public int StartingRow { get; set; }

        /// <summary>
        ///     Get/Set the total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        ///     Get/Set the total records read
        /// </summary>
        private int _TotalRecords;

        public int TotalRecords
        {
            get { return _TotalRecords; }
            set
            {
                _TotalRecords = value;
                CalculateTotalPages();
            }
        }

        #endregion
    }
}