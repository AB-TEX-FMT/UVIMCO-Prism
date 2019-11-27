using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPoco;
using DataRepository.Factories;
using DataRepository;
using DataRepository.Repositories;
using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static DataRepository.Factories.DbFactory;

namespace DataRepository.NPocoRepository
{
    public class NPocoPrismRepository : BaseRepository, IPrismRepository
    {
        #region Class Setup
        private readonly IDBFactory _dbFactory;

        public NPocoPrismRepository(ILogger<NPocoPrismRepository> logger, IDBFactory dbFactory) : base(logger)
        {
            _dbFactory = dbFactory;
        }

        private IDatabase Conn()
        {
            return _dbFactory.GetConnection(AvailableConnections.App);
        }
        #endregion

        #region ReportGroups
        /// <summary>
        /// Retrieve a list of all ReportGroups
        /// <para>Returns List<ReportGroup></para>
        /// </summary>
        /// <returns>List<ReportGroup></returns>
        public List<ReportGroup> GetReportGroups()
        {
            using IDatabase db = Conn();
            try
            {
                return db.Fetch<ReportGroup>("select * from dbo.vReportGroup");
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region PerformanceIndicators
        /// <summary>
        /// Retrieve a list of all PerformanceIndicators
        /// <para>Returns List<PerformanceIndicator></para>
        /// </summary>
        /// <returns>List<PerformanceIndicator></returns>
        public List<PerformanceIndicator> GetPerformanceIndicators()
        {
            using IDatabase db = Conn();
            try
            {
                return db.Fetch<PerformanceIndicator>("select * from dbo.vPerformanceIndicator");
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
