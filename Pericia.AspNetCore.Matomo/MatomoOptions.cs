using System;
using System.Collections.Generic;
using System.Text;

namespace Pericia.AspNetCore.Matomo
{
    internal class MatomoOptions
    {
        public string TrackerUrl { get; set; } = string.Empty;

        public string SiteId { get; set; } = string.Empty;

        public MatomoTrackerOptions TrackerOptions { get; set; } = new MatomoTrackerOptions();
    }

    internal class MatomoTrackerOptions
    {
        public bool DisableCookieTimeoutExtension { get; set; } = true;

        public bool NoScriptTracking { get; set; } = true;
        
        public bool PrependDomainToTitle { get; set; } = false;

        public bool ClientDoNotTrackDetection { get; set; } = true;
    }
}
