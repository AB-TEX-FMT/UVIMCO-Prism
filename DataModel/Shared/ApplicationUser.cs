using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Shared
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Holds the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Holds the LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Holds the Street Address
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Holds the City
        /// </summary>
        public string City {get; set; }


        /// <summary>
        /// Holds the State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Holds the Zip
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Determines whether the User is and Application User
        /// Application users can not be deleted
        /// </summary>
        public bool IsAppUser { get; set; }

        /// <summary>
        /// Determines whether the User has been deleted
        /// </summary>
        public bool DeletedUser { get; set; }

        /// <summary>
        /// Holds a list of the User's Application Roles
        /// </summary>
        public List<ApplicationRole> ApplicationRoles { get; set; }

        /// <summary>
        /// Holds a list of the User's Application Claims
        /// </summary>
        public List<ApplicationClaim> ApplicationClaims { get; set; }
    }
}
