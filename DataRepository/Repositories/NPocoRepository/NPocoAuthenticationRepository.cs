//using AutoMapper;
//using NPoco;
//using Microsoft.Extensions.Logging;
//using DataRepository.Factories;
//using DataRepository;
//using DataRepository.Repositories;
//using DataModel.Shared;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using static DataRepository.Factories.DbFactory;
//using System.Reflection;
//using System.Linq;

//namespace DataRepository.NPocoRepository
//{
//    public class NPocoAuthenticationRepository : BaseRepository, IAuthenticationRepository
//    {
//        #region Class Setup
//        private readonly IDBFactory _dbFactory;
//        private readonly AutoMapper.IMapper _mapper;

//        public NPocoAuthenticationRepository(ILogger<NPocoAuthenticationRepository> logger, IDBFactory dbFactory, AutoMapper.IMapper mapper) : base(logger)
//        {
//            _dbFactory = dbFactory;
//            _mapper = mapper;
//        }

//        private IDatabase Conn()
//        {
//            return _dbFactory.GetConnection(AvailableConnections.Auth);
//        }
//        #endregion

//        #region User
//        #region GetUsers
//        /// <summary>
//        /// Get a List of all Users
//        /// <para>Returns List<ApplicationUser></para>
//        /// </summary>
//        /// <returns>List<ApplicationUser></returns>
//        public List<ApplicationUser> GetUsers()
//        {
//            List<ApplicationUser> result;
//            using (var db = Conn())
//            {
//                try
//                {
//                    result = db.Fetch<ApplicationUser>("select * from dbo.AspNetUsers " );
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//            return result;
//        }
//        #endregion

//        #region SaveUser
//        /// <summary>
//        /// Saves the User creating it if it doesn't exist and updating it if it does
//        /// <para>ApplicationUserDTOModel</para>
//        /// </summary>
//        /// <param name="user">ApplicationUser</param>
//        /// <returns>ApplicationUser</returns>
//        public ApplicationUser SaveUser(ApplicationUser user)
//        {
//            using (var db = Conn())
//            {
//                try
//                {
//                    // Save the user
//                    db.Save(_mapper.Map<Model.Shared.ApplicationUser, DataRepository.Models.ApplicationUser>(user, new DataRepository.Models.ApplicationUser()));
//                    // Check to see if the User has the role 'User', if not create it
//                    List<ApplicationRole> result = GetRoles(user.Id);
//                    // User isn't assigned any roles yet so new user
//                    // assign them to the 'User' role
//                    if (!result.Any())
//                    {
//                        AddToRole(user.Id, "User");
//                    }
//                    return user;
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region DeleteUserByID
//        /// <summary>
//        /// Deletes the User specified by id
//        /// <para>Returns ApplicationUser</para>
//        /// </summary>
//        /// /// <param name="id">string</param>
//        /// <returns>ApplicationUser</returns>
//        public ApplicationUser DeleteUserByID(string id)
//        {
//            using (var db = Conn())
//            {
//                try
//                {
//                    DataRepository.Models.ApplicationUser user = _mapper.Map<Model.Shared.ApplicationUser, DataRepository.Models.ApplicationUser>(FindUserByID(id), new DataRepository.Models.ApplicationUser());
//                    if (user.IsAppUser)
//                    {
//                        LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + "Attempt to delete application User");
//                        throw new Exception("This User can not be deleted");
//                    }
//                    user.DeletedUser = true;
//                    db.Save(user);
//                    return user;
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region FindUserByID
//        /// <summary>
//        /// Gets the User specified by id
//        /// <para>Returns ApplicationUser</para>
//        /// </summary>
//        /// <param name="id">string</param>
//        /// <returns>ApplicationUser</returns>
//        public ApplicationUser FindUserByID(string id)
//        {
//            ApplicationUserDetail resultUser;
//            List<Safe> resultSafe;
//            NPoco.IDatabase db;
//            //Retrieve the UserDetails from the AuthDB
//            using (db = Conn())
//            {
//                try
//                {
//                    resultUser = db.Single<ApplicationUserDetail>("select * from dbo.AspNetUsers WHERE Id = @ID", new { ID = id });
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//                // Now grab the safe from the AppDB
//                //using (db = _dbFactory.GetConnection(AvailableConnections.App))
//                //{
//                //    try
//                //    {
//                //        resultSafe = db.Fetch<Safe>("select * from dbo.Safe WHERE UserID = @ID", new { ID = resultUser.Id });
//                //        if (resultSafe != null)
//                //        {
//                //            // Add the safes to the User
//                //            resultUser.Safes = resultSafe;
//                //        }
//                //        else
//                //        {
//                //            // We didn't find any Safes for the User so just initialize the collection
//                //            resultUser.Safes = new List<Safe>();
//                //        }
//                //    }
//                //    catch (Exception e)
//                //    {
//                //        LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                //        throw new Exception(e.Message);
//                //    }
//                //}
//                resultUser.Safes = new List<Safe>();
//            }
//            return resultUser;
//        }
//        #endregion 

//        #region FindUserByUserName
//        /// <summary>
//        /// Gets the User specified by userName
//        /// <para>Returns ApplicationUser</para>
//        /// </summary>
//        /// <param name="userName">string</param>
//        /// <returns>ApplicationUser</returns>
//        public ApplicationUser FindUserByUserName(string userName)
//        {
//            ApplicationUserDetail resultUser;
//            List<Safe> resultSafe;
//            NPoco.IDatabase db;
//            //Retrieve the UserDetails from the AuthDB
//            using (db = Conn())
//            {
//                try
//                {
//                    resultUser = db.Single<ApplicationUserDetail>("select * from dbo.AspNetUsers WHERE UserName = @UserName", new { UserName = userName });
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//                // Now grab the safe from the AppDB
//                //using (db = _dbFactory.GetConnection(AvailableConnections.App))
//                //{
//                //    try
//                //    {
//                //        resultSafe = db.Fetch<Safe>("select * from dbo.Safe WHERE UserID = @ID", new { ID = resultUser.Id });
//                //        if (resultSafe != null)
//                //        {
//                //            // Add the safes to the User
//                //            resultUser.Safes = resultSafe;
//                //        }
//                //        else
//                //        {
//                //            // We didn't find any Safes for the User so just initialize the collection
//                //            resultUser.Safes = new List<Safe>();
//                //        }
//                //    }
//                //    catch (Exception e)
//                //    {
//                //        LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                //        throw new Exception(e.Message);
//                //    }
//                //}
//                resultUser.Safes = new List<Safe>();
//            }
//            return resultUser;
//        }
//        #endregion 

//        #region FindUserByEmail
//        /// <summary>
//        /// Gets the User specified by email
//        /// <para>Returns ApplicationUser</para>
//        /// </summary>
//        /// <param name="email">string</param>
//        /// <returns>ApplicationUser</returns>
//        public ApplicationUser FindUserByEmail(string email)
//        {
//            ApplicationUserDetail resultUser;
//            List<Safe> resultSafe;
//            NPoco.IDatabase db;
//            //Retrieve the UserDetails from the AuthDB
//            using (db = Conn())
//            {
//                try
//                {
//                    resultUser = db.Single<ApplicationUserDetail>("select * from dbo.AspNetUsers WHERE Email = @Email", new { Email = email });
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//                // Now grab the safe from the AppDB
//                //using (db = _dbFactory.GetConnection(AvailableConnections.App))
//                //{
//                //    try
//                //    {
//                //        resultSafe = db.Fetch<Safe>("select * from dbo.Safe WHERE UserID = @ID", new { ID = resultUser.Id.ToString() });
//                //        if (resultSafe != null)
//                //        {
//                //            // Add the safes to the User
//                //            resultUser.Safes = resultSafe;
//                //        }
//                //        else
//                //        {
//                //            // We didn't find any Safes for the User so just initialize the collection
//                //            resultUser.Safes = new List<Safe>();
//                //        }
//                //    }
//                //    catch (Exception e)
//                //    {
//                //        LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                //        throw new Exception(e.Message);
//                //    }
//                //}
//                resultUser.Safes = new List<Safe>();
//            }
//            return resultUser;
//        }
//        #endregion

//        #region AddToRole
//        /// <summary>
//        /// Adds the User specified by userID to the Role Specified by name
//        /// <para>Returns ApplicationRole</para>
//        /// </summary>
//        /// <param name="userID">string</param>
//        /// <param name="roleName">ApplicationRole</param>
//        /// <returns>ApplicationRole</returns>
//        public ApplicationRole AddToRole(string userID, string roleName)
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    ApplicationRole role = FindRoleByRoleName(roleName);
//                    db.Save(new DataRepository.Models.ApplicationUserRole() { RoleId = role.Id, UserId = userID });
//                    return FindRoleByID(role.Id);
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region RemoveFromRole
//        /// <summary>
//        /// Remove the specified userID from the Role specified by roleName
//        /// <para>Returns List<ApplicationRole></para>
//        /// </summary>
//        /// <param name="userID"></param>
//        /// <param name="roleName"></param>
//        /// <returns>List<ApplicationRole></returns>
//        public List<ApplicationRole> RemoveFromRole(string userID, string roleName)
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    ApplicationRole role = FindRoleByRoleName(roleName);
//                    db.Delete(new DataRepository.Models.ApplicationUserRole() { RoleId = role.Id, UserId = userID });
//                    return db.Fetch<ApplicationRole>("SELECT anr.* FROM dbo.AspNetUserRoles anur Left Join dbo.AspNetRoles anr ON anr.Id = anur.RoleId WHERE anur.UserID = @UserID", new { UserID = userID });
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion
//        #endregion

//        #region Role
//        #region GetRoles
//        /// <summary>
//        /// Get a List of all Roles
//        /// <para>Returns List<ApplicationRole></para>
//        /// </summary>
//        /// <returns>List<ApplicationRole></returns>
//        public List<ApplicationRole> GetRoles()
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    return db.Fetch<ApplicationRole>("SELECT * FROM AspNetRoles");
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region GetRoles
//        /// <summary>
//        /// Get a List of all Roles for the specified userID
//        /// <para>Returns List<ApplicationRole></para>
//        /// </summary>
//        /// <param name="userID">string</param>
//        /// <returns>List<ApplicationRole></returns>
//        public List<ApplicationRole> GetRoles(string userID)
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    return db.Fetch<ApplicationRole>("SELECT anr.* FROM AspNetRoles anr LEFT JOIN AspNetUserRoles anur on anr.Id = anur.RoleId WHERE anur.UserId = @UserID", new { UserID = userID });
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region SaveRole
//        /// <summary>
//        /// Saves the role creating it if it doesn't exist and updating it if it does
//        /// <para>ApplicationRole</para>
//        /// </summary>
//        /// <param name="role">ApplicationRole</param>
//        /// <returns>ApplicationRole</returns>
//        public ApplicationRole SaveRole(ApplicationRole role)
//        {
//            using (var db = Conn())
//            {
//                try
//                {
//                    DataRepository.Models.ApplicationRole data = _mapper.Map<Model.Shared.ApplicationRole, DataRepository.Models.ApplicationRole>(role, new DataRepository.Models.ApplicationRole());
//                    db.Save(data);
//                    return role;
//                }
//                catch (Exception e)
//                {
//                    if (e.Message.Contains("Cannot insert duplicate key row in object"))
//                    {
//                        throw new Exception($"There is already a Role named '{role.Name}'");
//                    }
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region DeleteRoleByID
//        /// <summary>
//        /// Deletes the Role specified by id
//        /// <para>Returns ApplicationRole</para>
//        /// </summary>
//        /// /// <param name="id">string</param>
//        /// <returns>ApplicationRole</returns>
//        public ApplicationRole DeleteRoleByID(string id)
//        {
//            using (var db = Conn())
//            {
//                try
//                {
//                    DataRepository.Models.ApplicationRole role = _mapper.Map<Model.Shared.ApplicationRole, DataRepository.Models.ApplicationRole>(FindRoleByID(id), new DataRepository.Models.ApplicationRole());
//                    db.Delete(role);
//                    return role;
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region FindRoleByID
//        /// <summary>
//        /// Gets the Role specified by id
//        /// <para>Returns ApplicationRole</para>
//        /// </summary>
//        /// <param name="id">string</param>
//        /// <returns>ApplicationRole</returns>
//        public ApplicationRole FindRoleByID(string id)
//        {
//            //Retrieve the Role from the AuthDB
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    return db.Single<ApplicationRole>("select * from dbo.AspNetRoles WHERE Id = @ID", new { ID = id });
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region FindRoleByRoleName
//        /// <summary>
//        /// Gets the Role specified by name
//        /// <para>Returns ApplicationRoleDTOModel</para>
//        /// </summary>
//        /// <param name="name">string</param>
//        /// <returns>ApplicationRoleDTOModel</returns>
//        public ApplicationRole FindRoleByRoleName(string name)
//        {
//            //Retrieve the Role from the AuthDB
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    return db.Single<ApplicationRole>("select * from dbo.AspNetRoles WHERE Name = @Name", new { Name = name });
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion
//        #endregion

//        #region Claim
//        #region GetClaimsByUserID
//        /// <summary>
//        /// Get all the Claims for the User specified by userID
//        /// <para>Returns List<ApplicationUserClaim></para>
//        /// </summary>
//        /// <param name="userID"></param>
//        /// <returns>List<ApplicationClaim></returns>
//        public List<ApplicationUserClaim> GetClaimsByUserID(string userID)
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    return db.Fetch<ApplicationUserClaim>("SELECT anuc.* FROM AspNetUserClaims anuc WHERE UserID = @UserID", new { UserID = userID });
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region GetClaimsByRoleID
//        /// <summary>
//        /// Get all the Claims for the Role specified by roleID
//        /// <para>Returns List<ApplicationRoleClaim></para>
//        /// </summary>
//        /// <param name="roleID">string</param>
//        /// <returns>List<List<ApplicationRoleClaim>></returns>
//        public List<ApplicationRoleClaim> GetClaimsByRoleID(string roleID)
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    return db.Fetch<ApplicationRoleClaim>("SELECT anrc.* FROM AspNetRoleClaims anuc WHERE RoleID = @RoleID", new { RoleID = roleID });
//                }
//                catch (Exception e)
//                {
//                    LogWarning("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region AddUserClaims
//        /// <summary>
//        /// Adds all the Claims specified by claims to the specified userID
//        /// <para>Returns  List<ApplicationClaim></para>
//        /// </summary>
//        /// <param name="userID"></param>
//        /// <param name="claims"></param>
//        /// <returns>List<ApplicationClaim></returns>
//        public List<ApplicationClaim> AddUserClaims(string userID, List<ApplicationClaim> claims)
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    List<ApplicationUserClaim> userClaims = GetClaimsByUserID(userID);
//                    foreach (ApplicationClaim claim in claims)
//                    {
//                        try
//                        {
//                            ApplicationUserClaim addClaim = new DataRepository.Models.ApplicationUserClaim()
//                            {
//                                UserID = userID,
//                                ClaimType = claim.ClaimType,
//                                ClaimValue = claim.ClaimValue,
//                            };
//                            db.Save<ApplicationUserClaim>(addClaim);
//                        }
//                        catch (Exception e)
//                        {

//                            LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                            throw new Exception(e.Message);
//                        }
//                    }

//                    return db.Fetch<ApplicationClaim>("SELECT anuc.* FROM dbo.AspNetUserClaims anuc WHERE anuc.UserID = @UserID", new { UserID = userID });
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion

//        #region RemoveClaims
//        /// <summary>
//        /// Remove all the Claims specified by claims for the specified userID
//        /// <para>Returns List<ApplicationClaim></para>
//        /// </summary>
//        /// <param name="userID"></param>
//        /// <param name="claims"></param>
//        /// <returns>List<ApplicationClaim></returns>
//        public List<ApplicationClaim> RemoveUserClaims(string userID, List<ApplicationClaim> claims)
//        {
//            using (IDatabase db = Conn())
//            {
//                try
//                {
//                    List<ApplicationUserClaim> userClaims = GetClaimsByUserID(userID);
//                    foreach (ApplicationClaim claim in claims)
//                    {
//                        try
//                        {
//                            ApplicationUserClaim deleteClaim = userClaims.Where(x => x.ClaimType == claim.ClaimType).First();
//                            db.Delete(_mapper.Map<ApplicationUserClaim, DataRepository.Models.ApplicationUserClaim>(deleteClaim, new DataRepository.Models.ApplicationUserClaim()));
//                        }
//                        catch (Exception)
//                        {

//                            continue;
//                        }
//                    }
                    
//                    return db.Fetch<ApplicationClaim>("SELECT anuc.* FROM dbo.AspNetUserClaims anuc WHERE anuc.UserID = @UserID", new { UserID = userID });
//                }
//                catch (Exception e)
//                {
//                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
//                    throw new Exception(e.Message);
//                }
//            }
//        }
//        #endregion
//        #endregion
//    }
//}
