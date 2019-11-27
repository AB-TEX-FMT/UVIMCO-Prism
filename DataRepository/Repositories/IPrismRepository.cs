using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IPrismRepository
    {
        #region GetReportGroups
        /// <summary>
        /// Retrieve a list of all ReportGroups
        /// <para>Returns List<ReportGroup></para>
        /// </summary>
        /// <returns>List<ReportGroup></returns>
        List<ReportGroup> GetReportGroups();
        #endregion

        #region GetPerformanceIndicators
        /// <summary>
        /// Retrieve a list of all PerformanceIndicators
        /// <para>Returns List<PerformanceIndicator></para>
        /// </summary>
        /// <returns>List<PerformanceIndicator></returns>
        List<PerformanceIndicator> GetPerformanceIndicators();
        #endregion
    }
}
