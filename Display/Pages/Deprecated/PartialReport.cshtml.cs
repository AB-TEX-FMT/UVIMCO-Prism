using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Display.ViewModels;
using DataModel.DTOModels;

namespace Display.Pages
{
    public class PartialReport : BasePageModel
    {
        public PartialReport(ILogger<IndexModel> logger) : base(logger)
        {
            
        }

        [BindProperty]
        public string Authentication { get; set; }

        [BindProperty]
        public ReportDTOModel ReportModel { get; set; }

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

