using DataModel.BaseModels;
using DataModel.Shared;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace DataModel.Shared
{
    /// <summary>
    /// Component Data Transfer Object Model
    /// <para>ComponentID (int) Holds the ID of the requested Component</para>
    /// </summary>
    public class Component : BaseModel
    {
        /// <summary>
        /// Holds the GUID of the Component
        /// </summary>
        public string ComponentGUID { get; set; }

        /// <summary>
        /// Hold the toatal number of items of data
        /// </summary>
        public long TotalItems { get; set; }

        public JArray Items { get; set; }

        /// <summary>
        /// Holds the Table Options of the Component
        /// </summary>
        public ComponentTableOptions TableOptions { get; set; }

        /// <summary>
        /// Holds the Chart Options of the Component
        /// </summary>
        public ComponentChartOptions ChartOptions { get; set; }

        /// <summary>
        /// Holds the Component's Meta Data
        /// </summary>
        public ComponentMetaData ComponentMetaData { get; set; }

        /// <summary>
        /// Holds incoming/outgoing Column Meta Data parms
        /// </summary>
        public ColumnMetaData ColumnMetaData { get; set; }

    }
}
