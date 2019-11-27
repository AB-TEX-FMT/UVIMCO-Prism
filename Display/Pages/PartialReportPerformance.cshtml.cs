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
    public class PartialReportPerformance : BasePageModel
    {
        public PartialReportPerformance(ILogger<IndexModel> logger) : base(logger)
        {
            
        }

        [BindProperty]
        public string Authentication { get; set; }

        public void OnGet()
        {
            IsAuthenticated = Authentication != null && Authentication != "Not Authenticated";
        }

        public void OnPost(string Authentication)
        {
            IsAuthenticated = Authentication != null && Authentication != "Not Authenticated";
        }
    }
}

