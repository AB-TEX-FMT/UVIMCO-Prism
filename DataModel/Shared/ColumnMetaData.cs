using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Shared
{
    public class ColumnMetaData
    {
        /// <summary>
        /// Hold the toatal number of items of data
        /// </summary>
        public long TotalItems { get; set; }
        /// <summary>
        /// Holds a list of all columns by which the result can be sorted
        /// </summary>
        public List<Column> SortColumns { get; set; }
        /// <summary>
        /// Holds a list of all available columns
        /// </summary>
        public List<Column> AvailableColumns { get; set; }
        /// <summary>
        /// Holds a list of all available columns
        /// </summary>
        public List<Column> SelectedColumns { get; set; }

        public Column GetColumn(string colValue)
        {
            List<Column> cols = AvailableColumns;
            return cols.Find(x => x.ColName.Contains(colValue));
        }
    }
}