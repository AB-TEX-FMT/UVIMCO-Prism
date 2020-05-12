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
    public class ComponentDTOModel : BaseModel
    {

        /// <summary>
        /// Holds the Component
        /// </summary>
        public Component Item { get; set; }

    }
}
