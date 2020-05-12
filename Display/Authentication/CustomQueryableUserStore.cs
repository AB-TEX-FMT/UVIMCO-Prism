using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DataModel.Shared;
using DataService.Services;
using System;
using System.Linq;

namespace Display.Authentication
{

    public class CustomQueryableUserStore : CustomUserStore, IQueryableUserStore<ApplicationUser>
    {

        #region Class Setup
        public CustomQueryableUserStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        IQueryable<ApplicationUser> IQueryableUserStore<ApplicationUser>.Users => throw new NotImplementedException();
    }
}