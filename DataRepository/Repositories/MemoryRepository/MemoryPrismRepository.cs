using Microsoft.Extensions.Logging;
using DataRepository.Repositories;
using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataModel.DTOModels;

namespace DataRepository.MemoryRepository
{
    public class MemoryPrismRepository : BaseRepository, IPrismRepository
    {
        #region Class Setup
        [SuppressMessage("Microsoft.Usage", "IDE0044:MakeFieldReadOnly", MessageId = "args")]
        private readonly List<ReportGroup> _reportGroups;
        private readonly List<ReportDef> _returnGroupAnalysis;
        private readonly List<ReportDef> _returnGroupHoldings;
        private readonly List<ReportDef> _returnGroupMisc;
        private readonly List<PerformanceIndicator> _performanceIndicators;

        public MemoryPrismRepository(ILogger<MemoryPrismRepository> logger) : base(logger)
        {
            #region ReportGroups
            _returnGroupAnalysis = new List<ReportDef>()
            {
                new ReportDef()
                {
                    ID = 1,
                    ReportGroupID = 1,
                    Name = "Portfolio Analysis",
                    Description = "Full UVIMCO Portfolio Review",
                    URL = "PartialReport",
                    RenderNatively = true,
                },
                new ReportDef()
                {
                    ID = 2,
                    ReportGroupID = 1,
                    Name = "Public Fund Analysis",
                    Description = "Review of publicly traded funds",
                    URL = "https://gallery.shinyapps.io/lego-viz/",
                },
                new ReportDef()
                {
                    ID = 3,
                    ReportGroupID = 1,
                    Name = "Private Fund Analysis",
                    Description = "Review of privately held funds",
                    URL = "https://gallery.shinyapps.io/051-movie-explorer",
                },
            };

            _returnGroupHoldings = new List<ReportDef>()
            {
                new ReportDef()
                {
                    ID = 4,
                    ReportGroupID = 2,
                    Name = "Top Holdings",
                    Description = "A description for this report",
                    URL = "https://gallery.shinyapps.io/050-kmeans-example",
                },
                new ReportDef()
                {
                    ID = 5,
                    ReportGroupID = 2,
                    Name = "Place Holder Report 2",
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
                    Name = "Analysis",
                    Description = "Reports related to portfolio/fund analysis",
                    ReportDefs = _returnGroupAnalysis,
                },
                new ReportGroup()
                {
                    ID = 2,
                    Name = "Holdings",
                    Description = "Reports related to portfolio/fund holdings",
                    ReportDefs = _returnGroupHoldings,
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

        #region GetReport
        public ReportDTOModel GetReport(ReportDTOModel reportDTOModel)
        {
            return reportDTOModel;
        }
        #endregion
    }
}
