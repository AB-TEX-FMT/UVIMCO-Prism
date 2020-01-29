using DataModel.BaseModels;
using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    public class ApplicationRolesDTOModel : BaseModel
    {
        public List<ApplicationRole> Items { get; set; }

    }
}
