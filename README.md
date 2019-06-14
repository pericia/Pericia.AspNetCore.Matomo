# Pericia.AspNetCore.Matomo

[![Build status](https://dev.azure.com/glacasa/GithubBuilds/_apis/build/status/Pericia.AspNetCore.Matomo-CI)](https://dev.azure.com/glacasa/GithubBuilds/_build/latest?definitionId=69)

ASP.NET Core Tag helper for Matomo analytics service

## Install

The library `Pericia.AspNetCore.Matomo` is available on Nuget : [![NuGet](https://img.shields.io/nuget/v/Pericia.AspNetCore.Matomo.svg)](https://www.nuget.org/packages/Pericia.AspNetCore.Matomo/)

You can install it with the following command line in your Package Manager Console :

	Install-Package Pericia.AspNetCore.Matomo

Or with dotnet core :

	 dotnet add package Pericia.AspNetCore.Matomo 
	 	 
## GDPR compliance

This library is built to help you to be compliant with the European General Data Protection Regulation (GDPR), and is based on the [CNIL recommendations](https://www.cnil.fr/fr/solutions-pour-les-cookies-de-mesure-daudience)

In order to be fully compliant, you need to configure Matomo to anonymize data, and mask at leat 2 bytes of the visitors' IP. You also have to let the user opt-out, by addind the opt-out iframe in your privacy page (see below).

If you don't do this, you must ask for user consent before to add the tracking tag.

## How to use

### Configuration

Add a "Matomo" section in your settings file :

    "Matomo": {
		"trackerUrl": "https://stats.example.com",
		"siteId": "1",
		"trackerOptions": {
			"disableCookieTimeoutExtension": true,
			"noScriptTracking": true,
			"prependDomainToTitle": false,
			"clientDoNotTrackDetection": true
		}
    }

Required parameters :

- `trackerUrl` :  the url of your matomo site
- `siteId` : the id of the website in matomo

Optional parameters :

- `trackerOptions:disableCookieTimeoutExtension` : limit the cookie duration to 13 months - required for GDPR compliance (defaults true)
- `trackerOptions:noScriptTracking` : tracks users with JavaScript disabled (defaults true)
- `trackerOptions:prependDomainToTitle` : prepend the site domain to the page title when tracking (defaults false)
- `trackerOptions:clientDoNotTrackDetection` : enable client side DoNotTrack detection (defaults true)

### Tag helpers

Reference the tag helpers :

    @addTagHelper *, Pericia.AspNetCore.Matomo

Add the tracking script in your page before `</head>`

    <matomo-tracking></matomo-tracking>

Add the opt-out iframe in your Privacy page

    <matomo-opt-out></matomo-opt-out>