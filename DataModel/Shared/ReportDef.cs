using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Shared
{
    public class ReportDef
    {
        /// <summary>
        /// Holds the ReportDef ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Holds the ReportGroup ID
        /// </summary>
        public string ReportGroupID { get; set; }

        /// <summary>
        /// Holds the ReportDef Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holds the ReportDef Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Holds the ReportGroup Security or VisibilityLevel
        /// </summary>
        public int VisibilityLevel { get; set; }
    }
}