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

namespace Display.Pages
{
    public class ComponentTableDataModel : BasePageModel
    {
        private readonly IPrismService _service;

        public ComponentTableDataModel(ILogger<IndexModel> logger, IPrismService service) : base(logger)
        {
            _service = service;
        }

        [BindProperty]
        public int ComponentID { get; set; }

        [BindProperty]
        public Component Component { get; set; }

        #region OnGetAsync
        /// <summary>
        /// Returns the Report Data View by URL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(string componentID)
        {
            ComponentID = int.Parse(componentID);
            return await Task.FromResult<IActionResult>(new JsonResult(new { success = true, statusMessage = "", html = this.RenderViewAsync("Reports/ComponentTableView").Result }));
        }
        #endregion

        #region OnGetComponentJSON
        public JsonResult OnGetComponentJSON(DataTableAjaxPostModel model, string componentID)
        {
            var cols = new List<DataTableAjaxColumn>();
            GetComponent(componentID);

            foreach (var s in Component.ColumnMetaData.SelectedColumns)
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
                recordsTotal = Component.TotalItems,
                recordsFiltered = Component.TotalItems,
                data = Component.Items,
                columns = JsonConvert.SerializeObject(cols)
            });
        }
        #endregion
        public void OnPost(string Authentication)
        {
        }

        private void GetComponent(string id)
        {
            ComponentDTOModel componentDTOModel = new ComponentDTOModel() 
            { 
                Item = new Component()
                {
                    ComponentGUID = id
                }
            };
            componentDTOModel = _service.GetComponent(componentDTOModel);
            if (componentDTOModel.HasError)
            { 
                this.ErrorMessage = componentDTOModel.ErrorMessage;
            }
            else
            {
                Component = componentDTOModel.Item;
            }
        }
    }
}

