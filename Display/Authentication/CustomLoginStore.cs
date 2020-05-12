using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using DataService.Services;
using DataModel.Shared;
using AutoMapper;

namespace Display.Authentication
{
    public class CustomLoginStore : CustomUserStore, IUserLoginStore<ApplicationUser>
    {
        #region Class Setup
        public CustomLoginStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
    }
}