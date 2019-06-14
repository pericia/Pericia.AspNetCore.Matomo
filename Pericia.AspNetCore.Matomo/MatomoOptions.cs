using System;
using System.Collections.Generic;
using System.Text;

namespace Pericia.AspNetCore.Matomo
{
    internal class MatomoOptions
    {
        public Uri TrackerUrl { get; set; }

        public int SiteId { get; set; }

        public MatomoTrackerOptions TrackerOptions { get; set; } = new MatomoTrackerOptions();

        public MatomoOptoutOptions OptoutOptions { get; set; } = new MatomoOptoutOptions();
    }

    internal class MatomoTrackerOptions
    {
        public bool DisableCookieTimeoutExtension { get; set; } = true;

        public bool NoScriptTracking { get; set; } = true;
        
        public bool PrependDomainToTitle { get; set; } = false;

        public bool ClientDoNotTrackDetection { get; set; } = true;
    }

    internal class MatomoOptoutOptions
    {
        public string Language { get; set; } = "en";
    }
}
