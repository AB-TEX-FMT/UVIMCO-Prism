using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Shared
{
    public class ReportGroup
    {
        /// <summary>
        /// Holds the ReportGroup ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Holds the ReportGroup Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holds the ReportGroup Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Holds the ReportGroup Security or VisibilityLevel
        /// </summary>
        public int VisibilityLevel { get; set; }

        /// <summary>
        /// Holds a List of all Report Definitions for this report group
        /// </summary>
        public List<ReportDef> ReportDefs { get; set; }
    }
}
