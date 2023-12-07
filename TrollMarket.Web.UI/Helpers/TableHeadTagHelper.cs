using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace TrollMarket.Web.UI.Helpers
{
    [HtmlTargetElement("thead")]
    public class TableHeadTagHelper : TagHelper
    {
        [HtmlAttributeName("columns")]
        public string[] Columns { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "thead";
            output.TagMode= TagMode.StartTagAndEndTag;
            var tableHeaders = new StringBuilder();
            foreach (var column in Columns)
            {
                tableHeaders.Append($"<th>{column}</th>");
            }
            output.Content.AppendHtml($"<tr>{tableHeaders}</tr>");
        }
    }
}
