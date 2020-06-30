using Microsoft.Extensions.Logging;
using DataRepository.Repositories;
using DataModel.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataModel.DTOModels;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                    ID = "1",
                    ReportGroupID = "1",
                    Name = "Portfolio Analysis",
                    Description = "Full UVIMCO Portfolio Review",
                },
                new ReportDef()
                {
                    ID = "2",
                    ReportGroupID = "1",
                    Name = "Public Fund Analysis",
                    Description = "Review of publicly traded funds",    
                },
                new ReportDef()
                {
                    ID = "3",
                    ReportGroupID = "1",
                    Name = "Private Fund Analysis",
                    Description = "Review of privately held funds",
                },
            };

            _returnGroupHoldings = new List<ReportDef>()
            {
                new ReportDef()
                {
                    ID = "4",
                    ReportGroupID = "2",
                    Name = "Top Holdings",
                    Description = "A description for this report",
                },
                new ReportDef()
                {
                    ID = "5",
                    ReportGroupID = "2",
                    Name = "Place Holder Report 2",
                    Description = "A description for this report",
                },
            };

            _returnGroupMisc = new List<ReportDef>()
            {
                new ReportDef()
                {
                    ID = "5",
                    ReportGroupID = "3",
                    Name = "Misc Report 1",
                    Description = "A description for this report",
                },
                new ReportDef()
                {
                    ID = "6",
                    ReportGroupID = "3",
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
            switch (reportDTOModel.ReportDefID)
            {
                case "1":

                    ReportMetaData report = new ReportMetaData()
                    {
                        ReportTitle = "Full UVIMCO Portfolio Review - Performance",
                        ReportHeader = "",
                        ReportFootNote = "",
                        URL = "PartialReport",
                        RenderNatively = true,
                    };

                    reportDTOModel.ReportMetaData = report;
                    break;

                case "2":
                    reportDTOModel.ReportMetaData = new ReportMetaData
                    {
                        URL = "https://gallery.shinyapps.io/lego-viz/",
                        RenderNatively = false
                    };
                    break;

                case "3":
                    reportDTOModel.ReportMetaData = new ReportMetaData
                    {
                        URL = "https://shiny.uvimco.org/shinyRTDA/",
                        RenderNatively = false
                    };
                    break;

                case "4":
                    reportDTOModel.ReportMetaData = new ReportMetaData
                    {
                        URL = "https://gallery.shinyapps.io/050-kmeans-example",
                        RenderNatively = false
                    };
                    break;

                case "5":
                    reportDTOModel.ReportMetaData = new ReportMetaData
                    {
                        URL = "https://gallery.shinyapps.io/lego-viz/",
                        RenderNatively = false
                    };
                    break;
            }
            return reportDTOModel;
        }
        #endregion

        #region GetComponents
        public ComponentsDTOModel GetComponents(ComponentsDTOModel componentsDTOModel)
        {
            componentsDTOModel.Items = new List<ComponentMetaData>()
            {
                new ComponentMetaData()
                {
                    GUID = "111111-22222-33333",
                    Name = "Test Component 1",
                    Description = "Test Component 1 Description",
                    ComponentType = ComponentMetaData.ComponentTypeValue.Table,
                    ComponentTitle = "Table Component",
                    ComponentHeader = "Table Component Header",
                    ComponentFootNote = "Table Component Footer",

                },
                new ComponentMetaData()
                {
                    GUID = "111111-22222-44444",
                    Name = "Pie Component",
                    Description = "Pie Component Description",
                    ComponentType = ComponentMetaData.ComponentTypeValue.Pie,
                    ComponentTitle = "Pie Component Title",
                    ComponentHeader = "Pie Component Header",
                    ComponentFootNote = "Pie Component Footer",

                },
                new ComponentMetaData()
                {
                    GUID = "111111-22222-55555",
                    Name = "Line Component",
                    Description = "Line Component Description",
                    ComponentType = ComponentMetaData.ComponentTypeValue.Line,
                    ComponentTitle = "Line Component Title",
                    ComponentHeader = "Line Component Header",
                    ComponentFootNote = "Line Component Footer",

                },
                new ComponentMetaData()
                {
                    GUID = "111111-22222-666666",
                    Name = "Bar Component",
                    Description = "Bar Component Description",
                    ComponentType = ComponentMetaData.ComponentTypeValue.Bar,
                    ComponentTitle = "Bar Component Title",
                    ComponentHeader = "Bar Component Header",
                    ComponentFootNote = "Bar Component Footer",

                },
                new ComponentMetaData()
                {
                    GUID = "111111-22222-77777",
                    Name = "Column Component",
                    Description = "Column Component Description",
                    ComponentType = ComponentMetaData.ComponentTypeValue.Column,
                    ComponentTitle = "Column Component Title",
                    ComponentHeader = "Column Component Header",
                    ComponentFootNote = "Column Component Footer",

                },
                new ComponentMetaData()
                {
                    GUID = "111111-22222-88888",
                    Name = "Area Component",
                    Description = "Area Component Description",
                    ComponentType = ComponentMetaData.ComponentTypeValue.Area,
                    ComponentTitle = "Area Component Title",
                    ComponentHeader = "Area Component Header",
                    ComponentFootNote = "Area Component Footer",

                },
        };
            return componentsDTOModel;
        }
        #endregion

        #region GetComponent
        public ComponentDTOModel GetComponent(ComponentDTOModel componentDTOModel)
        {
            Component component = componentDTOModel.Item;
            switch (component.ComponentGUID)
            {
                
                //Data table
                case "111111-22222-33333":
                    component.ComponentMetaData = new ComponentMetaData()
                    {
                        GUID = "111111-22222-33333",
                        Name = "Test Component 1",
                        Description = "Test Component 1 Description",
                        ComponentType = ComponentMetaData.ComponentTypeValue.Table,
                        ComponentTitle = "Full UVIMCO Portfolio Review - Performance",
                        ComponentHeader = "",
                        ComponentFootNote = "",

                    };
                    component.ColumnMetaData = new ColumnMetaData()
                    {
                        AvailableColumns = new List<Column>()
                        {
                            new Column("Year", "eg 1900", "yr", Column.DataTypeValue.String, false, false, true),
                            new Column("Jan", "", "Jan", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Feb", "", "Feb", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Mar", "", "Mar", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Apr", "", "Apr", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("May", "", "May", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Jun", "", "Jun", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Jul", "", "Jul", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Aug", "", "Aug", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Sep", "", "Sep", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Oct", "", "Oct", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Nov", "", "Nov", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Dec", "", "Dec", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Jan_mv", "", "Jan_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Feb_mv", "", "Feb_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Mar_mv", "", "Mar_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Apr_mv", "", "Apr_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("May_mv", "", "May_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Jun_mv", "", "Jun_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Jul_mv", "", "Jul_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Aug_mv", "", "Aug_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Sep_mv", "", "Sep_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Oct_mv", "", "Oct_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Nov_mv", "", "Nov_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Dec_mv", "", "Dec_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Jan_src", "", "Jan_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Feb_src", "", "Feb_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Mar_src", "", "Mar_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Apr_src", "", "Apr_src", Column.DataTypeValue.String, false, false, false),
                            new Column("May_src", "", "May_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Jun_src", "", "Jun_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Jul_src", "", "Jul_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Aug_src", "", "Aug_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Sep_src", "", "Sep_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Oct_src", "", "Oct_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Nov_src", "", "Nov_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Dec_src", "", "Dec_src", Column.DataTypeValue.String, false, false, false),
                            new Column("CCYR", "", "CCYR", Column.DataTypeValue.Currency, false, false, false),
                            new Column("CYR", "", "ACCYR", Column.DataTypeValue.Currency, false, false, true),
                            new Column("CCYR_count", "", "CCYR_count", Column.DataTypeValue.Currency, false, false, false),
                            new Column("CFYR", "", "CFYR", Column.DataTypeValue.Currency, false, false, false),
                            new Column("FYR", "", "ACFYR", Column.DataTypeValue.Currency, false, false, true),
                            new Column("CFYR_count", "", "CFYR_count", Column.DataTypeValue.Currency, false, false, false),
                            new Column("UVIMCO Strategy", "", "bmk_uvimco_strategy", Column.DataTypeValue.Currency, false, false, true),
                            new Column("MSCI ACWI", "", "bmk_msci_acwi", Column.DataTypeValue.Currency, false, false, false),
                         },
                        SelectedColumns = new List<Column>()
                        {
                            new Column("Year", "eg 1900", "yr", Column.DataTypeValue.String, false, false, true),
                            new Column("Jan", "", "Jan", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Feb", "", "Feb", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Mar", "", "Mar", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Apr", "", "Apr", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("May", "", "May", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Jun", "", "Jun", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Jul", "", "Jul", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Aug", "", "Aug", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Sep", "", "Sep", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Oct", "", "Oct", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Nov", "", "Nov", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Dec", "", "Dec", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("Jan_mv", "", "Jan_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Feb_mv", "", "Feb_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Mar_mv", "", "Mar_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Apr_mv", "", "Apr_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("May_mv", "", "May_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Jun_mv", "", "Jun_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Jul_mv", "", "Jul_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Aug_mv", "", "Aug_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Sep_mv", "", "Sep_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Oct_mv", "", "Oct_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Nov_mv", "", "Nov_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Dec_mv", "", "Dec_mv", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("Jan_src", "", "Jan_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Feb_src", "", "Feb_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Mar_src", "", "Mar_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Apr_src", "", "Apr_src", Column.DataTypeValue.String, false, false, false),
                            new Column("May_src", "", "May_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Jun_src", "", "Jun_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Jul_src", "", "Jul_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Aug_src", "", "Aug_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Sep_src", "", "Sep_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Oct_src", "", "Oct_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Nov_src", "", "Nov_src", Column.DataTypeValue.String, false, false, false),
                            new Column("Dec_src", "", "Dec_src", Column.DataTypeValue.String, false, false, false),
                            new Column("CCYR", "", "CCYR", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("CYR", "", "ACCYR", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("CCYR_count", "", "CCYR_count", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("CFYR", "", "CFYR", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("FYR", "", "ACFYR", Column.DataTypeValue.Decimal, false, false, true),
                            new Column("CFYR_count", "", "CFYR_count", Column.DataTypeValue.Decimal, false, false, false),
                            new Column("UVIMCO Strategy", "", "bmk_uvimco_strategy", Column.DataTypeValue.String, false, false, true),
                            new Column("MSCI ACWI", "", "bmk_msci_acwi", Column.DataTypeValue.String, false, false, true),
                         },
                    };

                    DataSet dataSet = new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("yr", typeof(String));
                    dataTable.Columns.Add("Jan", typeof(decimal));
                    dataTable.Columns.Add("Feb", typeof(decimal));
                    dataTable.Columns.Add("Mar", typeof(decimal));
                    dataTable.Columns.Add("Apr", typeof(decimal));
                    dataTable.Columns.Add("May", typeof(decimal));
                    dataTable.Columns.Add("Jun", typeof(decimal));
                    dataTable.Columns.Add("Jul", typeof(decimal));
                    dataTable.Columns.Add("Aug", typeof(decimal));
                    dataTable.Columns.Add("Sep", typeof(decimal));
                    dataTable.Columns.Add("Oct", typeof(decimal));
                    dataTable.Columns.Add("Nov", typeof(decimal));
                    dataTable.Columns.Add("Dec", typeof(decimal));
                    dataTable.Columns.Add("Jan_mv", typeof(decimal));
                    dataTable.Columns.Add("Feb_mv", typeof(decimal));
                    dataTable.Columns.Add("Mar_mv", typeof(decimal));
                    dataTable.Columns.Add("Apr_mv", typeof(decimal));
                    dataTable.Columns.Add("May_mv", typeof(decimal));
                    dataTable.Columns.Add("Jun_mv", typeof(decimal));
                    dataTable.Columns.Add("Jul_mv", typeof(decimal));
                    dataTable.Columns.Add("Aug_mv", typeof(decimal));
                    dataTable.Columns.Add("Sep_mv", typeof(decimal));
                    dataTable.Columns.Add("Oct_mv", typeof(decimal));
                    dataTable.Columns.Add("Nov_mv", typeof(decimal));
                    dataTable.Columns.Add("Dec_mv", typeof(decimal));
                    dataTable.Columns.Add("Jan_src", typeof(String));
                    dataTable.Columns.Add("Feb_src", typeof(String));
                    dataTable.Columns.Add("Mar_src", typeof(String));
                    dataTable.Columns.Add("Apr_src", typeof(String));
                    dataTable.Columns.Add("May_src", typeof(String));
                    dataTable.Columns.Add("Jun_src", typeof(String));
                    dataTable.Columns.Add("Jul_src", typeof(String));
                    dataTable.Columns.Add("Aug_src", typeof(String));
                    dataTable.Columns.Add("Sep_src", typeof(String));
                    dataTable.Columns.Add("Oct_src", typeof(String));
                    dataTable.Columns.Add("Nov_src", typeof(String));
                    dataTable.Columns.Add("Dec_src", typeof(String));
                    dataTable.Columns.Add("CCYR", typeof(long));
                    dataTable.Columns.Add("ACCYR", typeof(long));
                    dataTable.Columns.Add("CCYR_count", typeof(int));
                    dataTable.Columns.Add("CFYR", typeof(long));
                    dataTable.Columns.Add("ACFYR", typeof(long));
                    dataTable.Columns.Add("CFYR_count", typeof(int));
                    dataTable.Columns.Add("bmk_uvimco_strategy", typeof(String));
                    dataTable.Columns.Add("bmk_msci_acwi", typeof(String));

                    DataRow row = dataTable.NewRow();
                    row["yr"] = "2011";
                    row["Jan"] = 0.003517;
                    row["Feb"] = 0.018995;
                    row["Mar"] = 0.016337;
                    row["Apr"] = 0.024354;
                    row["May"] = 0.012683;
                    row["Jun"] = 0.024106;
                    row["Jul"] = 0.006856;
                    row["Aug"] = -0.020131;
                    row["Sep"] = -0.030129;
                    row["Oct"] = 0.022629;
                    row["Nov"] = -0.00848;
                    row["Dec"] = -0.003181;
                    row["Jan_mv"] = 4931604312.4000000000;
                    row["Feb_mv"] = 5028110625.8000000000;
                    row["Mar_mv"] = 5109422325.7000000000;
                    row["Apr_mv"] = 5236504158.8000000000;
                    row["May_mv"] = 5229364442.5000000000;
                    row["Jun_mv"] = 5346502216.2000000000;
                    row["Jul_mv"] = 5380438937.4000000000;
                    row["Aug_mv"] = 5264985707.4000000000;
                    row["Sep_mv"] = 5099811060.0000000000;
                    row["Oct_mv"] = 5212256142.2000000000;
                    row["Nov_mv"] = 5087746371.0000000000;
                    row["Dec_mv"] = 5067346468.3000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.067795942;
                    row["ACCYR"] = 0.067795942;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = 0.242928896;
                    row["ACFYR"] = 0.242928896;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    row = dataTable.NewRow();
                    row["yr"] = "2012";
                    row["Jan"] = 0.025296;
                    row["Feb"] = 0.029299;
                    row["Mar"] = 0.014897;
                    row["Apr"] = 0.011438;
                    row["May"] = -0.012664;
                    row["Jun"] = 0.016105;
                    row["Jul"] = 0.005692;
                    row["Aug"] = 0.012281;
                    row["Sep"] = 0.011941;
                    row["Oct"] = -0.000295;
                    row["Nov"] = 0.015646;
                    row["Dec"] = 0.008332;
                    row["Jan_mv"] = 5193700725.5000000000;
                    row["Feb_mv"] = 5354121923.5000000000;
                    row["Mar_mv"] = 5432895854.6000000000;
                    row["Apr_mv"] = 5491140264.3000000000;
                    row["May_mv"] = 5347885328.4000000000;
                    row["Jun_mv"] = 5430017096.8000000000;
                    row["Jul_mv"] = 5462607474.1000000000;
                    row["Aug_mv"] = 5526763015.5000000000;
                    row["Sep_mv"] = 5591491234.1000000000;
                    row["Oct_mv"] = 5596209869.7000000000;
                    row["Nov_mv"] = 5678151725.7000000000;
                    row["Dec_mv"] = 5723390524.8000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.146290411;
                    row["ACCYR"] = 0.146290411;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = 0.0510923;
                    row["ACFYR"] = 0.0510923;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    row = dataTable.NewRow();
                    row["yr"] = "2013";
                    row["Jan"] = 0.022026;
                    row["Feb"] = 0.008012;
                    row["Mar"] = 0.019731;
                    row["Apr"] = 0.014331;
                    row["May"] = 0.013904;
                    row["Jun"] = -0.004737;
                    row["Jul"] = 0.008999;
                    row["Aug"] = -0.004985;
                    row["Sep"] = 0.025306;
                    row["Oct"] = 0.019063;
                    row["Nov"] = 0.014232;
                    row["Dec"] = 0.019459;
                    row["Jan_mv"] = 5787055973.9000000000;
                    row["Feb_mv"] = 5830067337.9000000000;
                    row["Mar_mv"] = 5942287572.6000000000;
                    row["Apr_mv"] = 5983104069.2000000000;
                    row["May_mv"] = 5985505188.4000000000;
                    row["Jun_mv"] = 5959541292.1000000000;
                    row["Jul_mv"] = 6012975867.4000000000;
                    row["Aug_mv"] = 5980762658.8000000000;
                    row["Sep_mv"] = 6123544630.8000000000;
                    row["Oct_mv"] = 6239957412.3000000000;
                    row["Nov_mv"] = 6306371255.2000000000;
                    row["Dec_mv"] = 6393780257.9000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.166298271;
                    row["ACCYR"] = 0.166298271;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = 0.134140116;
                    row["ACFYR"] = 0.134140116;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    row = dataTable.NewRow();
                    row["yr"] = "2014";
                    row["Jan"] = -0.007889;
                    row["Feb"] = 0.027473;
                    row["Mar"] = -0.001486;
                    row["Apr"] = 0.000354;
                    row["May"] = 0.038469;
                    row["Jun"] = 0.037634;
                    row["Jul"] = -0.005104;
                    row["Aug"] = 0.013587;
                    row["Sep"] = -0.010838;
                    row["Oct"] = 0.001197;
                    row["Nov"] = 0.015392;
                    row["Dec"] = -0.004807;
                    row["Jan_mv"] = 6340720750.5000000000;
                    row["Feb_mv"] = 6514315139.7000000000;
                    row["Mar_mv"] = 6514219163.2000000000;
                    row["Apr_mv"] = 6514840100.3000000000;
                    row["May_mv"] = 6702267377.9000000000;
                    row["Jun_mv"] = 6949542818.8000000000;
                    row["Jul_mv"] = 6908137170.2000000000;
                    row["Aug_mv"] = 7002139163.1000000000;
                    row["Sep_mv"] = 6956596587.7000000000;
                    row["Oct_mv"] = 6937116067.5000000000;
                    row["Nov_mv"] = 7036899958.9000000000;
                    row["Dec_mv"] = 7037249106.6000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.107243183;
                    row["ACCYR"] = 0.107243183;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = 0.190030944;
                    row["ACFYR"] = 0.190030944;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    row = dataTable.NewRow();
                    row["yr"] = "2015";
                    row["Jan"] = -0.001805;
                    row["Feb"] = 0.027337;
                    row["Mar"] = 0.01356;
                    row["Apr"] = -0.000938;
                    row["May"] = 0.02276;
                    row["Jun"] = 0.005166;
                    row["Jul"] = 0.010623;
                    row["Aug"] = -0.015402;
                    row["Sep"] = -0.015377;
                    row["Oct"] = 0.015156;
                    row["Nov"] = 0.002278;
                    row["Dec"] = -0.004134;
                    row["Jan_mv"] = 7020049706.7000000000;
                    row["Feb_mv"] = 7205947474.1000000000;
                    row["Mar_mv"] = 7261936214.6000000000;
                    row["Apr_mv"] = 7233395719.0000000000;
                    row["May_mv"] = 7375556639.0000000000;
                    row["Jun_mv"] = 7528349543.0000000000;
                    row["Jul_mv"] = 7604289028.4000000000;
                    row["Aug_mv"] = 7478583214.6000000000;
                    row["Sep_mv"] = 7365517627.5000000000;
                    row["Oct_mv"] = 7474660522.7000000000;
                    row["Nov_mv"] = 7482526048.8000000000;
                    row["Dec_mv"] = 7448191686.4000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.059794692;
                    row["ACCYR"] = 0.059794692;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = 0.077329316;
                    row["ACFYR"] = 0.077329316;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    row = dataTable.NewRow();
                    row["yr"] = "2016";
                    row["Jan"] = -0.028572;
                    row["Feb"] = -0.013852;
                    row["Mar"] = 0.026261;
                    row["Apr"] = 0.00967;
                    row["May"] = 0.000467;
                    row["Jun"] = -0.001342;
                    row["Jul"] = 0.015747;
                    row["Aug"] = 0.012127;
                    row["Sep"] = 0.013548;
                    row["Oct"] = -0.002676;
                    row["Nov"] = 0.008402;
                    row["Dec"] = 0.004985;
                    row["Jan_mv"] = 7280665449.5000000000;
                    row["Feb_mv"] = 7227557673.1000000000;
                    row["Mar_mv"] = 7460938065.3000000000;
                    row["Apr_mv"] = 7583868782.4000000000;
                    row["May_mv"] = 7632304588.5000000000;
                    row["Jun_mv"] = 7619353872.2000000000;
                    row["Jul_mv"] = 7738798411.3000000000;
                    row["Aug_mv"] = 7835699998.1000000000;
                    row["Sep_mv"] = 7939035088.8000000000;
                    row["Oct_mv"] = 7935138084.1000000000;
                    row["Nov_mv"] = 7995204448.3000000000;
                    row["Dec_mv"] = 8035026499.6000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.04448925;
                    row["ACCYR"] = 0.04448925;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = -0.015423334;
                    row["ACFYR"] = -0.015423334;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    row = dataTable.NewRow();
                    row["yr"] = "2017";
                    row["Jan"] = 0.025296;
                    row["Feb"] = 0.029299;
                    row["Mar"] = 0.014897;
                    row["Apr"] = 0.011438;
                    row["May"] = -0.012664;
                    row["Jun"] = 0.016105;
                    row["Jul"] = 0.005692;
                    row["Aug"] = 0.012281;
                    row["Sep"] = 0.011941;
                    row["Oct"] = -0.000295;
                    row["Nov"] = 0.015646;
                    row["Dec"] = 0.008332;
                    row["Jan_mv"] = 5193700725.5000000000;
                    row["Feb_mv"] = 5354121923.5000000000;
                    row["Mar_mv"] = 5432895854.6000000000;
                    row["Apr_mv"] = 5491140264.3000000000;
                    row["May_mv"] = 5347885328.4000000000;
                    row["Jun_mv"] = 5430017096.8000000000;
                    row["Jul_mv"] = 5462607474.1000000000;
                    row["Aug_mv"] = 5526763015.5000000000;
                    row["Sep_mv"] = 5591491234.1000000000;
                    row["Oct_mv"] = 5596209869.7000000000;
                    row["Nov_mv"] = 5678151725.7000000000;
                    row["Dec_mv"] = 5723390524.8000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.146290411;
                    row["ACCYR"] = 0.146290411;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = 0.0510923;
                    row["ACFYR"] = 0.0510923;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    row = dataTable.NewRow();
                    row["yr"] = "2018";
                    row["Jan"] = 0.025296;
                    row["Feb"] = 0.029299;
                    row["Mar"] = 0.014897;
                    row["Apr"] = 0.011438;
                    row["May"] = -0.012664;
                    row["Jun"] = 0.016105;
                    row["Jul"] = 0.005692;
                    row["Aug"] = 0.012281;
                    row["Sep"] = 0.011941;
                    row["Oct"] = -0.000295;
                    row["Nov"] = 0.015646;
                    row["Dec"] = 0.008332;
                    row["Jan_mv"] = 5193700725.5000000000;
                    row["Feb_mv"] = 5354121923.5000000000;
                    row["Mar_mv"] = 5432895854.6000000000;
                    row["Apr_mv"] = 5491140264.3000000000;
                    row["May_mv"] = 5347885328.4000000000;
                    row["Jun_mv"] = 5430017096.8000000000;
                    row["Jul_mv"] = 5462607474.1000000000;
                    row["Aug_mv"] = 5526763015.5000000000;
                    row["Sep_mv"] = 5591491234.1000000000;
                    row["Oct_mv"] = 5596209869.7000000000;
                    row["Nov_mv"] = 5678151725.7000000000;
                    row["Dec_mv"] = 5723390524.8000000000;
                    row["Jan_src"] = "UVIMCO";
                    row["Feb_src"] = "UVIMCO";
                    row["Mar_src"] = "UVIMCO";
                    row["Apr_src"] = "UVIMCO";
                    row["May_src"] = "UVIMCO";
                    row["Jun_src"] = "UVIMCO";
                    row["Jul_src"] = "UVIMCO";
                    row["Aug_src"] = "UVIMCO";
                    row["Sep_src"] = "UVIMCO";
                    row["Oct_src"] = "UVIMCO";
                    row["Nov_src"] = "UVIMCO";
                    row["Dec_src"] = "UVIMCO";
                    row["CCYR"] = 0.146290411;
                    row["ACCYR"] = 0.146290411;
                    row["CCYR_count"] = 12;
                    row["CFYR"] = 0.0510923;
                    row["ACFYR"] = 0.0510923;
                    row["CFYR_count"] = 12;
                    row["bmk_uvimco_strategy"] = null;
                    row["bmk_msci_acwi"] = null;
                    dataTable.Rows.Add(row);
                    JArray dataString = ConvertDataTabletoJSON(dataTable);

                    component.Items = dataString;
                    break;
                case "111111-22222-44444":
                    component.ComponentMetaData = new ComponentMetaData()
                    {
                        GUID = "111111-22222-44444",
                        Name = "Pie Chart Component (111111-22222-44444)",
                        Description = "Pie Chart Component (111111-22222-44444) Description",
                        ComponentType = ComponentMetaData.ComponentTypeValue.Pie,
                        ComponentTitle = "Pie Chart Sector",
                        ComponentHeader = "Pie Chart Sector Header",
                        ComponentFootNote = "Pie Chart Sector Footnote",

                    };

                    component.ColumnMetaData = new ColumnMetaData()
                    {
                        AvailableColumns = new List<Column>()
                        {
                            new Column("Sector", "eg Tech", "sector", Column.DataTypeValue.String, false, false, true),
                            new Column("Percent", "eg 10", "percent", Column.DataTypeValue.Decimal, false, false, true),
                         },
                        SelectedColumns = new List<Column>()
                        {
                            new Column("Sector", "eg Tech", "sector", Column.DataTypeValue.String, false, false, true),
                            new Column("Percent", "eg 10", "percent", Column.DataTypeValue.Decimal, false, false, true),
                         },
                    };

                    dataSet = new DataSet();
                    dataTable = new DataTable();
                    dataTable.Columns.Add("sector", typeof(String));
                    dataTable.Columns.Add("percent", typeof(decimal));
                    dataTable.Rows.Add(new Object[]{"Tech", 3 });
                    dataTable.Rows.Add(new Object[] { "Medical", 1 });
                    dataTable.Rows.Add(new Object[] { "Aerospace", 2 });
                    dataTable.Rows.Add(new Object[] { "Banking", 1 });
                    dataTable.Rows.Add(new Object[] { "Misc", 2 });
                    component.Items = ConvertDataTabletoJSON(dataTable);
                    component.ChartOptions = new ComponentPieChartOptions()
                    { 
                        Title = "Holdings",
                        Is3D = true,
                        LegendAlignment = ComponentChartOptions.LegendAlignmentValue.Center,
                        LegendLocation = ComponentChartOptions.LegendLocationValue.Top,
                        LegendTextFontBold = true,
                        LegendTextFontItalic = true,
                        LegendTextColor = "red",
                        PieHole = 0.0M,
                        EnableInteractivity = true,
                        Width = 600,
                        Height = 400
                    };
                    component.TableOptions = new ComponentTableOptions();
                    break;

            }

            return componentDTOModel;
        }
        #endregion

        public JArray ConvertDataTabletoJSON(DataTable dt)
        {

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt); //.Replace("\"", "'");
            JArray obj = JArray.Parse(JSONString);
            return obj;
        }
    }
}
