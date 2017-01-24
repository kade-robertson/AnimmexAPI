----

# AnimmexAPI

[![GitHub stars](https://img.shields.io/github/stars/kade-robertson/AnimmexAPI.svg)](https://github.com/kade-robertson/AnimmexAPI/stargazers) [![Build Status](https://travis-ci.org/kade-robertson/AnimmexAPI.svg?branch=master)](https://travis-ci.org/kade-robertson/AnimmexAPI/builds#) [![GitHub license](https://img.shields.io/badge/license-MIT-orange.svg)](https://raw.githubusercontent.com/kade-robertson/AnimmexAPI/master/LICENSE.md)

Animmex is a not-so-popular website hosted in Russia that provides a platforrm
to update and watch videos of all kinds. However, it is ad-riddled and you are
forced to use it through the browser. I am making this API simply as an
experiment and to increase the platforms on which the site can be used.

The API has been built in VS as a portable class library that is compatible with
applications on iOS, Android and Windows, and with Xamarin. The actual supported
environments are as follows:

-   .NET Framework 4.5

-   ASP.NET Core 5.0

-   Windows 8

-   Windows Phone 8.1

-   Xamarin.Android

-   Xamarin.iOS

-   Xamarin.iOS (Classic)

Documentation
=============

All of the functions and classes have XML documentation supported by Xamarin.
You can also find documentation on the project [wiki page](https://github.com/kade-robertson/AnimmexAPI/wiki).

Installation
=============

You have a couple of options for how to use this code:

1. You can clone this repository, and build it either through Visual Studio or Mono. This will give you the latest features and fixes before they make it into official releases.
2. You can check the Releases tab of this repository. Every time I feel that there have been enough changes to warrant a new release, this will be updated. This is convenient for those who won't need new features as they come along (they are happy with the library as-is), but it means you will have to download new versions as they are released. Alternatively:
3. You can download the package straight from NuGet! This can be done manually through the website [here](https://www.nuget.org/packages/AnimmexAPI/), or by adding it to your project either through the Package Manager, or through the Package Manager Console using the command `Install-Package AnimmexAPI`

Example Usage
=============

There are quite a few functions that have been built in now, mostly catering to
the video functionality of the website. The best way right now to use them is to
browse the documentation. However, sometimes features are added to the current
code and not immediately added to the wiki, so if something is missing check the
code.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
var api = new AnimmexClient();
var videos1 = await api.GetRecentlyViewed();

foreach (AnimmexVideo video in videos1) {
    MessageBox.Show($"Title: {video.Title}" + Environment.NewLine +
                    $"Views: {video.Views}" + Environment.NewLine +
                    $"Length: {video.Duration}" + Environment.NewLine +
                    $"Rating: {video.Rating}%");
}

var video_to_watch = await api.ImFeelingLucky("fellowship of the ring");
var direct_links = await api.GetDirectVideoLinks(video_to_watch);

MessageBox.Show(direct_links.BestQualityStream);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Notes
=====

This project is simply as an experiment for myself, expect this code not to
necessarily be held to production standards or the like. I will however mark
this under the MIT License so it is available to be used freely.

This project now makes use of the ModernHTTPClient NuGet package to solve security
issues found on Android devices. This update was included in the latest build of
Animmex-Video. 
