using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pericia.AspNetCore.Matomo
{
    public class MatomoTrackingTagHelper : TagHelper
    {
        private readonly IConfigurationSection configuration;

        public MatomoTrackingTagHelper(IConfiguration configuration)
        {
            this.configuration = configuration.GetSection("Matomo");
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var trackerUrl = configuration["trackerUrl"];
            var siteId = configuration["siteId"];
            if (string.IsNullOrEmpty(trackerUrl)|| string.IsNullOrEmpty(siteId))
            {
                return;
            }

            output.TagName = "script";
            output.Attributes.SetAttribute("type", "text/javascript");

            var scriptContent = new StringBuilder();

            scriptContent.AppendLine("");
            scriptContent.AppendLine("  var _paq = window._paq || [];");

            // Non prorogation des cookies
            // https://www.cnil.fr/sites/default/files/typo/document/Configuration_piwik.pdf
            scriptContent.AppendLine("  _paq.push([function() {");
            scriptContent.AppendLine("    var self = this;");
            scriptContent.AppendLine("    function getOriginalVisitorCookieTimeout() {");
            scriptContent.AppendLine("      var now = new Date(),nowTs = Math.round(now.getTime() / 1000),visitorInfo = self.getVisitorInfo();");
            scriptContent.AppendLine("      var createTs = parseInt(visitorInfo[2]);");
            scriptContent.AppendLine("      var cookieTimeout = 33696000; // 13 mois en secondes");
            scriptContent.AppendLine("      var originalTimeout = createTs + cookieTimeout - nowTs;");
            scriptContent.AppendLine("      return originalTimeout;");
            scriptContent.AppendLine("    }");
            scriptContent.AppendLine("    this.setVisitorCookieTimeout( getOriginalVisitorCookieTimeout() );");
            scriptContent.AppendLine("  }]);");
            
            scriptContent.AppendLine("  _paq.push(['trackPageView']);");
            scriptContent.AppendLine("  _paq.push(['enableLinkTracking']);");

            scriptContent.AppendLine("  (function() {");
            scriptContent.AppendLine($"    var u = '{trackerUrl}';");
            scriptContent.AppendLine("    _paq.push(['setTrackerUrl', u + 'matomo.php']);");
            scriptContent.AppendLine($"    _paq.push(['setSiteId', '{siteId}']);");
            scriptContent.AppendLine("    var d = document, g = d.createElement('script'), s = d.getElementsByTagName('script')[0];");
            scriptContent.AppendLine("    g.type = 'text/javascript'; g.async = true; g.defer = true; g.src = u + 'matomo.js'; s.parentNode.insertBefore(g, s);");
            scriptContent.AppendLine("  })();");

            output.Content.SetHtmlContent(scriptContent.ToString());
        }
    }
}
