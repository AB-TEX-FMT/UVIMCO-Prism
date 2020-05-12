using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Display.ViewModels;
using DataService.Services;
using DataModel.DTOModels;
using DataModel.Shared;
using Display.Utilities;
using Newtonsoft.Json;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Display.Pages
{
    public class ReportsDataModel : BasePageModel
    {
        private readonly IPrismService _service;

        public ReportsDataModel(ILogger<IndexModel> logger, IPrismService service) : base(logger)
        {
            _service = service;
        }

        [BindProperty]
        public string Authentication { get; set; }

        [BindProperty]
        public List<MenuGroupButton> MenuGroupButtons { get; set; }

        [BindProperty]
        public ReportDTOModel Report { get; set; }

        [BindProperty]
        public List<ComponentMetaData> Components { get; set; }

        public string[] AuthMethods = new[] { "Authenticated", "Not Authenticated" };

        #region OnGetAsync
        /// <summary>
        /// Returns the Report Data View by URL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(string reportID)
        {
            IsAuthenticated = Authentication != null && Authentication != "Not Authenticated";
            GetReport(reportID);
            GetComponents(reportID);
            if (Report.ReportMetaData.RenderNatively)
            {
                GenerateMenuButtons();
                return await Task.FromResult<IActionResult>(new JsonResult(new { success = true, statusMessage = "", html = this.RenderViewAsync("Reports/ReportNativeView").Result }));
            }
            else
            {
                return await Task.FromResult<IActionResult>(new JsonResult(new { success = true, statusMessage = "", html = this.RenderViewAsync("Reports/ReportIFrameView").Result }));
            }
        }
        #endregion

        //#region OnGetComponentJSON
        //public JsonResult OnGetComponentJSON(DataTableAjaxPostModel model, string componentID)
        //{
        //    var cols = new List<DataTableAjaxColumn>();
        //    GetReport(componentID);

        //    foreach (var s in Report.ColumnMetaData.SelectedColumns)
        //    {
        //        // simple remapping adding extra info to found dataset
        //        cols.Add(new DataTableAjaxColumn()
        //        {
        //            data = s.ColName,
        //            name = s.DisplayName,
        //            orderable = s.Orderable,
        //            isvisible = s.IsVisible,
        //        });
        //        switch (s.DataType)
        //        {
        //            case Column.DataTypeValue.Currency:
        //                cols.Last().format = "Currency";
        //                break;
        //            case Column.DataTypeValue.Decimal:
        //                cols.Last().format = "Decimal";
        //                break;
        //            case Column.DataTypeValue.Int:
        //                cols.Last().format = "Int";
        //                break;
        //            case Column.DataTypeValue.String:
        //                cols.Last().format = "String";
        //                break;
        //            case Column.DataTypeValue.Bool:
        //                cols.Last().format = "Bool";
        //                break;
        //            case Column.DataTypeValue.Date:
        //                cols.Last().format = "Date";
        //                break;
        //            default:
        //                cols.Last().format = "";
        //                break;
        //        }
        //    };
        //    //DataRow[] x = Report.Items.Tables[0].Select();
        //    ////DataTable[] z = x.AsQueryable().Select(y => y.Table);
        //    //string[] array = Report.Items.Tables[0].Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
        //    //string JSONString1 = JsonConvert.SerializeObject(x.AsQueryable().Select(y => y.Table));
        //    //string JSONString3 = JsonConvert.SerializeObject(x.AsQueryable().Select(y => y.ItemArray));
        //    //string JSONString2 = JSONString1.Substring(1,JSONString1.Length - 2);
        //    //var rows = Report.Items.Tables[0].AsEnumerable().Select(r => string.Format("[|{0}|]", string.Join("|,|", r.ItemArray)));

        //    //var output = string.Format("[{0}]", string.Join(",", rows.ToArray())).Replace('|', '"');
        //    //string JSONString = JsonConvert.SerializeObject(output);
        //    return new JsonResult(new
        //    {
        //        success = true,
        //        message = "Retrieved Successfully",
        //        // this is what datatables wants sending back
        //        draw = model.draw,
        //        recordsTotal = Report.TotalItems,
        //        recordsFiltered = Report.TotalItems,
        //        data = Report.Items,
        //        columns = JsonConvert.SerializeObject(cols)
        //    });
        //}
        //#endregion
        public void OnPost(string Authentication)
        {
            IsAuthenticated = Authentication != null && Authentication != "Not Authenticated";
            GenerateMenuButtons();
        }

        /// <summary>
        /// Generate the MenuButtons collection
        /// </summary>
        private void GenerateMenuButtons()
        {
            // Retrieve the Report group DTO from the service
            ReportGroupListDTOModel dto = _service.GetReportGroups();

            // Check for errors
            if (dto.HasError)
            {
                ErrorMessage = dto.ErrorMessage;
                return;
            }

            MenuGroupButtons = new List<MenuGroupButton>();
            // Add each menu button from the dto to the group
            foreach (ReportGroup grp in dto.Items)
            {
                MenuGroupButton btn = new MenuGroupButton { ButtonText = grp.Name, IsDisabled = IsAuthenticated, ToolTip = grp.Description };
                btn.MenuButtons = new List<MenuButton>();
                foreach (ReportDef def in grp.ReportDefs)
                {
                    btn.MenuButtons.Add(new MenuButton { ButtonText = def.Name, IsDisabled = IsAuthenticated, ToolTip = def.Description});
                }
                MenuGroupButtons.Add(btn);
            };
        }

        private void GetReport(string id)
        {
            Report = new ReportDTOModel() { ReportDefID = id};
            Report = _service.GetReport(Report);
            if (Report.HasError)
            { 
                this.ErrorMessage = Report.ErrorMessage;
            }
        }

        private void GetComponents(string id)
        {
            ComponentsDTOModel dto = new ComponentsDTOModel() { ReportDefID = id };
            dto = _service.GetComponents(dto);
            if (dto.HasError)
            {
                this.ErrorMessage = Report.ErrorMessage;
            }
            else
            {
                Components = dto.Items;
            }
        }

        #region OnGetTableComponentJSON
        public JsonResult OnGetTableComponentJSON(DataTableAjaxPostModel model, string reportID)
        {
            var cols = new List<DataTableAjaxColumn>();
            Component component = GetComponent(reportID);
            if (component is null)
            {
                //Prob need to throw an error message here
                return null;
            }

            foreach (var s in component.ColumnMetaData.SelectedColumns)
            {
                // simple remapping adding extra info to found dataset
                cols.Add(new DataTableAjaxColumn()
                {
                    data = s.ColName,
                    name = s.DisplayName,
                    orderable = s.Orderable,
                    isvisible = s.IsVisible,
                });
                switch (s.DataType)
                {
                    case Column.DataTypeValue.Currency:
                        cols.Last().format = "Currency";
                        break;
                    case Column.DataTypeValue.Decimal:
                        cols.Last().format = "Decimal";
                        break;
                    case Column.DataTypeValue.Int:
                        cols.Last().format = "Int";
                        break;
                    case Column.DataTypeValue.String:
                        cols.Last().format = "String";
                        break;
                    case Column.DataTypeValue.Bool:
                        cols.Last().format = "Bool";
                        break;
                    case Column.DataTypeValue.Date:
                        cols.Last().format = "Date";
                        break;
                    default:
                        cols.Last().format = "";
                        break;
                }
            };
            //DataRow[] x = Report.Items.Tables[0].Select();
            ////DataTable[] z = x.AsQueryable().Select(y => y.Table);
            //string[] array = Report.Items.Tables[0].Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            //string JSONString1 = JsonConvert.SerializeObject(x.AsQueryable().Select(y => y.Table));
            //string JSONString3 = JsonConvert.SerializeObject(x.AsQueryable().Select(y => y.ItemArray));
            //string JSONString2 = JSONString1.Substring(1,JSONString1.Length - 2);
            //var rows = Report.Items.Tables[0].AsEnumerable().Select(r => string.Format("[|{0}|]", string.Join("|,|", r.ItemArray)));

            //var output = string.Format("[{0}]", string.Join(",", rows.ToArray())).Replace('|', '"');
            //string JSONString = JsonConvert.SerializeObject(output);
            return new JsonResult(new
            {
                success = true,
                message = "Retrieved Successfully",
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = component.TotalItems,
                recordsFiltered = component.TotalItems,
                data = component.Items,
                columns = JsonConvert.SerializeObject(cols)
            });
        }
        #endregion

        #region OnGetChartComponentJSON
        public JsonResult OnGetChartComponentJSON(string reportID)
        {
            var cols = new List<DataTableAjaxColumn>();
            Component component = GetComponent(reportID);
            if (component is null)
            {
                //Prob need to throw an error message here
                return null;
            }

            foreach (var s in component.ColumnMetaData.SelectedColumns)
            {
                // simple remapping adding extra info to found dataset
                cols.Add(new DataTableAjaxColumn()
                {
                    data = s.ColName,
                    name = s.DisplayName,
                    orderable = s.Orderable,
                    isvisible = s.IsVisible,
                });
                switch (s.DataType)
                {
                    case Column.DataTypeValue.Currency:
                        cols.Last().format = "Currency";
                        break;
                    case Column.DataTypeValue.Decimal:
                        cols.Last().format = "Decimal";
                        break;
                    case Column.DataTypeValue.Int:
                        cols.Last().format = "Int";
                        break;
                    case Column.DataTypeValue.String:
                        cols.Last().format = "String";
                        break;
                    case Column.DataTypeValue.Bool:
                        cols.Last().format = "Bool";
                        break;
                    case Column.DataTypeValue.Date:
                        cols.Last().format = "Date";
                        break;
                    default:
                        cols.Last().format = "";
                        break;
                }
            };
            //DataRow[] x = Report.Items.Tables[0].Select();
            ////DataTable[] z = x.AsQueryable().Select(y => y.Table);
            //string[] array = Report.Items.Tables[0].Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            //string JSONString1 = JsonConvert.SerializeObject(x.AsQueryable().Select(y => y.Table));
            //string JSONString3 = JsonConvert.SerializeObject(x.AsQueryable().Select(y => y.ItemArray));
            //string JSONString2 = JSONString1.Substring(1,JSONString1.Length - 2);
            //var rows = Report.Items.Tables[0].AsEnumerable().Select(r => string.Format("[|{0}|]", string.Join("|,|", r.ItemArray)));

            //var output = string.Format("[{0}]", string.Join(",", rows.ToArray())).Replace('|', '"');
            //string JSONString = JsonConvert.SerializeObject(output);
            return new JsonResult(new
            {
                success = true,
                message = "Retrieved Successfully",
                // this is what datatables wants sending back
                recordsTotal = component.TotalItems,
                recordsFiltered = component.TotalItems,
                data = component.Items,
                columns = JsonConvert.SerializeObject(cols)
            });
        }
        #endregion

        #region GetComponent
        private Component GetComponent(string id)
        {
            ComponentDTOModel componentDTOModel = new ComponentDTOModel()
            {
                Item = new Component()
                {
                    ComponentID = id
                }
            };
            componentDTOModel = _service.GetComponent(componentDTOModel);
            if (componentDTOModel.HasError)
            {
                this.ErrorMessage = componentDTOModel.ErrorMessage;
                return null;
            }
            else
            {
                return componentDTOModel.Item;
            }
        }
        #endregion
    }
}

