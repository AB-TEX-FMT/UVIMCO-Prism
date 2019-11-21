using Microsoft.AspNetCore.Identity;
using DataModel.DTOModels;
using DataModel.Shared;
using System.Collections.Generic;

namespace DataService.Services
{
    //[ServiceContract]
    public interface IAuthenticationService
    {
        #region User
        /// <summary>
        /// Get a List of all Users
        /// <para>Returns ApplicationUserListDTOModel</para>
        /// </summary>
        /// <returns>ApplicationUserListDTOModel</returns>
        ApplicationUserListDTOModel GetUsers();

        /// <summary>
        /// Saves the User creating it if it doesn't exist and updating it if it does
        /// <para>ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="user">ApplicationRole</param>
        /// <returns>ApplicationUserDTOModel</returns>
        ApplicationUserDTOModel SaveUser(ApplicationUser user);

        /// <summary>
        /// Deletes the User specified by id
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// /// <param name="id">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        ApplicationUserDTOModel DeleteUserByID(string id);

        /// <summary>
        /// Gets the User specified by id
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        ApplicationUserDTOModel FindUserByID(string id);

        /// <summary>
        /// Gets the User specified by userName
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        ApplicationUserDTOModel FindUserByUserName(string userName);

        /// <summary>
        /// Gets the User specified by email
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        ApplicationUserDTOModel FindUserByEmail(string email);

        /// <summary>
        /// Adds the User specified by userID to the Role Specified by name
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="roleName">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        ApplicationRoleDTOModel AddToRole(string userID, string roleName);

        /// <summary>
        /// Remove the User specified by userID from the Role specified by roleName
        /// <para>Returns ApplicationRolesDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="roleName">string</param>
        /// <returns>ApplicationRolesDTOModel</returns>
        ApplicationRolesDTOModel RemoveFromRole(string userID, string roleName);
        #endregion

        #region Role
        #region GetRoles
        /// <summary>
        /// Get a List of all Roles
        /// <para>Returns ApplicationRolesDTOModel</para>
        /// </summary>
        /// <returns>ApplicationRolesDTOModel</returns>
        ApplicationRolesDTOModel GetRoles();
        #endregion

        #region GetRoles
        /// <summary>
        /// Get a List of all Roles for the specified userID
        /// <para>Returns ApplicationRolesDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <returns>ApplicationRolesDTOModel</returns>
        ApplicationRolesDTOModel GetRoles(string userID);
        #endregion

        #region SaveRole
        /// <summary>
        /// Saves the role creating it if it doesn't exist and updating it if it does
        /// <para>ReturnsApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="role">ApplicationRole</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        ApplicationRoleDTOModel SaveRole(ApplicationRole role);
        #endregion

        #region DeleteRoleByID
        /// <summary>
        /// Deletes the Role specified by id
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// /// <param name="id">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        ApplicationRoleDTOModel DeleteRoleByID(string id);
        #endregion

        #region FindRoleByID
        /// <summary>
        /// Gets the Role specified by id
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        ApplicationRoleDTOModel FindRoleByID(string id);
        #endregion

        #region FindRoleByRoleName
        /// <summary>
        /// Gets the Role specified by name
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        ApplicationRoleDTOModel FindRoleByRoleName(string name);
        #endregion
        #endregion

        #region Claims
        #region GetClaimsByUserID
        /// <summary>
        /// Get all the Claims for the User specified by userID
        /// <para>Returns ApplicationUserClaimsDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <returns>ApplicationUserClaimsDTOModel</returns>
        ApplicationUserClaimsDTOModel GetClaimsByUserID(string userID);
        #endregion

        #region GetClaimsByRoleID
        /// <summary>
        /// Get all the Claims for the Role specified by roleID
        /// <para>Returns ApplicationRoleClaimsDTOModel</para>
        /// </summary>
        /// <param name="roleID">string</param>
        /// <returns>List<ApplicationClaim></returns>
        ApplicationRoleClaimsDTOModel GetClaimsByRoleID(string roleID);
        #endregion

        #region AddUserClaims
        /// <summary>
        /// Adds all the Claims specified by claims to the specified userID
        /// <para>Returns ApplicationClaimsDTOModel</para>
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="claims">List<ApplicationClaim></param>
        /// <returns>List<ApplicationClaim></returns>
        ApplicationClaimsDTOModel AddUserClaims(string userID, List<ApplicationClaim> claims);
        #endregion

        #region RemoveUserClaims
        /// <summary>
        /// Remove all the Claims specified by claims for the specified userID
        /// <para>Returns ApplicationClaimsDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="claims">List<ApplicationClaim></param>
        /// <returns>ApplicationClaimsDTOModel</returns>
        ApplicationClaimsDTOModel RemoveUserClaims(string userID, List<ApplicationClaim> claims);
        #endregion 
        #endregion
    }
}
