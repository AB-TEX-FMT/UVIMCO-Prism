using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Holds an ApplicationUserClaimsDTOModel
    /// <para>string UserID</para>
    /// <para>List<ApplicationUserClaim> Items</para>
    /// </summary>
    public class ApplicationUserClaimsDTOModel : BaseDTOModel
    {
        /// <summary>
        /// Holds the UserID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Holds a List of ApplicationClaim
        /// </summary>
        public List<ApplicationUserClaim> Items { get; set; }

    }
}
