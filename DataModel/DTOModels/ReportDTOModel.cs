using DataModel.BaseModels;
using DataModel.Shared;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Report  Data Transfer Object Model
    /// <para>ReportDefID (int) Holds the Definition ID of the requested Report</para>
    /// </summary>
    public class ReportDTOModel : BaseModel
    {
        /// <summary>
        /// Holds the Definition ID of the Report
        /// </summary>
        public string ReportDefID { get; set; }

        /// <summary>
        /// Holds incoming/outgoing Report Meta Data parms
        /// </summary>
        public ReportMetaData ReportMetaData { get; set; }

    }
}
