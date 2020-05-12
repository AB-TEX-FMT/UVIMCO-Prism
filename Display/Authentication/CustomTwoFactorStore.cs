using Microsoft.AspNetCore.Identity;
using System;
using DataModel.DTOModels;
using DataModel.Shared;
using DataService.Services;
using System.Threading.Tasks;
using AutoMapper;

namespace Display.Authentication
{
    public class CustomTwoFactorStore : CustomUserStore, IUserTwoFactorStore<ApplicationUser>
    {

        #region Class Setup
        public CustomTwoFactorStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUserDTOModel user)
        {
            return Task.FromResult(user.Item.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
            //user.TwoFactorEnabled = enabled;
            //_authService.(user);
            //return Task.FromResult(user.TwoFactorEnabled);
        }
    }
}