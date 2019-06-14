using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pericia.AspNetCore.Matomo
{
    public class MatomoTrackingTagHelper : TagHelper
    {
        private readonly MatomoOptions options;

        public MatomoTrackingTagHelper(IConfiguration configuration)
        {
            var options = new MatomoOptions();
            configuration.GetSection("Matomo").Bind(options);
            this.options = options;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var trackerUrl = options.TrackerUrl;
            var siteId = options.SiteId;
            if (string.IsNullOrEmpty(trackerUrl) || string.IsNullOrEmpty(siteId))
            {
                return;
            }

            if (!trackerUrl.EndsWith("/"))
            {
                trackerUrl += "/";
            }

            output.TagName = "script";
            output.Attributes.SetAttribute("type", "text/javascript");

            var scriptContent = new StringBuilder();

            scriptContent.AppendLine("");
            scriptContent.AppendLine("  var _paq = window._paq || [];");

            if (options.TrackerOptions.DisableCookieTimeoutExtension)
            {
                scriptContent.AppendLine("  _paq.push([function() {");
                scriptContent.AppendLine("    var self = this;");
                scriptContent.AppendLine("    function getOriginalVisitorCookieTimeout() {");
                scriptContent.AppendLine("      var now = new Date(),nowTs = Math.round(now.getTime() / 1000),visitorInfo = self.getVisitorInfo();");
                scriptContent.AppendLine("      var createTs = parseInt(visitorInfo[2]);");
                scriptContent.AppendLine("      var cookieTimeout = 33696000;");
                scriptContent.AppendLine("      var originalTimeout = createTs + cookieTimeout - nowTs;");
                scriptContent.AppendLine("      return originalTimeout;");
                scriptContent.AppendLine("    }");
                scriptContent.AppendLine("    this.setVisitorCookieTimeout( getOriginalVisitorCookieTimeout() );");
                scriptContent.AppendLine("  }]);");
            }

            if (options.TrackerOptions.PrependDomainToTitle)
            {
                scriptContent.AppendLine("  _paq.push(['setDocumentTitle', document.domain + '/' + document.title]);");
            }

            if (options.TrackerOptions.ClientDoNotTrackDetection)
            {
                scriptContent.AppendLine("  _paq.push(['setDoNotTrack', true]);");
            }

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

            if (options.TrackerOptions.NoScriptTracking)
            {
                output.PostElement.AppendHtml($"<noscript><p><img src=\"{trackerUrl}matomo.php?idsite={siteId}&amp;rec=1\" style=\"border:0;\" alt=\"\" /></p></noscript>");
            }
        }
    }
}
