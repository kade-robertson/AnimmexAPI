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

* .NET Framework 4.5
* ASP.NET Core 5.0
* Windows 8
* Windows Phone 8.1
* Xamarin.Android
* Xamarin.iOS
* Xamarin.iOS (Classic)

Documentation
=============

All of the functions and classes have XML documentation supported by Xamarin,
although a wiki with more information will be available in the future.

Example Usage
=============

As of the most recent build, there is a whole 1 function available for usage!
Here’s an example. You might want to look at the structure of AnimmexVideo
object to see what you can do.

    var api = new AnimmexAPI.AnimmexAPI();
    var videos = api.GetRecentlyViewed();
    foreach (AnimmexAPI.AnimmexVideo video in videos) {
        MessageBox.Show(video.Title);
    }

Notes
=====

This project is simply as an experiment for myself, expect this code not to
necessarily be held to production standards or the like. I will however mark
this under the MIT License so it is available to be used freely.
