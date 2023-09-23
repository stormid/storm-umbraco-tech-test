using Microsoft.AspNetCore.Razor.TagHelpers;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Storm.Core.TagHelpers
{
    [HtmlTargetElement("nullimage", TagStructure = TagStructure.WithoutEndTag)]
    public class ImageTagHelper : TagHelper
    {
        public IPublishedContent Content { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            if (Content != null)
            {
                output.Attributes.SetAttribute("src", Content.Url());
                output.Attributes.SetAttribute("alt", Content.Name);
            }
            else
            {
                output.Attributes.SetAttribute("src", "/assets/images/no-image.jpg");
                output.Attributes.SetAttribute("alt", "no-image");
            }
        }
    }
}
