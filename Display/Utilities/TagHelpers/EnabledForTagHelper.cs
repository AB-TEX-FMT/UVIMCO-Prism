using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Display.Utilities.TagHelpers
{
    [HtmlTargetElement("input", Attributes = "enabled-for")]
    public class DisabledTagHelper : InputTagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DisabledTagHelper(IHttpContextAccessor httpContextAccessor, IHtmlGenerator generator) : base(generator)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            // Disable the field
            output.Attributes.Remove(new TagHelperAttribute("enabled-for"));
            output.Attributes.Add(new TagHelperAttribute("disabled"));

            // Now get the roles (if any) for which to enable it
            string roles = context.AllAttributes.First(y => y.Name == "enabled-for").Value.ToString();
            
            if (roles == "")
            {
                // If we don't have a value, it should be disable for all roles
                return;
            }
            else
            {
                // If we do have a value, it should be enabled for specific roles
                // Split the value into a string array of role names
                string[] rolesArray = roles.Split(":");
                // Create an action delegate to be performed on all the array members in rolesArray
                Action<string> action = new Action<string>(RemoveDisabled);
                // Now check each of the roleNames in the array
                Array.ForEach(rolesArray, action);
            }

            void RemoveDisabled(string roleName)
            {
                // Check if we have a disabled attribute
                if (output.Attributes.First(y => y.Name == "disabled") != null)
                {
                    // Check if the user is in the specified role
                    if (_httpContextAccessor.HttpContext.User.IsInRole(roleName.ToUpper()))
                    {
                        // If so, remove the disabled attribute
                        output.Attributes.Remove(new TagHelperAttribute("disabled"));
                    }
                }
            }
        }
    }
}