using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using DataModel.Shared;
using DataService.Services;
using AutoMapper;

namespace Display.Authentication
{
    public class CustomPasswordStore : CustomUserStore, IUserPasswordStore<ApplicationUser>
    {

        #region Class Setup
        public CustomPasswordStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            if (user.PasswordHash != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user);
        }
    }
}