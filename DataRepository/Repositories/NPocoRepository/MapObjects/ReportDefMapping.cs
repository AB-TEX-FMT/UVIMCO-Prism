using DataModel.Shared;
using NPoco;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.NPocoRepository.MapObjects
{
    public class ReportDefMapping
    {
        /// <summary>
        /// Holds the ReportDef ID
        /// </summary>
        public int R_ID { get; set; }

        /// <summary>
        /// Holds the ReportGroup ID
        /// </summary>
        public int RG_ID { get; set; }

        /// <summary>
        /// Holds the ReportDef Name
        /// </summary>
        public string R_Name { get; set; }

        /// <summary>
        /// Holds the ReportDef Description
        /// </summary>
        public string R_Description { get; set; }


        /// <summary>
        /// Holds the Report Security or VisibilityLevel
        /// </summary>
        public int R_VisibilityLevel { get; set; }
    }
}
