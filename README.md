AnimmexAPI
==========
[![GitHub stars](https://img.shields.io/github/stars/kade-robertson/AnimmexAPI.svg)](https://github.com/kade-robertson/AnimmexAPI/stargazers) [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/kade-robertson/AnimmexAPI/master/LICENSE.md)

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

All of the functions and classes have XML documentation supported by Xamarin,
although a wiki with more information will be available in the future.

Example Usage
=============

There are quite a few functions that have been built in now, mostly catering to
the video functionality of the website. The best way right now to use them is to
browse the source until I have documentation set up.

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
