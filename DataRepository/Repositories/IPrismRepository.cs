using DataModel.DTOModels;
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

        #region GetReport
        /// <summary>
        /// Retrieve a ReportDTOModel
        /// <para>Returns ReportDTOModel</para>
        /// </summary>
        /// <returns>ReportDTOModel</returns>
        ReportDTOModel GetReport(ReportDTOModel reportDTOModel);
        #endregion

        #region GetComponents
        /// <summary>
        /// Retrieve a ComponentsDTOModel
        /// <para>Returns ComponentsDTOModel</para>
        /// </summary>
        /// <returns>ComponentsDTOModel</returns>
        ComponentsDTOModel GetComponents(ComponentsDTOModel componentsDTOModel);
        #endregion

        #region GetComponent
        /// <summary>
        /// Retrieve a ComponentDTOModel
        /// <para>Returns Component</para>
        /// </summary>
        /// <returns>Component</returns>
        Component GetComponent(Component component);
        #endregion
    }
}
