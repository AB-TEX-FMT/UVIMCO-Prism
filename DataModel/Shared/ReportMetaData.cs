using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.Shared
{
    /// <summary>
    /// Report Meta Data 
    /// <para>ReportTitle (string) Holds the Title of the Report</para>
    /// <para>ReportHeader (string) Holds the Header of the Report</para>
    /// <para>ReportFootNote (string) Holds the FootNote of the Report</para>
    /// </summary>
    public class ReportMetaData
    {
        /// <summary>
        /// Holds the Title of the Report
        /// </summary>
        public string ReportTitle { get; set; }

        /// <summary>
        /// Holds the Header of the Report
        /// </summary>
        public string ReportHeader { get; set; }

        /// <summary>
        /// Holds the Footnte of the Report
        /// </summary>
        public string ReportFootNote { get; set; }
    }
}
