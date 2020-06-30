using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.Shared
{
    /// <summary>
    /// Compnent Table Options
    /// </summary>
    public class ComponentTableOptions
    {
        #region Setup
        public ComponentTableOptions()
        {
            AllowHTML = true;
            AlternatingRowStyle = true;
            FirstRowNumber = 1;
            FrozenColumns = 0;
            PageSize = 10;
            PagingButtons = "auto";
            ShowRowNumber = false;
            Sort = "enable";
            SortAscending = true;
            SortColumn = -1;
            StartPage = 1;
        }
        #endregion

        /// <summary>
        /// Boolean, should usually be set to true or most of the formatting will not work
        /// Default = True
        /// </summary>
        public bool AllowHTML { get; set; }

        /// <summary>
        /// Boolean, Determines whether the rows in the table are striped
        /// Default = True
        /// </summary>
        public bool AlternatingRowStyle { get; set; }

        /// <summary>
        /// Int, The row number for the first row in the dataTable. Used only if showRowNumber is true.
        /// Default = 1
        /// </summary>
        public int FirstRowNumber { get; set; }

        /// <summary>
        /// Int, The number of columns from the left that will be frozen. These columns will remain in place 
        /// when scrolling the remaining columns horizontally. If showRowNumber is false, setting 
        /// frozenColumns to 0 will appear the same as if set to null, but if showRowNumber is set to true, 
        /// the row number column will be frozen.
        /// Default = null (0)
        /// </summary>
        public int FrozenColumns { get; set; }

        /// <summary>
        /// Int, The number of rows in each page, when paging is enabled with the page option.
        /// Default = 10
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Sets a specified option for the paging buttons. The options are as follows:
        ///'both' - enable prev and next buttons
        ///'prev' - only prev button is enabled
        ///'next' - only next button is enabled
        ///'auto' - the buttons are enabled according to the current page.On the first page only next is 
        ///         shown.On the last page only prev is shown.Otherwise both are enabled.
        ///number - the number of paging buttons to show.This explicit number will override computed number 
        ///         from pageSize.
        ///Default = 'auto'
        /// </summary>
        public string PagingButtons { get; set; }

        /// <summary>
        /// Boolean, If set to true, shows the row number as the first column of the table.
        /// Default = False
        /// </summary>
        public bool ShowRowNumber { get; set; }

        /// <summary>
        /// String, 	
        ///If and how to sort columns when the user clicks a column heading.If sorting is enabled, consider 
        ///         setting the sortAscending and sortColumn properties as well.Choose one of the following 
        ///         string values:
        ///'enable' - [Default] Users can click on column headers to sort by the clicked column. When users 
        ///         click on the column header, the rows will be automatically sorted, and a 'sort' event 
        ///         will be triggered.
        ///'event' - When users click on the column header, a 'sort' event will be triggered, but the rows 
        ///         will not be automatically sorted.This option should be used when the page implements its 
        ///         own sort.See the TableQueryWrapper example for an example of how to handle sorting events 
        ///         manually.
        ///'disable' - Clicking a column header has no effect.
        /// Default = enable
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Boolean, The order in which the initial sort column is sorted. True for ascending, false for 
        ///         descending. Ignored if sortColumn is not specified.
        /// Default = True
        /// </summary>
        public bool SortAscending { get; set; }

        /// <summary>
        /// Int, An index of a column in the data table, by which the table is initially sorted. The column 
        ///         will be marked with a small arrow indicating the sort order.
        /// Default = -1
        /// </summary>
        public int SortColumn { get; set; }

        /// <summary>
        /// Int, The first table page to display. Used only if page is in mode enable/event.
        /// Default = 0
        /// </summary>
        public int StartPage { get; set; }
    }
}
