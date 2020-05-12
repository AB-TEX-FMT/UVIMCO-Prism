using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using DataModel.Shared;
using DataService.Services;
using AutoMapper;

namespace Display.Authentication
{
    public class CustomEmailStore : CustomUserStore, IUserEmailStore<ApplicationUser>
    {

        #region Class Setup
        public CustomEmailStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            if (email == null || email == "") throw new ArgumentNullException(nameof(email));
            var result = _authService.FindUserByEmail(email);
            return Task.FromResult(result.Item);
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            user.Email = email;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.Email);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.EmailConfirmed);
        }
    }
}