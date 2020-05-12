using Microsoft.AspNetCore.Authorization;

namespace Display
{
    public class Policies
    {
        #region Acces Related Policies
        /// <summary>
        /// Sets the policy for Super Administrator
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool SuperAdminPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for Administrator and Above
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool AdminPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("ADMIN") ||
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for Sub Administrator and Above
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool SubAdminPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("SUB ADMIN") || 
                context.User.IsInRole("ADMIN") ||
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for User
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool UserPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("USER");
        }
        #endregion

        #region Role Policies
        /// <summary>
        /// Sets the policy for creating roles
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool ManageRolesPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("ADMIN") &&
                context.User.HasClaim(claim => claim.Type == "View Roles" && claim.Value == "true") || 
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for creating roles
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool CreateRolePolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for editing roles
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool EditRolePolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("ADMIN") &&
                context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for deleting roles
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool DeleteRolePolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("SUPER ADMIN");
        }
        #endregion

        #region User Policies
        /// <summary>
        /// Sets the policy for creating Users
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool ManageUsersPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("SUB ADMIN") ||
                context.User.IsInRole("ADMIN") ||
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for creating Users
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool CreateUserPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("ADMIN") ||
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for editing Users
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool EditUserPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("ADMIN") &&
                context.User.HasClaim(claim => claim.Type == "Edit User" && claim.Value == "true") ||
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for deleting Users
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool DeleteUserPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("SUB ADMIN") &&
                context.User.HasClaim(claim => claim.Type == "Delete User" && claim.Value == "true") || 
                context.User.IsInRole("ADMIN") ||
                context.User.IsInRole("SUPER ADMIN");
        }

        /// <summary>
        /// Sets the policy for managing a Users roles
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool ManageUserRolesPolicyAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("ADMIN") &&
                context.User.HasClaim(claim => claim.Type == "Manage User Roles" && claim.Value == "true") ||
                context.User.IsInRole("SUPER ADMIN");
        }
        #endregion
    }
}
