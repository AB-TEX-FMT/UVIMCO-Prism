using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DataModel.DTOModels;
using DataModel.Shared;
using DataService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Display.Authentication
{
    public class CustomUserStore : IUserStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IUserPhoneNumberStore<ApplicationUser>,
        IUserLoginStore<ApplicationUser>,
        IUserLockoutStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserClaimStore<ApplicationUser>,
        IQueryableUserStore<ApplicationUser>
    {
        #region Class Setup
        protected readonly IAuthenticationService _authService;
        protected readonly IMapper _mapper;

        public CustomUserStore(IAuthenticationService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        #endregion

        #region IUserStore
        //Returns a List of all Users
        public IQueryable<ApplicationUser> Users => GetUsers();

        public IQueryable<ApplicationUser> GetUsers()
        {
            var result = _authService.GetUsers();
            if (result.Items == null)
            {
                return new List<ApplicationUser>().AsQueryable<ApplicationUser>();
            }
            return result.Items.AsQueryable<ApplicationUser>();
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            var result = _authService.SaveUser(user);
            if (result.HasError)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage }));
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }

        public Task Create(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            // Get the result from the service
            var result = _authService.SaveUser(user);
            if (result.HasError)
            {
                // There was a problem so throw an error
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage }));
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }

        /// <summary>
        /// Deletes the User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            // Get the result from the service
            var result = _authService.DeleteUserByID(user.Id);
            if (result.HasError)
            {
                // There was a problem so throw an error
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage }));
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Get the User specified by the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            // check for a valid GUID
            if (!Guid.TryParse(userId, out Guid idGuid))
            {
                throw new ArgumentException("Not a valid Guid", nameof(userId));
            }
            // Get the result from the service
            var result = _authService.FindUserByID(userId);
            if (result.Item == null)
            {
                // There was a problem so throw an error
                return Task.FromResult<ApplicationUser>(null);
            }
            return Task.FromResult(result.Item);
        }

        /// <summary>
        /// Gets an ApplicationUser with the specified userName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ApplicationUser> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            // Get the result from the service
            var result = _authService.FindUserByUserName(userName);
            if (result.Item == null)
            {
                // There was a problem so throw an error
                return Task.FromResult<ApplicationUser>(null);
            }
            return Task.FromResult(result.Item);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Id);
        }

        /// <summary>
        /// Gets the UserName for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.UserName);
        }

        /// <summary>
        /// Sets the the Users NormalizedUserName to the value of normalizedName
        /// </summary>
        /// <param name="user"></param>
        /// <param name="normalizedName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));
            // set the data
            user.NormalizedUserName = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            // Get the result from the service
            var result = _authService.SaveUser(user);
            if (result.HasError)
            {
                // There was a problem so throw an error
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage }));
            }
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }
        #endregion

        #region ITwoFactorStore
        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(user.TwoFactorEnabled);
        }
        #endregion

        #region ISecurityStampStore
        public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (roleName == "")
            {
                throw new ArgumentNullException("roleName");
            }
            var result = _authService.AddToRole(user.Id, roleName);
            user.ApplicationRoles.Add(result.Item);
            return Task.FromResult(result.Item);
        }

        /// <summary>
        /// Remove the specified user from the specified role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            ApplicationRolesDTOModel result = _authService.RemoveFromRole(user.Id, roleName);
            if (result.HasError)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage }));
            }
            user.ApplicationRoles = result.Items;
            return Task.FromResult(result.Items);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //Initialize the User Application roles if neccessary
            if (user.ApplicationRoles == null)
            {
                user.ApplicationRoles = new List<ApplicationRole>();
            }
            //See if the roles have been added
            if (user.ApplicationRoles.Count == 0)
            {
                var result = _authService.GetRoles(user.Id);
                if (!result.HasError && result.Items != null)
                {
                    foreach (ApplicationRole role in result.Items)
                    {
                        user.ApplicationRoles.Add(role);
                    }
                }
            }
            IList<string> roles = new List<string>();
            foreach (ApplicationRole role in user.ApplicationRoles)
            {
                roles.Add(role.Name.ToUpper());
            }
            return Task.FromResult(roles);
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //Check for a null value
            if (user.ApplicationRoles == null)
            {
                var roles = await GetRolesAsync(user, cancellationToken);
                return roles.Contains(roleName);
            }
            return user.ApplicationRoles.Exists(x => x.NormalizedName == roleName);
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IPasswordStore
        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user.PasswordHash != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(user);
        }
        #endregion

        #region IPhoneNumberStore
        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(user.PhoneNumber);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(user.PhoneNumberConfirmed);
        }
        #endregion

        #region IUserLoginStore

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(ApplicationUser user, string login, string test, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ILockoutStore
        public Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user.LockoutEnd.HasValue)
            {
                return Task.FromResult(new DateTimeOffset?(user.LockoutEnd.Value));
            }
            return Task.FromResult(new DateTimeOffset?(DateTime.MinValue));

        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount += 1;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd.Value.DateTime;
            return Task.FromResult(user.LockoutEnd);
        }


        #endregion

        #region IUserEmailStore
        public Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (email == null || email == "") throw new ArgumentNullException(nameof(email));
            // Get the result from the service
            var result = _authService.FindUserByEmail(email);
            if (result.HasError && result.Item == null)
            {
                // There was a problem so throw an error
                return Task.FromResult<ApplicationUser>(null);
            }
            return Task.FromResult(result.Item);
        }

        /// <summary>
        /// Gets the Email for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Gets the NormalizedEmail value for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<string></returns>
        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.NormalizedEmail);
        }

        /// <summary>
        /// Sets the the Users NormalizedEmail to the value of normalizedEmail
        /// </summary>
        /// <param name="user"></param>
        /// <param name="normalizedEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedEmail == null) throw new ArgumentNullException(nameof(normalizedEmail));
            // set the data
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Gets the EmailConfirmed value for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<string></returns>
        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// Sets the the Users Email to the value of email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Email = email;
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Sets the the Users EmailConfirmed to the value of confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.EmailConfirmed = confirmed;
            return Task.FromResult(user.EmailConfirmed);
        }
        #endregion

        #region IUserClaimStore
        public Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claims == null)
            {
                throw new ArgumentNullException("claim");
            }
            var inputItems = _mapper.Map<List<System.Security.Claims.Claim>, List<ApplicationClaim>>(claims.ToList(), new List<ApplicationClaim>());
            // Get the result from the service
            var result = _authService.AddUserClaims(user.Id, inputItems);
            // If there is no error add the claims to the User's claims list
            if (!result.HasError) { user.ApplicationClaims.AddRange(result.Items); }
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //See if the claims have been added
            if (user.ApplicationClaims == null || !user.ApplicationClaims.Any())
            {
                //Get the data from the service
                var result = _authService.GetClaimsByUserID(user.Id);

                if (!result.HasError) { user.ApplicationClaims = new List<ApplicationClaim>(result.Items); }
            }
            IList <Claim> claims = _mapper.Map<List<ApplicationClaim>, List<Claim>>(user.ApplicationClaims, new List<Claim>());
            return Task.FromResult(claims);
        }

        public Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove all the Claims specified by claims for the specified userID
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="claims">IEnumerable<Claim></param>
        /// <returns>Task</returns>
        public Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // See if we have a null input and throw error
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claims == null)
            {
                throw new ArgumentNullException("claim");
            }
            var inputItems = _mapper.Map<List<System.Security.Claims.Claim>, List<ApplicationClaim>>(claims.ToList(), new List<ApplicationClaim>());
            // Get the result from the service
            var result = _authService.RemoveUserClaims(user.Id, inputItems);
            // If there is no error add the claims to the User's claims list
            if (!result.HasError) { user.ApplicationClaims.AddRange(result.Items); }
            return Task.FromResult(0);
        }

        public Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }
        #endregion


    }
}