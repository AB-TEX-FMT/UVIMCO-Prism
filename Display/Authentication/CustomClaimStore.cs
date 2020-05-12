using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using DataModel.Shared;
using DataService.Services;
using AutoMapper;
using System.Linq;
using System.Threading;

namespace Display.Authentication
{
    public class CustomClaimStore : CustomUserStore, IUserClaimStore<ApplicationUser>
    {

        #region Class Setup
        public CustomClaimStore(IAuthenticationService authService, IMapper mapper) : base(authService, mapper)
        {
        }
        #endregion

        #region Claims
        #region GetAllClaims
        /// <summary>
        /// Gets a Iqueryable List of Claims
        /// </summary>
        /// <returns></returns>
        public static List<ApplicationClaim> AllClaims()
        {
            try
            {
                return new List<ApplicationClaim>()
                {
                    new ApplicationClaim() { ClaimType = "View Roles", ClaimValue = "View Role" },
                    new ApplicationClaim() { ClaimType = "Edit Role", ClaimValue = "Edit Role" },
                    new ApplicationClaim() { ClaimType = "Edit User", ClaimValue = "Edit User" },
                    new ApplicationClaim() { ClaimType = "Delete User", ClaimValue = "Delete User" },
                    new ApplicationClaim() { ClaimType = "Manager User Roles", ClaimValue = "Edit Role" },

                };
            }
            catch
            {
                return new List<ApplicationClaim>();
            }
        }
        #endregion
        #endregion

        //#region UserClaims
        //public Task AddClaimAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    var result = _authService.GetClaims(user.Id);
        //    IList<Claim> claims = new List<Claim>();
        //    foreach (ApplicationClaim claim in result.Items)
        //    {
        //        claims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
        //    }
        //    return Task.FromResult(claims);
        //}

        //public Task RemoveClaimAsync(ApplicationUser user, Claim claim, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion
    }
}