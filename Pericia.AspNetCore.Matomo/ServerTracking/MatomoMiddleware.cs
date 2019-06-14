using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Pericia.AspNetCore.Matomo.ServerTracking
{
    public class MatomoMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly MatomoOptions options;

        public MatomoMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.next = next;
            this.httpClientFactory = httpClientFactory;

            var options = new MatomoOptions();
            configuration.GetSection("Matomo").Bind(options);
            this.options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _ = Track(context);

            // Call the next delegate/middleware in the pipeline
            await next(context);
        }

        private async Task Track(HttpContext context)
        {
            var request = context.Request;

            var client = httpClientFactory.CreateClient(nameof(MatomoMiddleware));

            var urlParams = $"piwik.php?idsite={options.SiteId}&rec=1&token_auth={options.TokenAuth}";

            urlParams += "&url=" + HttpUtility.UrlEncode(UriHelper.GetDisplayUrl(request));
            urlParams += "&cip=" + context.Connection.RemoteIpAddress;

            var url = new Uri(options.TrackerUrl, urlParams);

            await client.GetAsync(url);

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
