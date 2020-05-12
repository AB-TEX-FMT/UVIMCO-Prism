using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using DataModel.Shared;
using DataService.Services;
using DataModel.DTOModels;
using System.Linq;
using System.Collections.Generic;

namespace Display.Authentication
{
    public class CustomRoleStore : IRoleStore<ApplicationRole>, IQueryableRoleStore<ApplicationRole>
    {
        #region Class Setup
        readonly IAuthenticationService _authService;

        public CustomRoleStore(IAuthenticationService userService)
        {
            _authService = userService;
        }
        #endregion

        #region GetRoles
        /// <summary>
        /// Gets a Iqueryable List of Roles
        /// </summary>
        /// <returns></returns>
        //public IQueryable<ApplicationRole> Roles => throw new NotImplementedException();
        //=> throw new NotImplementedException();
        IQueryable<ApplicationRole> IQueryableRoleStore<ApplicationRole>.Roles => Roles();
        public IQueryable<ApplicationRole> Roles()
        {
            try
            {
                return _authService.GetRoles().Items.AsQueryable<ApplicationRole>();
            }
            catch
            {
                return new List<ApplicationRole>().AsQueryable<ApplicationRole>();
            }
        }
        #endregion


        /// <summary>
        /// Creates a newe Role in the data store
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task CreateAsync(ApplicationRole role)
        {
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            _authService.SaveRole(role);

            return Task.FromResult<IdentityResult>(null);
        }

        /// <summary>
        /// Creates a newe Role in the data store
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            var result = _authService.SaveRole(role);
            if (result.HasError)
            {
                return Task.FromResult<IdentityResult>(null);
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }

        /// <summary>
        /// Deletes the Role specified by role
        /// <para>Retuns Task</para>
        /// </summary>
        /// <param name="role">ApplicationRole</param>
        /// <returns>Task</returns>
        public Task DeleteAsync(ApplicationRole role)
        {

            if (role == null) throw new ArgumentNullException(nameof(role));
            var result = _authService.DeleteRoleByID(role.Id);
            if (result.HasError)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new List<IdentityError>() { new IdentityError() { Description = result.ErrorMessage } }.ToArray()));
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            var result = _authService.DeleteRoleByID(role.Id);
            if (result.HasError)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new List<IdentityError>() { new IdentityError() { Description = result.ErrorMessage } }.ToArray()));
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }

        public void Dispose()
        {
        }

        public Task<ApplicationRole> FindByIdAsync(string roleId)
        {
            if (roleId == null) throw new ArgumentNullException(nameof(roleId));
            if (!Guid.TryParse(roleId, out _))
            {
                throw new ArgumentException("Not a valid Guid id", nameof(roleId));
            }
            var result = _authService.FindRoleByID(roleId);
            return Task.FromResult<ApplicationRole>(result.Item);
        }

        /// <summary>
        /// Get the Role specified by the roleId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (roleId == null) throw new ArgumentNullException(nameof(roleId));
            // check for a valid GUID
            if (!Guid.TryParse(roleId, out _))
            {
                throw new ArgumentException("Not a valid Guid", nameof(roleId));
            }
            // Get the result from the service
            var result = _authService.FindRoleByID(roleId);
            if (result.Item == null)
            {
                // There was a problem so throw an error
                return Task.FromResult<ApplicationRole>(null);
            }
            return Task.FromResult(result.Item);
        }

        /// <summary>
        /// Gets an ApplicationRole with the specified normalizedRoleName
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName)
        {
            if (normalizedRoleName == null) throw new ArgumentNullException(nameof(normalizedRoleName));

            var result = _authService.FindRoleByRoleName(normalizedRoleName);
            return Task.FromResult<ApplicationRole>(result.Item);
        }

        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (normalizedRoleName == null) throw new ArgumentNullException(nameof(normalizedRoleName));
            // Get the result from the service
            var result = _authService.FindRoleByRoleName(normalizedRoleName);
            if (result.Item == null)
            {
                // There was a problem so throw an error
                return Task.FromResult<ApplicationRole>(null);
            }
            return Task.FromResult(result.Item);
        }

        /// <summary>
        /// Gets the NormalizedName value for the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role)
        {
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult<string>(role.NormalizedName);
        }

        /// <summary>
        /// Gets the NormalizedName value for the role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.NormalizedName);
        }

        /// <summary>
        /// Gets the ID value for the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetRoleIdAsync(ApplicationRole role)
        {
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Id.ToString());
        }

        /// <summary>
        /// Gets the ID value for the role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Id.ToString());
        }

        /// <summary>
        /// Gets the Name for the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetRoleNameAsync(ApplicationRole role)
        {
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult<string>(role.Name);
        }

        /// <summary>
        /// Gets the Name for the role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// Sets the the Role's NormalizedName to the value of normalizedName
        /// </summary>
        /// <param name="role"></param>
        /// <param name="normalizedName"></param>
        /// <returns></returns>
        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName)
        {
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            // set the data
            role.NormalizedName = normalizedName ?? throw new ArgumentNullException(nameof(normalizedName));
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Sets the the Role's NormalizedName to the value of normalizedName
        /// </summary>
        /// <param name="role"></param>
        /// <param name="normalizedName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            // set the data
            role.NormalizedName = normalizedName ?? throw new ArgumentNullException(nameof(normalizedName));
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Sets the the Role's Name to the value of roleName
        /// </summary>
        /// <param name="role"></param>
        /// <param name="normalizedName"></param>
        /// <returns></returns>
        public Task SetRoleNameAsync(ApplicationRole role, string roleName)
        {
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            // set the data
            role.Name = roleName ?? throw new ArgumentNullException(nameof(roleName));
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Sets the the Role's Name to the value of roleName
        /// </summary>
        /// <param name="role"></param>
        /// <param name="normalizedName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            // set the data
            role.Name = roleName ?? throw new ArgumentNullException(nameof(roleName));
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Persist the changes to the data store
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task UpdateAsync(ApplicationRole role)
        {
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            // perform the action
            var result = _authService.SaveRole(role);
            if (result.HasError)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new List<IdentityError>() { new IdentityError() { Description = result.ErrorMessage } }.ToArray()));
            }
            return Task.FromResult<IdentityResult>(null);
        }

        /// <summary>
        /// Persist the changes to the data store
        /// </summary>
        /// <param name="role"></param>
        /// /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (role == null) throw new ArgumentNullException(nameof(role));
            // perform the action
            ApplicationRoleDTOModel result = _authService.SaveRole(role);
            if (result.HasError)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new List<IdentityError>() { new IdentityError() { Description = result.ErrorMessage } }.ToArray()));
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }
    }
}