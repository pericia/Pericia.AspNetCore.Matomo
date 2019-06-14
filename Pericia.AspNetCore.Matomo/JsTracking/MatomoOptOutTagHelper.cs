using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pericia.AspNetCore.Matomo
{
    public class MatomoOptOutTagHelper : TagHelper
    {
        private readonly MatomoOptions options;

        public MatomoOptOutTagHelper(IConfiguration configuration)
        {
            var options = new MatomoOptions();
            configuration.GetSection("Matomo").Bind(options);
            this.options = options;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var trackerUrl = options.TrackerUrl;
            if (options.TrackerUrl == null)
            {
                return;
            }

            output.TagName = "iframe";
            output.Attributes.SetAttribute("style", "border: 0; height: 200px; width: 600px;");

            var path = new StringBuilder();
            path.Append(trackerUrl);
            path.Append("index.php?module=CoreAdminHome&action=optOut");

            path.Append("&language=");
            path.Append(options.OptoutOptions.Language);

            //TODO : configuration
            path.Append("&backgroundColor=");
            path.Append("&fontColor=");
            path.Append("&fontSize=");
            path.Append("&fontFamily=");

            var src = new Uri(trackerUrl, path.ToString());

            output.Attributes.SetAttribute("src", src);

        }
    }
}
