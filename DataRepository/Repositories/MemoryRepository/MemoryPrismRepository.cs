using Microsoft.Extensions.Logging;
using DataRepository.Repositories;
using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DataRepository.MemoryRepository
{
    public class MemoryPrismRepository : BaseRepository, IPrismRepository
    {
        #region Class Setup
        [SuppressMessage("Microsoft.Usage", "IDE0044:MakeFieldReadOnly", MessageId = "args")]
        private readonly List<ReportGroup> _reportGroups;
        private readonly List<ReportDef> _returnGroupReturn;
        private readonly List<ReportDef> _returnGroupExposure;
        private readonly List<ReportDef> _returnGroupMisc;
        private readonly List<PerformanceIndicator> _performanceIndicators;

        public MemoryPrismRepository(ILogger<MemoryPrismRepository> logger) : base(logger)
        {
            #region ReportGroups
            _returnGroupReturn = new List<ReportDef>()
            {
                new ReportDef()
                {
                    ID = 1,
                    ReportGroupID = 1,
                    Name = "Return Report 1",
                    Description = "A description for this report",
                    URL = "https://gallery.shinyapps.io/051-movie-explorer",
                },
                new ReportDef()
                {
                    ID = 2,
                    ReportGroupID = 1,
                    Name = "Return Report 2",
                    Description = "A description for this report",
                    URL = "https://gallery.shinyapps.io/lego-viz/",
                },
            };

            _returnGroupExposure = new List<ReportDef>()
            {
                new ReportDef()
                {
                    ID = 3,
                    ReportGroupID = 2,
                    Name = "Exposure Report 1",
                    Description = "A description for this report",
                    URL = "https://gallery.shinyapps.io/050-kmeans-example",
                },
                new ReportDef()
                {
                    ID = 4,
                    ReportGroupID = 2,
                    Name = "Exposure Report 2",
                    Description = "A description for this report",
                    URL = "PartialReportPerformance",
                },
            };

            _returnGroupMisc = new List<ReportDef>()
            {
                new ReportDef()
                {
                    ID = 5,
                    ReportGroupID = 3,
                    Name = "Misc Report 1",
                    Description = "A description for this report",
                },
                new ReportDef()
                {
                    ID = 6,
                    ReportGroupID = 3,
                    Name = "Misc Report 2",
                    Description = "A description for this report",
                },
            };

            _reportGroups = new List<ReportGroup>()
            {
                new ReportGroup()
                {
                    ID = 1,
                    Name = "Return",
                    Description = "Reports related to Investment Returns",
                    ReportDefs = _returnGroupReturn,
                },
                new ReportGroup()
                {
                    ID = 2,
                    Name = "Exposure",
                    Description = "Reports related to Investment Exposure",
                    ReportDefs = _returnGroupExposure,
                },
                new ReportGroup()
                {
                    ID = 3,
                    Name = "Misc",
                    Description = "Contains reports that don't fit into any other category",
                    ReportDefs = _returnGroupMisc,
                },
            };
            #endregion

            #region PerformanceIndicators
            _performanceIndicators = new List<PerformanceIndicator>()
            {
                new PerformanceIndicator()
                {
                    ID = 1,
                    Month = "January",
                    PercentChange = 0.047,
                },
                new PerformanceIndicator()
                {
                    ID = 2,
                    Month = "February",
                    PercentChange = 0.051,
                }
            };
            #endregion
        }
        #endregion

        #region ReportGroups
        public List<ReportGroup> GetReportGroups()
        {
            return _reportGroups;
        }
        #endregion

        #region PerformanceIndicators
        public List<PerformanceIndicator> GetPerformanceIndicators()
        {
            return _performanceIndicators;
        }
        #endregion
    }
}
