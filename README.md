# Pericia.AspNetCore.Matomo

[![Build status](https://dev.azure.com/glacasa/GithubBuilds/_apis/build/status/Pericia.AspNetCore.Matomo-CI)](https://dev.azure.com/glacasa/GithubBuilds/_build/latest?definitionId=69)

ASP.NET Core Tag helper for Matomo analytics service

## Install

This lib will be available on Nuget

## How to use

### Configuration

    "Matomo": {
      "trackerUrl": "",
      "siteId": ""
    },

### Tag helpers

Reference the tag helpers :

    @addTagHelper *, Pericia.AspNetCore.Matomo

Add the tracking script before `</head>`

    <matomo-tracking></matomo-tracking>

Add the opt-out iframe in your Privacy page

    <matomo-opt-out></matomo-opt-out>