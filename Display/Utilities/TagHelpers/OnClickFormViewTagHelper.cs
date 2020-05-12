using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Display.Utilities.TagHelpers
{
    [HtmlTargetElement(Attributes = "onclickformView-for")]
    public class OnClickFormViewTagHelper : TagHelper
    {
        public override int Order => 2;

        public string Action { get; set; }

        public string Route { get; set; }

        public string Div { get; set; }

        public string RequestType { get; set; }

        public string FormName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagHelperAttribute tag = new TagHelperAttribute("onclick", "loadFormView(`/" + Action + (Route != "" ? "/" + Route : "") + "`, `" + Div + "`, `" + RequestType + "`, `" + FormName + "`)");
            output.Attributes.Remove(new TagHelperAttribute("onclickformview-for"));
            output.Attributes.Add(tag);
        }
    }
}
