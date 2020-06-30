using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using DataModel.Shared;
using DataModel.DTOModels;
using DataRepository;
using System.Threading;
using System.Reflection;

namespace DataService.Services
{
    public class PrismService : BaseService, IPrismService
    {
        //HttpClient _client;
        //private readonly ServiceOptions _options;
        private readonly IPrismRepository _repository;

        public PrismService(ILogger<PrismService> logger, IPrismRepository repository) : base(logger)
        {
            //    _client = new HttpClient();
            //    _options = options;
            _repository = repository;
        }

        #region GetReportGroups
        /// <summary>
        /// Retrieve a list of all ReportGroups
        /// <para>Returns ReportGroupListDTOModel</para>
        /// </summary>
        /// <returns>ReportGroupListDTOModel</returns>
        public async Task<ReportGroupListDTOModel> GetReportGroupsAsync()
        {
            return await GetReportGroupsAsync(new CancellationToken(false));
        }

        public async Task<ReportGroupListDTOModel> GetReportGroupsAsync(CancellationToken token)
        {
            return await Task.FromResult<ReportGroupListDTOModel>(GetReportGroups());
        }

        public ReportGroupListDTOModel GetReportGroups()
        {
            try
            {
                return new ReportGroupListDTOModel()
                {
                    Items = _repository.GetReportGroups()
                };
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                return new ReportGroupListDTOModel()
                {
                    ErrorMessage = e.Message,
                };
            }
        }
        #endregion

        #region GetReport
        /// <summary>
        /// Retrieve a report
        /// <para>Returns ReportDTOModel</para>
        /// </summary>
        /// <returns>ReportDTOModel</returns>
        public async Task<ReportDTOModel> GetReportAsync(ReportDTOModel reportDTOModel)
        {
            return await GetReportAsync(new CancellationToken(false), reportDTOModel);
        }

        public async Task<ReportDTOModel> GetReportAsync(CancellationToken token, ReportDTOModel reportDTOModel)
        {
            return await Task.FromResult<ReportDTOModel>(GetReport(reportDTOModel));
        }

        public ReportDTOModel GetReport(ReportDTOModel reportDTOModel)
        {
            try
            {
                return _repository.GetReport(reportDTOModel);
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                return new ReportDTOModel()
                {
                    ErrorMessage = e.Message,
                };
            }
        }
        #endregion

        #region GetComponents
        /// <summary>
        /// Retrieve a list of Component Meta Data
        /// <para>Returns ReportDTOModel</para>
        /// </summary>
        /// <returns>ReportDTOModel</returns>
        public async Task<ComponentsDTOModel> GetComponentsAsync(ComponentsDTOModel componentsDTOModel)
        {
            return await GetComponentsAsync(new CancellationToken(false), componentsDTOModel);
        }

        public async Task<ComponentsDTOModel> GetComponentsAsync(CancellationToken token, ComponentsDTOModel componentsDTOModel)
        {
            return await Task.FromResult<ComponentsDTOModel>(GetComponents(componentsDTOModel));
        }

        public ComponentsDTOModel GetComponents(ComponentsDTOModel componentsDTOModel)
        {
            try
            {
                return _repository.GetComponents(componentsDTOModel);
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                return new ComponentsDTOModel()
                {
                    ErrorMessage = e.Message,
                };
            }
        }
        #endregion

        #region GetComponent
        /// <summary>
        /// Retrieve a Component and it's Meta Data
        /// <para>Returns ComponentDTOModel</para>
        /// </summary>
        /// <returns>ComponentDTOModel</returns>
        public async Task<ComponentDTOModel> GetComponentAsync(ComponentDTOModel componentDTOModel)
        {
            return await GetComponentAsync(new CancellationToken(false), componentDTOModel);
        }

        public async Task<ComponentDTOModel> GetComponentAsync(CancellationToken token, ComponentDTOModel componentDTOModel)
        {
            return await Task.FromResult<ComponentDTOModel>(GetComponent(componentDTOModel));
        }

        public ComponentDTOModel GetComponent(ComponentDTOModel componentDTOModel)
        {
            try
            {
                componentDTOModel = _repository.GetComponent(componentDTOModel);
                return componentDTOModel;
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                return new ComponentDTOModel()
                {
                    ErrorMessage = e.Message,
                };
            }
        }
        #endregion
    }
}
