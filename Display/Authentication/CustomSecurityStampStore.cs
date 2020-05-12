using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DataModel.Shared;
using DataService.Services;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Display.Authentication
{
    public class CustomSecurityStampStore : CustomUserStore, IUserSecurityStampStore<ApplicationUser>
    {

        #region Class Setup
        public CustomSecurityStampStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        public Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.SecurityStamp);
        }
    }
}