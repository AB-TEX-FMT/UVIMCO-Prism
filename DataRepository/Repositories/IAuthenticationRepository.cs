using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IAuthenticationRepository
    {
        #region User
        #region GetUsers
        /// <summary>
        /// Get a List of all Users
        /// <para>Returns List<ApplicationUser></para>
        /// </summary>
        /// <returns>List<ApplicationUser></returns>
        List<ApplicationUser> GetUsers();
        #endregion

        #region SaveUser
        /// <summary>
        /// Saves the User creating it if it doesn't exist and updating it if it does
        /// <para>ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>ApplicationUser</returns>
        ApplicationUser SaveUser(ApplicationUser user);
        #endregion

        #region DeleteUserByID
        /// <summary>
        /// Deletes the User specified by id
        /// <para>Returns ApplicationUser</para>
        /// </summary>
        /// /// <param name="id">string</param>
        /// <returns>ApplicationUser</returns>
        ApplicationUser DeleteUserByID(string id);
        #endregion

        #region FindUserByID
        /// <summary>
        /// Gets the User specified by id
        /// <para>Returns ApplicationUser</para>
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ApplicationUser</returns>
        ApplicationUser FindUserByID(string id);
        #endregion

        #region FindUserByUserName
        /// <summary>
        /// Gets the User specified by userName
        /// <para>Returns ApplicationUser</para>
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>ApplicationUser</returns>
        ApplicationUser FindUserByUserName(string userName);
        #endregion

        #region FindUserByEmail
        /// <summary>
        /// Gets the User specified by email
        /// <para>Returns ApplicationUser</para>
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>ApplicationUser</returns>
        ApplicationUser FindUserByEmail(string email);
        #endregion

        #region AddToRole
        /// <summary>
        /// Adds the User specified by userID to the Role Specified by name
        /// <para>Returns ApplicationRole</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="roleName">ApplicationRole</param>
        /// <returns>ApplicationRole</returns>
        ApplicationRole AddToRole(string userID, string roleName);
        #endregion

        #region RemoveFromRole
        /// <summary>
        /// Remove the specified userID from the Role specified by roleName
        /// <para>Returns List<ApplicationRole></para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="roleName">string</param>
        /// <returns>List<ApplicationRole></returns>
        List<ApplicationRole> RemoveFromRole(string userID, string roleName);
        #endregion
        #endregion

        #region Roles
        #region GetRoles
        /// <summary>
        /// Get a List of all Roles
        /// <para>Returns List<ApplicationRole></para>
        /// </summary>
        /// <returns>List<ApplicationRole></returns>
        List<ApplicationRole> GetRoles();
        #endregion

        #region GetRoles
        /// <summary>
        /// Get a List of all Roles for the specified userID
        /// <para>Returns List<ApplicationRole></para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <returns>List<ApplicationRole></returns>
        List<ApplicationRole> GetRoles(string userID);
        #endregion

        #region SaveRole
        /// <summary>
        /// Saves the role creating it if it doesn't exist and updating it if it does
        /// <para>ApplicationRole</para>
        /// </summary>
        /// <param name="role">ApplicationRole</param>
        /// <returns>ApplicationRole</returns>
        ApplicationRole SaveRole(ApplicationRole role);
        #endregion

        #region DeleteRoleByID
        /// <summary>
        /// Deletes the Role specified by id
        /// <para>Returns ApplicationRole</para>
        /// </summary>
        /// /// <param name="id">string</param>
        /// <returns>ApplicationRole</returns>
        ApplicationRole DeleteRoleByID(string id);
        #endregion

        #region FindRoleByID
        /// <summary>
        /// Gets the Role specified by id
        /// <para>Returns ApplicationRole</para>
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ApplicationRole</returns>
        ApplicationRole FindRoleByID(string id);
        #endregion

        #region FindRoleByRoleName
        /// <summary>
        /// Gets the Role specified by name
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        ApplicationRole FindRoleByRoleName(string name);
        #endregion
        #endregion

        #region Claims
        #region GetClaimsByUserID
        /// <summary>
        /// Get all the Claims for the User specified by userID
        /// <para>Returns List<ApplicationUserClaim></para>
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List<ApplicationClaim></returns>
        List<ApplicationUserClaim> GetClaimsByUserID(string userID);
        #endregion

        #region GetClaimsByRoleID
        /// <summary>
        /// Get all the Claims for the Role specified by roleID
        /// <para>Returns List<ApplicationRoleClaim></para>
        /// </summary>
        /// <param name="roleID">string</param>
        /// <returns>List<List<ApplicationRoleClaim>></returns>
        List<ApplicationRoleClaim> GetClaimsByRoleID(string roleID);
        #endregion

        #region AddUserClaims
        /// <summary>
        /// Adds all the Claims specified by claims to the specified userID
        /// <para>Returns  List<ApplicationClaim></para>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="claims"></param>
        /// <returns>List<ApplicationClaim></returns>
        List<ApplicationClaim> AddUserClaims(string userID, List<ApplicationClaim> claims);
        #endregion

        #region RemoveUserClaims
        /// <summary>
        /// Remove all the Claims specified by claims for the specified userID
        /// <para>Returns List<ApplicationClaim></para>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="claims"></param>
        /// <returns>List<ApplicationClaim></returns>
        List<ApplicationClaim> RemoveUserClaims(string userID, List<ApplicationClaim> claims);
        #endregion
        #endregion
    }
}
