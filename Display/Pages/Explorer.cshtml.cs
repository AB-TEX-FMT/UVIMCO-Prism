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
    public class ExplorerModel : BasePageModel
    {
        public ExplorerModel(ILogger<IndexModel> logger) : base(logger)
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
                new MenuButton { ButtonText = "Movie Explorer", IsDisabled = IsAuthenticated, ToolTip = "Runs the Movie Explorer sample located at gallery.shinyapps.io", Url = "https://gallery.shinyapps.io/051-movie-explorer", },
                new MenuButton { ButtonText = "Lego", IsDisabled = IsAuthenticated, ToolTip = "Runs the Lego sample located at gallery.shinyapps.io", Url = "https://gallery.shinyapps.io/lego-viz/", },
                new MenuButton { ButtonText = "k-means", IsDisabled = !IsAuthenticated, ToolTip = "Runs the K-means sample located at gallery.shinyapps.io", Url = "https://gallery.shinyapps.io/050-kmeans-example", },
            };
        }
    }
}

