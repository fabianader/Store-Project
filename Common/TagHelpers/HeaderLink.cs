using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreProject.Common.TagHelpers
{
    public class HeaderLink : TagHelper
    {
        public string Url { get; set; }
        public string Level { get; set; } = "4"; // default <h4>

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tag = $"h{Level}";
            output.TagName = tag;

            // Make it behave like a link
            output.Attributes.SetAttribute("role", "link");
            output.Attributes.SetAttribute("tabindex", "0");
            output.Attributes.SetAttribute("onclick", $"location.href='{Url}'");
            output.Attributes.SetAttribute("style", "cursor:pointer;");
        }
    }
}
