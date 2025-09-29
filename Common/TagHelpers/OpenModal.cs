using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreProject.Common.TagHelpers
{
    public class OpenModal : TagHelper
    {
        public string Tag { get; set; } = "button";
        public string ModalSize { get; set; } = "lg";
        public string Method { get; set; } = "get";
        public string Url { get; set; }
        public string Title { get; set; }
        public string Class { get; set; } = "btn btn-info";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = Tag;
            output.Attributes.Add("onclick", $"OpenModal('{Url}', 'default_Modal', '{Title}', '{ModalSize}', '{Method}')");
            output.Attributes.Add("class", Class);
            base.Process(context, output);
        }
    }
}
