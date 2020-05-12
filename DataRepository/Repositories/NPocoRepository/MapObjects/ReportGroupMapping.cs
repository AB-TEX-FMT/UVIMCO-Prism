using DataModel.Shared;
using NPoco;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.NPocoRepository.MapObjects
{
    [PrimaryKey("RG_ID")]
    public class ReportGroupMapping
    {
        /// <summary>
        /// Holds the ReportGroup ID
        /// </summary>
        public int RG_ID { get; set; }

        /// <summary>
        /// Holds the ReportGroup Name
        /// </summary>
        public string RG_Name { get; set; }

        /// <summary>
        /// Holds the ReportGroup Description
        /// </summary>
        public string RG_Description { get; set; }

        /// <summary>
        /// Holds the ReportGroup Security or VisibilityLevel
        /// </summary>
        public int RG_VisibilityLevel { get; set; }

        /// <summary>
        /// Holds a List of all Report Definitions for this report group
        /// </summary>
        [Reference(ReferenceType.Many, ColumnName = "RG_ID", ReferenceMemberName = "RG_ID")]
        public List<ReportDefMapping> ReportDefMappings { get; set; }
    }
}
