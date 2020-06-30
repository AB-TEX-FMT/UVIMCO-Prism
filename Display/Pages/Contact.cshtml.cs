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
    public class ContactModel : BasePageModel
    {
        public ContactModel(ILogger<ContactModel> logger) : base(logger)
        {
            
        }

        public void OnGet()
        {
        }

        public void OnPost(string Authentication)
        {
        }
    }
}
