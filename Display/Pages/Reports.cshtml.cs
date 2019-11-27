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

namespace Display.Pages
{
    public class ExplorerModel : BasePageModel
    {
        private readonly IPrismService _service;

        public ExplorerModel(ILogger<IndexModel> logger, IPrismService service) : base(logger)
        {
            _service = service;
        }

        [BindProperty]
        public string Authentication { get; set; }

        [BindProperty]
        public List<MenuGroupButton> MenuGroupButtons { get; set; }

        public string[] AuthMethods = new[] { "Authenticated", "Not Authenticated" };

        public void OnGet()
        {
            IsAuthenticated = Authentication != null && Authentication != "Not Authenticated";
            GenerateMenuButtons();
        }

        public void OnPost(string Authentication)
        {
            IsAuthenticated = Authentication != null && Authentication != "Not Authenticated";
            GenerateMenuButtons();
        }

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
                foreach(ReportDef def in grp.ReportDefs)
                {
                    btn.MenuButtons.Add(new MenuButton { ButtonText = def.Name, IsDisabled = IsAuthenticated, ToolTip = def.Description, Url = "https://gallery.shinyapps.io/051-movie-explorer", });
                }
                MenuGroupButtons.Add(btn);
            };
            //MenuButton btn = new MenuButton { ButtonText = grp.Name, IsDisabled = IsAuthenticated, ToolTip = grp.Description, Url = "https://gallery.shinyapps.io/051-movie-explorer", },
            //    new MenuButton { ButtonText = "Lego", IsDisabled = IsAuthenticated, ToolTip = "Runs the Lego sample located at gallery.shinyapps.io", Url = "https://gallery.shinyapps.io/lego-viz/", },
            //    new MenuButton { ButtonText = "k-means", IsDisabled = !IsAuthenticated, ToolTip = "Runs the K-means sample located at gallery.shinyapps.io", Url = "https://gallery.shinyapps.io/050-kmeans-example", },
        }
    }
}

