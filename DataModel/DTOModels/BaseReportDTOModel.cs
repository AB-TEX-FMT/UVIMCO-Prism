using System;
using DataModel.BaseModels;
using DataModel.Shared;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Base Data Transfer Object Model
    /// <para>ColumnMetaData (ColumnMetaData) Holds incoming/outgoing Column Meta Data parms</para>
    /// <para>ReportMetaData (ReportMetaData) Holds incoming/outgoing Report Meta Data parms</para>
    /// </summary>
    [Serializable()]
    public class BaseReportDTOModel : BaseModel
    {
        /// <summary>
        /// parameterless default constructor only for serialization
        /// </summary>
        public BaseReportDTOModel() { }

        /// <summary>
        /// Holds incoming/outgoing Column Meta Data parms
        /// </summary>
        public ColumnMetaData ColumnMetaData { get; set; }

        /// <summary>
        /// Holds incoming/outgoing Report Meta Data parms
        /// </summary>
        public ReportMetaData ReportMetaData { get; set; }
    }
}