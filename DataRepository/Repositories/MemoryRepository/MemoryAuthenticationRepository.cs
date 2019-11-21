//using Microsoft.Extensions.Logging;
//using DataRepository.Repositories;
//using DataModel.Shared;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;

//namespace DataRepository.MemoryRepository
//{
//    public class MemoryAuthenticationRepository : BaseRepository, IAuthenticationRepository
//    {
//        #region Class Setup
//        [SuppressMessage("Microsoft.Usage", "IDE0044:MakeFieldReadOnly", MessageId = "args")]
//        private List<SafeItemDetail> _safeItems;
//        private readonly List<Category> _categories;

//        public MemoryAuthenticationRepository(ILogger<MemorySafeRepository> logger) : base(logger)
//        {
//            _safeItems = new List<SafeItemDetail>()
//            {
//                new SafeItemDetail()
//                {
//                    ID = 1,
//                    Name = "Test",
//                    Color = "Red",
//                    Manufacturer = "Test Manufacturer",
//                    Make = "Test Make",
//                    Model = "Test Model",
//                    CategoryID = 1,
//                    Serial = "123Test456",
//                    PurchaseDate = DateTime.Now,
//                    Notes = "Some note",
//                },
//                new SafeItemDetail()
//                {
//                    ID = 2,
//                    Name = "Test 2",
//                    Color = "Red 2",
//                    Manufacturer = "Test Manufacturer 2",
//                    Make = "Test Make 2",
//                    Model = "Test Model 2",
//                    CategoryID = 2,
//                    Serial = "123Test456",
//                    PurchaseDate = DateTime.Now,
//                    Notes = "Some note 2",
//                },
//                new SafeItemDetail()
//                {
//                    ID = 3,
//                    Name = "Test 3",
//                    Color = "Red 3",
//                    Manufacturer = "Test Manufacturer 3",
//                    Make = "Test Make 3",
//                    Model = "Test Model 3",
//                    CategoryID = 3,
//                    Serial = "123Test456",
//                    PurchaseDate = DateTime.Now,
//                    Notes = "Some note 3",
//                },
//                new SafeItemDetail()
//                {
//                    ID = 4,
//                    Name = "Test 4",
//                    Color = "Red 4",
//                    Manufacturer = "Test Manufacturer 4",
//                    Make = "Test Make 4",
//                    Model = "Test Model 4",
//                    CategoryID = 4,
//                    Serial = "123Test456",
//                    PurchaseDate = DateTime.Now,
//                    Notes = "Some note 4",
//                },
//            };

//            _categories = new List<Category>()
//            {
//                new Category()
//                {
//                    ID = 1,
//                    Name = "Tools",
//                    Description = "Tools",
//                },
//                new Category()
//                {
//                    ID = 2,
//                    Name = "Electronics",
//                    Description = "Electronics",
//                },
//                new Category()
//                {
//                    ID = 2,
//                    Name = "Jewelry",
//                    Description = "Jewelry",
//                },
//            };
//        }
//        #endregion

//        #region Users
//        public List<ApplicationUser> GetUsers()
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationUser DeleteUserByID(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationUser SaveUser(ApplicationUser user)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationUser FindUserByID(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationUser FindUserByUserName(string userName)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationUser FindUserByEmail(string email)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion

//        #region Roles
//        public ApplicationRole SaveRole(ApplicationRole role)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationRole FindRoleByID(string roleId)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationRole FindRoleByRoleName(string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationRole> GetRoles(string userID)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationRole> GetRoles()
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationRole AddToRole(string userID, ApplicationRole role)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationUserRole RemoveFromRole(string userID, ApplicationRole role)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion

//        #region Claims
//        public List<ApplicationClaim> AddClaims(List<ApplicationClaim> claim)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationClaim> RemoveClaims(List<ApplicationClaim> claims)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationClaim> GetClaims(string userID)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationRole> RemoveFromRole(string userID, string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationClaim> AddUserClaims(string userID, List<ApplicationClaim> claims)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationClaim> RemoveUserClaims(string userID, List<ApplicationClaim> claims)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationUserClaim> GetClaimsByUserID(string userID)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ApplicationRoleClaim> GetClaimsByRoleID(string roleID)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationRole DeleteRoleByID(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public ApplicationRole AddToRole(string userID, string roleName)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion
//    }
//}
