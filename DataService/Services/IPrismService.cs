using DataModel.DTOModels;
using DataModel.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataService.Services
{
    public interface IPrismService
    {
        #region GetReportGroups
        /// <summary>
        /// Retrieve a list of all ReportGroups
        /// <para>Returns ReportGroupListDTOModel</para>
        /// </summary>
        /// <returns>ReportGroupListDTOModel</returns>
        ReportGroupListDTOModel GetReportGroups();
        Task<ReportGroupListDTOModel> GetReportGroupsAsync();
        Task<ReportGroupListDTOModel> GetReportGroupsAsync(CancellationToken token);
        #endregion
    }
}
