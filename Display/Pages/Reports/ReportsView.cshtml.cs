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

namespace Display.Pages
{
    public class ReportsModel : BasePageModel
    {
        private readonly IPrismService _service;

        public ReportsModel(ILogger<IndexModel> logger, IPrismService service) : base(logger)
        {
            _service = service;
        }

        [BindProperty]
        public string Authentication { get; set; }

        [BindProperty]
        public List<MenuGroupButton> MenuGroupButtons { get; set; }

        public string[] AuthMethods = new[] { "Authenticated", "Not Authenticated" };

        #region OnGetAsync
        /// <summary>
        /// Returns the Report View
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            IsAuthenticated = Authentication != null && Authentication != "Not Authenticated";
            GenerateMenuButtons();
            return await Task.FromResult<IActionResult>(new JsonResult(new { success = true, statusMessage = "", html = this.RenderViewAsync("Reports/ReportsView").Result }));
        }
        #endregion

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
                if (grp.ReportDefs != null)
                    {
                    foreach (ReportDef def in grp.ReportDefs)
                    {
                        btn.MenuButtons.Add(new MenuButton { ButtonText = def.Name, IsDisabled = IsAuthenticated, ToolTip = def.Description, ReportID = def.ID });
                    }
                }
                MenuGroupButtons.Add(btn);
            };
        }
    }
}

