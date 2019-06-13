using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pericia.AspNetCore.Matomo
{
    public class MatomoOptOutTagHelper : TagHelper
    {
        private readonly IConfigurationSection configuration;

        public MatomoOptOutTagHelper(IConfiguration configuration)
        {
            this.configuration = configuration.GetSection("Matomo");
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var trackerUrl = configuration["trackerUrl"];
            if (string.IsNullOrEmpty(trackerUrl))
            {
                return;
            }

            if (!trackerUrl.EndsWith("/"))
            {
                trackerUrl += "/";
            }

            output.TagName = "iframe";
            output.Attributes.SetAttribute("style", "border: 0; height: 200px; width: 600px;");

            var src = new StringBuilder();
            src.Append(trackerUrl);
            src.Append("index.php?module=CoreAdminHome&action=optOut");

            //TODO : configuration
            src.Append("&language=en");            
            src.Append("&backgroundColor=");
            src.Append("&fontColor=");
            src.Append("&fontSize=");
            src.Append("&fontFamily=");

            output.Attributes.SetAttribute("src", src.ToString());      
  
        }
    }
}
