using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Shared
{
    public class Column
    {
        #region Class Setup
        public Column()
        { }
        public Column(string displayName, string dataExample, string colName, DataTypeValue dataType) : this()
        {
            DisplayName = displayName;
            DataExample = dataExample;
            ColName = colName;
            DataType = dataType;
        }
        public Column(string displayName, string dataExample, string colName, DataTypeValue dataType, bool sortable) : this(displayName, dataExample, colName, dataType)
        {
            Sortable = sortable;
        }
        public Column(string displayName, string dataExample, string colName, DataTypeValue dataType, bool sortable, bool orderable) : this(displayName, dataExample, colName, dataType, sortable)
        {
            Orderable = orderable;
        }
        public Column(string displayName, string dataExample, string colName, DataTypeValue dataType, bool sortable, bool orderable, bool visible) : this(displayName, dataExample, colName, dataType, sortable, orderable)
        {
            IsVisible = visible;
        }
        #endregion

        /// <summary>
        /// Possible values for DataType
        /// </summary>
        public enum DataTypeValue { Int, String, Date, Bool, Decimal, Currency }

        /// <summary>
        /// Holds the Onscreen Display value
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Holds the DataExample
        /// </summary>
        public string DataExample { get; set; }

        /// <summary>
        /// Holds the ColumnName
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// Holds the DataType
        /// Possible values int:string:date
        /// </summary>
        public DataTypeValue DataType { get; set; }

        /// <summary>
        /// Determines whether the Column is sortable
        /// </summary>
        public bool Sortable { get; set; }

        /// <summary>
        /// Determines whether the Column is Orderable
        /// </summary>
        public bool Orderable { get; set; }

        /// <summary>
        /// Determines whether the Column is Visible
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
