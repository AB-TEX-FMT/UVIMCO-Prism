using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Display.ViewModels;

namespace Display.Pages
{
    public class RelationalDataTableModel : BasePageModel
    {
        public RelationalDataTableModel(ILogger<IndexModel> logger) : base(logger)
        {
            
        }

        [BindProperty]
        public string Authentication { get; set; }

        [BindProperty]
        public List<MenuButton> MenuButtons { get; set; }

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
            MenuButtons = new List<MenuButton>()
            {
                new MenuButton { ButtonText = "Company", IsDisabled = IsAuthenticated, ToolTip = "Display a web page", Url = "https://google.com/", },
                //new MenuButton { ButtonText = "Relational Data Table", IsDisabled = IsAuthenticated, ToolTip = "Shows a Data Table", Url = "https://gallery.shinyapps.io/lego-viz/", },
                //new MenuButton { ButtonText = "k-means", IsDisabled = !IsAuthenticated, ToolTip = "Runs the K-means sample located at gallery.shinyapps.io", Url = "https://gallery.shinyapps.io/050-kmeans-example", },
            };
        }
    }
}

