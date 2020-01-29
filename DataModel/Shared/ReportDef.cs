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
        public int ID { get; set; }

        /// <summary>
        /// Holds the ReportGroup ID
        /// </summary>
        public int ReportGroupID { get; set; }

        /// <summary>
        /// Holds the ReportDef Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holds the ReportDef Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Holds the URL for the Report
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool RenderNatively { get; set; }
    }
}
