using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.DTOModels
{
    /// <summary>
    /// PerformanceIndicator List Data Transfer Object Model
    /// <para>Items (PerformanceIndicator) Holds a list of PerformanceIndicators</para>
    /// </summary>
    public class PerformanceIndicatorsListDTOModel : BaseDTOModel
    {
        public List<PerformanceIndicator> Items { get; set; }
    }
}
