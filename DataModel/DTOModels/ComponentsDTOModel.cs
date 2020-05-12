using DataModel.BaseModels;
using DataModel.Shared;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Components Data Transfer Object Model
    /// <para>ReportDefID (int) Holds the Definition ID of the requested Report Component List</para>
    /// </summary>
    public class ComponentsDTOModel : BaseModel
    {
        /// <summary>
        /// Holds the Definition ID of the Report
        /// </summary>
        public string ReportDefID { get; set; }

        /// <summary>
        /// Holds a list of Components
        /// </summary>
        public List<ComponentMetaData> Items { get; set; }

    }
}
