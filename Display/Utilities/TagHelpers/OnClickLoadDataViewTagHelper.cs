using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Display.Utilities.TagHelpers
{
    [HtmlTargetElement(Attributes = "onclickdataview-for")]
    public class OnClickDataViewTagHelper : TagHelper
    {
        public override int Order => 2;

        public string Action { get; set; }

        public string Route { get; set; }

        public string Div { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagHelperAttribute tag = new TagHelperAttribute("onclick", "loadDataView(`/" + Action + (Route != "" ? "/" + Route : "") + "`, `" + Div + "`)");
            output.Attributes.Remove(new TagHelperAttribute("onclickdataview-for"));
            output.Attributes.Add(tag);
        }
    }
}
