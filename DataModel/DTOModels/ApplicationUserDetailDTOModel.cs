using DataModel.BaseModels;
using DataModel.Shared;

namespace DataModel.DTOModels
{
    /// <summary>
    /// User Detail Data Transfer Object Model
    /// <para>Item (User) Holds a User</para>
    /// </summary>
    public class ApplicationUserDetailDTOModel : BaseModel
    {
        public ApplicationUserDetail Item { get; set; }
    }
}
