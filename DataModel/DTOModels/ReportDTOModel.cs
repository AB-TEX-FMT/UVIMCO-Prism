using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Report  Data Transfer Object Model
    /// <para>ReportDefID (int) Holds the Definition ID of the requested Report</para>
    /// </summary>
    public class ReportDTOModel : BaseReportDTOModel
    {
        /// <summary>
        /// Holds the Definition ID of the Report
        /// </summary>
        public int ReportDefID { get; set; }

        public IReportData Items { get; set; }

    }
}
