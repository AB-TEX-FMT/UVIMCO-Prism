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
using DataModel.DTOModels;
using DataRepository.NPocoRepository.MapObjects;
using AutoMapper;
using IMapper = AutoMapper.IMapper;

namespace DataRepository.NPocoRepository
{
    public class NPocoPrismRepository : BaseRepository, IPrismRepository
    {
        #region Class Setup
        private readonly IDBFactory _dbFactory;
        private readonly IMapper _mapper;

        public NPocoPrismRepository(ILogger<NPocoPrismRepository> logger, IDBFactory dbFactory, IMapper mapper) : base(logger)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
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
                List<ReportGroupMapping> data = db.FetchOneToMany<ReportGroupMapping>(x => x.ReportDefMappings, "exec rpt.GetReportGroups");
                List<ReportGroup> output = _mapper.Map<List<ReportGroupMapping>, List<ReportGroup>>(data);
                return output;
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region GetReport
        /// <summary>
        /// Retrieve a ReportDTOModel
        /// <para>Returns ReportDTOModel</para>
        /// </summary>
        /// <returns>ReportDTOModel</returns>
        public ReportDTOModel GetReport(ReportDTOModel reportDTOModel)
        {
            using IDatabase db = Conn();
            try
            {
                try
                {
                    ReportMetaData reportMetaData = db.Single<ReportMetaData>("exec rpt.GetReportMetaData @ReportID",
                        new
                        {
                            ReportID = int.Parse(reportDTOModel.ReportDefID)
                        });
                    reportDTOModel.ReportMetaData = reportMetaData;
                    return reportDTOModel;
                }
                catch (Exception e)
                {
                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                    throw new Exception(e.Message);
                }
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region GetComponents
        /// <summary>
        /// Retrieve a ComponentsDTOModel
        /// <para>Returns ComponentsDTOModel</para>
        /// </summary>
        /// <returns>ComponentsDTOModel</returns>
        public ComponentsDTOModel GetComponents(ComponentsDTOModel componentsDTOModel)
        {
            using IDatabase db = Conn();
            try
            {
                try
                {
                    List<ComponentMetaData> componentMetaDatas = db.Fetch<ComponentMetaData>("exec rpt.GetReportComponentsMetaData @ReportID",
                        new
                        {
                            ReportID = int.Parse(componentsDTOModel.ReportDefID)
                        });
                    componentsDTOModel.Items = componentMetaDatas;
                    return componentsDTOModel;
                }
                catch (Exception e)
                {
                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                    throw new Exception(e.Message);
                }
            }
            catch (Exception e)
            {
                LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region GetComponent
        /// <summary>
        /// Retrieve a ComponentDTOModel
        /// <para>Returns ComponentDTOModel</para>
        /// </summary>
        /// <returns>ComponentDTOModel</returns>
        public Component GetComponent(Component component)
        {
            using IDatabase db = Conn();
            try
            {
                try
                {
                    List<ColumnMapping> cols = db.Fetch<ColumnMapping>("exec rpt.GetReportComponentMetadata @ComponentID",
                        new
                        {
                            ComponentID = component.ComponentID,
                        }
                    );

                    List<Column> columns = new List<Column>();
                    foreach (ColumnMapping col in cols)
                    {
                        columns.Add(col.GetColumn());
                    }
                    component.ColumnMetaData = new ColumnMetaData();
                    component.ColumnMetaData.AvailableColumns = columns;
                    component.ColumnMetaData.SelectedColumns = columns;
                    //ColumnMetaData columnMetaData = db.Single<ReportMetaData>("exec rpt.GetReportMetaData");
                    //reportDTOModel.ColumnMetaData = columnMetaData;
                    //reportDTOModel.Items = employeeString;
                    //reportDTOModel.TotalItems = 2;
                    return component;
                }
                catch (Exception e)
                {
                    LogCritical("|" + MethodBase.GetCurrentMethod() + "|" + e.Message);
                    throw new Exception(e.Message);
                }
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
