using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace TrollMarket.Web.UI.Helpers
{
    [HtmlTargetElement("pagination")]
    public class TableFootTagHelper : TagHelper
    {
        [HtmlAttributeName("page")]
        public int Page { get; set; }
        [HtmlAttributeName("total-pages")]
        public int TotalPages { get; set; }
        [HtmlAttributeName("parameters")]
        public object Parameters { get; set; }
        [HtmlAttributeName("controller")]
        public string Controller { get; set; }
        [HtmlAttributeName("action")]
        public string Action { get; set; }
        [HtmlAttributeName("id")]
        public string Id { get; set; }
        [HtmlAttributeName("colspan")]
        public int Colspan { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "tfoot";
            output.TagMode = TagMode.StartTagAndEndTag;
            var content = GetPaginationDOM();
            output.Content.AppendHtml(content);
        }

        public string GetPaginationDOM()
        {
            var output = $@"<tr><td colspan=""{this.Colspan}""><div class=""pagination"">
                <div>Page {this.Page} of {this.TotalPages}</div>
                <div>{GetPageButtons()}</div>
            </div></td></tr>";
            return output;
        }

        public string GetPageButtons()
        {
            var pageButtons = new StringBuilder();
            for (int index = 1; index <= this.TotalPages; index++)
            {
                pageButtons.Append($"<a href=\"{GetActionLink(index)}\">{index}</a> ");
            }
            return pageButtons.ToString();
        }

        public string GetActionLink(int pageNumber)
        {
            var controllerPath = (this.Controller != null) ? $"/{this.Controller}" : "/";
            var actionPath = (this.Action != null) ? $"/{this.Action}" : "";
            var idPath = (this.Id != null) ? $"/{this.Id}" : "";
            var pagePath = $"?page={pageNumber}";
            var parametersPath = GetParametersPath();
            var actionLink = $"{controllerPath}{actionPath}{idPath}{pagePath}{parametersPath}";
            return actionLink;
        }

        public string GetParametersPath()
        {
            var parametersPath = "";
            if (this.Parameters != null)
            {
                var properties = this.Parameters.GetType().GetProperties();
                var parametersBuilder = new StringBuilder();
                foreach (var property in properties)
                {
                    var name = property.Name;
                    var value = property.GetValue(this.Parameters);
                    parametersBuilder.Append($"&{name}={value}");
                }
                parametersPath = parametersBuilder.ToString();
            }
            return parametersPath;
        }
    }
}
