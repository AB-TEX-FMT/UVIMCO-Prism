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

        #region GetPerformanceIndicators
        /// <summary>
        /// Retrieve a list of all PerformanceIndicators
        /// <para>Returns PerformanceIndicatorsListDTOModel</para>
        /// </summary>
        /// <returns>PerformanceIndicatorsListDTOModel</returns>
        public async Task<PerformanceIndicatorsListDTOModel> GetPerformanceIndicatorsAsync()
        {
            return await GetPerformanceIndicatorsAsync(new CancellationToken(false));
        }

        public async Task<PerformanceIndicatorsListDTOModel> GetPerformanceIndicatorsAsync(CancellationToken token)
        {
            return await Task.FromResult<PerformanceIndicatorsListDTOModel>(GetPerformanceIndicators());
        }

        public PerformanceIndicatorsListDTOModel GetPerformanceIndicators()
        {
            try
            {
                return new PerformanceIndicatorsListDTOModel()
                {
                    Items = _repository.GetPerformanceIndicators()
                };
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                return new PerformanceIndicatorsListDTOModel()
                {
                    ErrorMessage = e.Message,
                };
            }
        }
        #endregion
    }
}
