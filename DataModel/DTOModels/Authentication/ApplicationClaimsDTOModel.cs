using DataModel.BaseModels;
using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Holds an ApplicationClaim
    /// <para>List<ApplicationClaim> Items</para>
    /// </summary>
    public class ApplicationClaimsDTOModel : BaseModel
    {
        /// <summary>
        /// Holds a List of ApplicationClaim
        /// </summary>
        public List<ApplicationClaim> Items { get; set; }

    }
}
