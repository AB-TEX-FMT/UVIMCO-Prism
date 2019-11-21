using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Holds an ApplicationRoleClaimsDTOModel
    /// <para>string RoleID</para>
    /// <para>List<ApplicationRoleClaim> Items</para>
    /// </summary>
    public class ApplicationRoleClaimsDTOModel : BaseDTOModel
    {
        /// <summary>
        /// Holds the RoleID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// Holds a List of ApplicationClaim
        /// </summary>
        public List<ApplicationRoleClaim> Items { get; set; }

    }
}
