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

        #region GetReport
        /// <summary>
        /// Retrieve the Report and it's Meta Data
        /// <para>Returns ReportDTOModel</para>
        /// </summary>
        /// <returns>Returns ReportDTOModel</returns>
        ReportDTOModel GetReport(ReportDTOModel reportDTOModel);
        Task<ReportDTOModel> GetReportAsync(ReportDTOModel reportDTOModel);
        Task<ReportDTOModel> GetReportAsync(CancellationToken token, ReportDTOModel reportDTOModel);
        #endregion

        #region GetComponents
        /// <summary>
        /// Retrieve a list of all Components Meta Data
        /// <para>Returns ComponentsDTOModel</para>
        /// </summary>
        /// <returns>Returns ComponentsDTOModel</returns>
        ComponentsDTOModel GetComponents(ComponentsDTOModel componentsDTOModel);
        Task<ComponentsDTOModel> GetComponentsAsync(ComponentsDTOModel componentsDTOModel);
        Task<ComponentsDTOModel> GetComponentsAsync(CancellationToken token, ComponentsDTOModel componentsDTOModel);
        #endregion

        #region GetComponent
        /// <summary>
        /// Retrieve a Component and it's Meta Data
        /// <para>Returns ComponentsDTOModel</para>
        /// </summary>
        /// <returns>Returns ComponentsDTOModel</returns>
        ComponentDTOModel GetComponent(ComponentDTOModel componentDTOModel);
        Task<ComponentDTOModel> GetComponentAsync(ComponentDTOModel componentDTOModel);
        Task<ComponentDTOModel> GetComponentAsync(CancellationToken token, ComponentDTOModel componentDTOModel);
        #endregion
    }
}
