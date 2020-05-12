using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DataModel.Shared;
using DataService.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Display.Authentication
{
    public class CustomLockoutStore : CustomUserStore, IUserLockoutStore<ApplicationUser>
    {

        #region Class Setup
        public CustomLockoutStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            if (user.LockoutEnd.HasValue)
            {
                DateTime dt = user.LockoutEnd.Value.DateTime;
                return Task.FromResult(new DateTimeOffset(dt));
            }
            return Task.FromResult(new DateTimeOffset(DateTime.MinValue));

        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            user.AccessFailedCount += 1;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            user.AccessFailedCount = 0;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEnd = lockoutEnd.DateTime;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.LockoutEnd);
        }
    }
}