using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IPrismRepository
    {
        #region Categories
        /// <summary>
        /// Retrieve a list of all ReportGroup
        /// <para>Returns List<ReportGroup></para>
        /// </summary>
        /// <returns>List<ReportGroup></returns>
        List<ReportGroup> GetReportGroups();
        #endregion
    }
}
