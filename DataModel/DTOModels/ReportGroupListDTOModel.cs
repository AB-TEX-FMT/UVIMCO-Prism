﻿using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Category List Data Transfer Object Model
    /// <para>Items (Category) Holds a list of Category</para>
    /// </summary>
    public class ReportGroupListDTOModel : BaseDTOModel
    {
        public List<ReportGroup> Items { get; set; }
    }
}
