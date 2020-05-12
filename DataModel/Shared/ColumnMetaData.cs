using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Shared
{
    public class ColumnMetaData
    {
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