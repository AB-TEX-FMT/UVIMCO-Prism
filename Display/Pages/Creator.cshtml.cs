using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Display.Pages;
using Display.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Display.Pages
{
    public class CreatorModel : BasePageModel
    {
        public CreatorModel(ILogger<CreatorModel> logger) : base(logger)
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
                new MenuButton { ButtonText = "Customer", IsDisabled = IsAuthenticated, ToolTip = "Create a new Customer", },
                new MenuButton { ButtonText = "Asset", IsDisabled = IsAuthenticated, ToolTip = "Log an incoming asset", },
                new MenuButton { ButtonText = "Liability", IsDisabled = !IsAuthenticated, ToolTip = "Log an out going liability", },
            };
        }
    }
}

