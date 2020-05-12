using DataModel.Shared;
using NPoco;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.NPocoRepository.MapObjects
{
    public class ColumnMapping
    {
        /// <summary>
        /// Holds the ReportGroup ID
        /// </summary>
        public int ReportComponent_ID { get; set; }

        /// <summary>
        /// Holds the RC_Column Name
        /// </summary>
        public string RC_Column { get; set; }

        /// <summary>
        /// Holds the ColumnBold
        /// </summary>
        /// Values = Y,N, Null
        public string ColumnBold { get; set; }

        /// <summary>
        /// Holds the ColumnItalic
        /// </summary>
        /// Values = Y,N, Null
        public string ColumnItalic { get; set; }

        /// <summary>
        /// Holds the ColumnToolTipLabel
        /// </summary>
        public string ColumnToolTipLabel { get; set; }

        /// <summary>
        /// Holds the ColumnHide
        /// </summary>
        /// Values = Y,N, Null
        public string ColumnHide { get; set; }

        /// <summary>
        /// Holds the ColumnDisplayName
        /// </summary>
        public string ColumnDisplayName { get; set; }

        /// <summary>
        /// Holds the ColumnSortable
        /// </summary>
        /// Values = Y,N, Null
        public string ColumnSortable { get; set; }

        /// <summary>
        /// Holds the ColumnOrderable
        /// </summary>
        /// Values = Y,N, Null
        public string ColumnOrderable { get; set; }

        public Column GetColumn()
        {
            Column col = new Column()
            {
                ColName = RC_Column,
                DisplayName = ColumnDisplayName,
                IsVisible = (ColumnHide == "Y" ? true : false),
                Sortable = (ColumnSortable == "Y" ? true : false),
                Orderable = (ColumnOrderable == "Y" ? true : false),
                Bold = (ColumnBold == "Y" ? true : false),
                Italic = (ColumnItalic == "Y" ? true : false),
            };
            return col;
        }
    }
}
