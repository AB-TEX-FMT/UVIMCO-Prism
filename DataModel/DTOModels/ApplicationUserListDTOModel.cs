using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Application User List Data Transfer Object Model
    /// <para>Items (User) Holds a list of User</para>
    /// </summary>
    public class ApplicationUserListDTOModel : BaseDTOModel
    {
        public List<ApplicationUser> Items { get; set; }
    }
}
