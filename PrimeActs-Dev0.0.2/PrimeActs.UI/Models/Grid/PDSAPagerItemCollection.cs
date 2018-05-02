#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.UI.Models.Grid
{
    /// <summary>
    ///     Class to hold a collection of pager items to display on a page
    /// </summary>
    public class PDSAPagerItemCollection : List<PDSAPagerItem>
    {
        #region Constructor

        public PDSAPagerItemCollection(int rowCount, int pageSize, int pageIndex)
        {
            // Calculate total pages based on RowCount and PageSize
            var pageCount = 0;

            pageCount = Convert.ToInt32(
                Math.Ceiling(
                    Convert.ToDecimal(rowCount)/
                    Convert.ToDecimal(pageSize)));

            // Initialize the collection of pager items
            Init(pageCount, pageIndex);
        }

        #endregion

        #region Public Properties

        public int PageCount { get; set; }

        #endregion

        #region Init Method

        private void Init(int pageCount, int pageIndex)
        {
            var itemIndex = 0;

            PageCount = pageCount;

            Add(new PDSAPagerItem(PDSAPagerCommands.FirstText,
                PDSAPagerCommands.First,
                (pageIndex == 0), PDSAPagerCommands.FirstTooltipText));
            itemIndex++;
            Add(new PDSAPagerItem(PDSAPagerCommands.PreviousText,
                PDSAPagerCommands.Previous,
                (pageIndex == 0), PDSAPagerCommands.PreviousTooltipText));
            itemIndex++;

            for (var i = 0; i < PageCount; i++)
            {
                Add(new PDSAPagerItem(i, pageIndex,
                    PDSAPagerCommands.PageText + " " + (i + 1)));
                itemIndex++;
            }

            Add(new PDSAPagerItem(PDSAPagerCommands.NextText,
                PDSAPagerCommands.Next,
                (PageCount - 1 == pageIndex), PDSAPagerCommands.NextTooltipText));
            itemIndex++;
            Add(new PDSAPagerItem(PDSAPagerCommands.LastText,
                PDSAPagerCommands.Last,
                (PageCount - 1 == pageIndex), PDSAPagerCommands.LastTooltipText));
        }

        #endregion
    }
}