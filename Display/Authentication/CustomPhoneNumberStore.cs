using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using DataModel.Shared;
using DataService.Services;
using AutoMapper;

namespace Display.Authentication
{
    public class CustomPhoneNumberStore : CustomUserStore, IUserPhoneNumberStore<ApplicationUser>
    {
        public CustomPhoneNumberStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.PhoneNumber);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;
            UpdateAsync(user, new CancellationToken());
            return Task.FromResult(user.PhoneNumberConfirmed);
        }
    }
}