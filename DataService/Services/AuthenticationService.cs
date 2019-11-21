using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using DataRepository;
using DataModel.DTOModels;
using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        #region Class Setup
        private readonly IAuthenticationRepository _repository;
        HttpClient _client;
        private readonly ServiceOptions _options;

        public AuthenticationService(ILogger<AuthenticationService> logger, ServiceOptions options) : base(logger)
        {
            _client = new HttpClient();
            _options = options;
        }
        #endregion

        #region Users
        #region GetUsers
        /// <summary>
        /// Get a List of all Users
        /// <para>Returns ApplicationUserListDTOModel</para>
        /// </summary>
        /// <returns>ApplicationUserListDTOModel</returns>
        public ApplicationUserListDTOModel GetUsers()
        {
            try
            {
                return new ApplicationUserListDTOModel
                {
                    Items = _repository.GetUsers(),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationUserListDTOModel
                {
                    ErrorMessage =  e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region SaveUser
        /// <summary>
        /// Saves the User creating it if it doesn't exist and updating it if it does
        /// <para>ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="role">ApplicationRole</param>
        /// <returns>ApplicationUserDTOModel</returns>
        public ApplicationUserDTOModel SaveUser(ApplicationUser user)
        {
            try
            {
                return new ApplicationUserDTOModel
                {
                    Item = _repository.SaveUser(user),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationUserDTOModel
                {
                    ErrorMessage =  e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region DeleteUserByID
        /// <summary>
        /// Deletes the User specified by id
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// /// <param name="id">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        public ApplicationUserDTOModel DeleteUserByID(string id)
        {
            try
            {
                return new ApplicationUserDTOModel
                {
                    Item = _repository.DeleteUserByID(id)
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationUserDTOModel
                {
                    ErrorMessage =  e.Message,
                    HasError = true,
                    Item = new ApplicationUser()
                };
            }
        }
        #endregion

        #region FindUserByID
        /// <summary>
        /// Gets the User specified by id
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        public ApplicationUserDTOModel FindUserByID(string id)
        {
            try
            {
                return new ApplicationUserDTOModel
                {
                    Item = _repository.FindUserByID(id),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationUserDTOModel
                {
                    ErrorMessage =  e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region FindUserByUserName
        /// <summary>
        /// Gets the User specified by userName
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        public ApplicationUserDTOModel FindUserByUserName(string userName)
        {
            try
            {
                return new ApplicationUserDTOModel
                {
                    Item = _repository.FindUserByUserName(userName),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationUserDTOModel
                {
                    ErrorMessage =  e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region FindUserByEmail
        /// <summary>
        /// Gets the User specified by email
        /// <para>Returns ApplicationUserDTOModel</para>
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>ApplicationUserDTOModel</returns>
        public ApplicationUserDTOModel FindUserByEmail(string email)
        {
            try
            {
                return new ApplicationUserDTOModel
                {
                    Item = _repository.FindUserByEmail(email),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationUserDTOModel
                {
                    ErrorMessage =  e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region AddToRole
        /// <summary>
        /// Adds the User specified by userID to the Role Specified by name
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="roleName">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        public ApplicationRoleDTOModel AddToRole(string userID, string roleName)
        {
            try
            {
                return new ApplicationRoleDTOModel
                {
                    Item = _repository.AddToRole(userID, roleName),
                };
            }
            catch (Exception e)
            {
                return new ApplicationRoleDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region RemoveFromRole
        /// <summary>
        /// Remove the User specified by userID from the Role specified by roleName
        /// <para>Returns ApplicationRolesDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="roleName">string</param>
        /// <returns>ApplicationRolesDTOModel</returns>
        public ApplicationRolesDTOModel RemoveFromRole(string userID, string roleName)
        {
            try
            {
                return new ApplicationRolesDTOModel
                {
                    Items = _repository.RemoveFromRole(userID, roleName),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationRolesDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion
        #endregion

        #region Roles
        #region GetRoles
        /// <summary>
        /// Get a List of all Roles
        /// <para>Returns ApplicationRolesDTOModel</para>
        /// </summary>
        /// <returns>ApplicationRolesDTOModel</returns>
        public ApplicationRolesDTOModel GetRoles()
        {
            try
            {
                return new ApplicationRolesDTOModel
                {
                    Items = _repository.GetRoles(),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationRolesDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region GetRoles
        /// <summary>
        /// Get a List of all Roles for the specified userID
        /// <para>Returns ApplicationRolesDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <returns>ApplicationRolesDTOModel</returns>
        public ApplicationRolesDTOModel GetRoles(string userID)
        {
            try
            {
                return new ApplicationRolesDTOModel
                {
                    Items = _repository.GetRoles(userID),
                };
            }
            catch (Exception e)
            {
                return new ApplicationRolesDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region SaveRole
        /// <summary>
        /// Saves the role creating it if it doesn't exist and updating it if it does
        /// <para>ReturnsApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="role">ApplicationRole</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        public ApplicationRoleDTOModel SaveRole(ApplicationRole role)
        {
            try
            {
                return new ApplicationRoleDTOModel()
                {
                    Item = _repository.SaveRole(role),
                };
            }
            catch (Exception e)
            {
                return new ApplicationRoleDTOModel()
                {
                    HasError = true,
                    ErrorMessage = e.Message,
                };
            }
        }
        #endregion

        #region DeleteRoleByID
        /// <summary>
        /// Deletes the Role specified by id
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// /// <param name="id">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        public ApplicationRoleDTOModel DeleteRoleByID(string id)
        {
            try
            {
                return new ApplicationRoleDTOModel
                {
                    Item = _repository.DeleteRoleByID(id),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationRoleDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region FindRoleByID
        /// <summary>
        /// Gets the Role specified by id
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        public ApplicationRoleDTOModel FindRoleByID(string id)
        {
            try
            {
                return new ApplicationRoleDTOModel
                {
                    Item = _repository.FindRoleByID(id),
                };
            }
            catch (Exception e)
            {
                return new ApplicationRoleDTOModel
                {
                    ErrorMessage =  e.Message,
                    HasError = true,
                };
            };
        }
        #endregion

        #region FindRoleByRoleName
        /// <summary>
        /// Gets the Role specified by name
        /// <para>Returns ApplicationRoleDTOModel</para>
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>ApplicationRoleDTOModel</returns>
        public ApplicationRoleDTOModel FindRoleByRoleName(string roleName)
        { 
            try
            {
                return new ApplicationRoleDTOModel
                {
                Item = _repository.FindRoleByRoleName(roleName),
                };
            }
            catch (Exception e)
            {
                return new ApplicationRoleDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion
        #endregion

        #region Claim
        #region GetClaimsByUserID
        /// <summary>
        /// Get all the Claims for the User specified by userID
        /// <para>Returns ApplicationUserClaimsDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <returns>ApplicationUserClaimsDTOModel</returns>
        public ApplicationUserClaimsDTOModel GetClaimsByUserID(string userID)
        {
            try
            {
                return new ApplicationUserClaimsDTOModel
                {
                    Items = _repository.GetClaimsByUserID(userID),
                };
            }
            catch (Exception e)
            {
                return new ApplicationUserClaimsDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region GetClaimsByRoleID
        /// <summary>
        /// Get all the Claims for the Role specified by roleID
        /// <para>Returns ApplicationRoleClaimsDTOModel</para>
        /// </summary>
        /// <param name="roleID">string</param>
        /// <returns>List<ApplicationClaim></returns>
        public ApplicationRoleClaimsDTOModel GetClaimsByRoleID(string userID)
        {
            try
            {
                return new ApplicationRoleClaimsDTOModel
                {
                    Items = _repository.GetClaimsByRoleID(userID),
                };
            }
            catch (Exception e)
            {
                return new ApplicationRoleClaimsDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region AddUserClaims
        /// <summary>
        /// Adds all the Claims specified by claims to the specified userID
        /// <para>Returns ApplicationClaimsDTOModel</para>
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="claims">List<ApplicationClaim></param>
        /// <returns>List<ApplicationClaim></returns>
        public ApplicationClaimsDTOModel AddUserClaims(string userID, List<ApplicationClaim> claims)
        {
            try
            {
                return new ApplicationClaimsDTOModel
                {
                    Items = _repository.AddUserClaims(userID, claims),
                };
            }
            catch (Exception e)
            {
                return new ApplicationClaimsDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion

        #region RemoveUserClaims
        /// <summary>
        /// Remove all the Claims specified by claims for the specified userID
        /// <para>Returns ApplicationClaimsDTOModel</para>
        /// </summary>
        /// <param name="userID">string</param>
        /// <param name="claims">List<ApplicationClaim></param>
        /// <returns>ApplicationClaimsDTOModel</returns>
        public ApplicationClaimsDTOModel RemoveUserClaims(string userID, List<ApplicationClaim> claims)
        {
            try
            {
                return new ApplicationClaimsDTOModel
                {
                    Items = _repository.RemoveUserClaims(userID, claims),
                };
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
                return new ApplicationClaimsDTOModel
                {
                    ErrorMessage = e.Message,
                    HasError = true,
                };
            }
        }
        #endregion
        #endregion
    }
}
