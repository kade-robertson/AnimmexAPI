﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    /// <summary>
    /// Enumeration of the different available video qualities.
    /// </summary>
    public enum VideoQuality {
        /// <summary>
        /// Standard definition (480p and below).
        /// </summary>
        SD,
        /// <summary>
        /// High definition (720p).
        /// </summary>
        HD,
        /// <summary>
        /// Full high definition (1080p).
        /// </summary>
        FullHD,
        /// <summary>
        /// Quad high definition (1440p).
        /// </summary>
        QuadHD,
        /// <summary>
        /// Ultra high definition (2160p).
        /// </summary>
        UHD,
        /// <summary>
        /// Best quality available.
        /// </summary>
        Best
    }

    public class AnimmexClient
    {
        private string m_primebase = "https://prime.animmex.com";
        private string m_endpoint = "https://amx.4553t5pugtt1qslvsnmpc0tpfz5fo.xyz/KL8jJhGjUN0g3HuGhUHSa5XRZ9MVrjXUuvkbCmFyo1GBMFPhvcFyc7gGKdoBxSV/N3WPL4Y3RIcyKUcBunsEyFZal6Imwlrkgcf6E2ZSZG0M8AvvtcB1.php?id={0}";

        private Dictionary<string, string> m_endpoints = new Dictionary<string, string>() {
            { "LOGIN", "/login" },
            { "VIDEOS", "/videos" },
            { "ALBUMS", "/albums" },
            { "CATEGORIES", "/categories" },
            { "COMMUNITY", "/community" },
            { "USER", "/user/{0}" },
            { "VIDEO", "/video/{0}" },
            { "ALBUM", "/album/{0}" },
            { "CATEGORY", "/category/{0}" }
        };

        /// <summary>
        /// Holds the cookies collected from making requests.
        /// </summary>
        private CookieContainer m_cookies;

        /// <summary>
        /// Holds the chosen user-agent for making requests.
        /// </summary>
        private UserAgent m_useragent;

        private string GetEndpoint(string name) {
            return $"{m_primebase}{m_endpoints[name]}";
        }

        private string GetEndpoint(string name, params string[] formatting) {
            return string.Format(GetEndpoint(name), formatting);
        }

        /// <summary>
        /// Creates a new instance of the API.
        /// </summary>
        /// <param name="useragent">Optional, specify a UserAgent for the requests to use.</param>
        public AnimmexClient(UserAgent useragent = default(UserAgent))
        {
            m_useragent = useragent == null ? UserAgent.Chrome : useragent;
            m_cookies = new CookieContainer();
            try {
                UpdateEndpoint();
            } catch { }
        }

        public async void UpdateEndpoint() {
            m_endpoint = (await Http.DoGetAsync("https://github.com/kade-robertson/AnimmexAPI/raw/master/animmex-endpoint.txt", "https://github.com/", m_useragent, m_cookies).ConfigureAwait(false)).Data;
        }

        public async Task<AnimmexVideo> GetDetailsFromVideoID(int id)
        {
            var result = await Http.DoGetAsync($"https://www.animmex.net/video/{id}/",
                                                "https://www.animmex.net/",
                                                m_useragent,
                                                m_cookies).ConfigureAwait(false);
            return VideoParser.VideoPageParse(id, result.Data);
        }

        public async Task<List<AnimmexVideo>> GetVideos(int page = 1, bool keepinvalid = false)
        {
            var retlist = new List<AnimmexVideo>();

            var result = await Http.DoGetAsync($"https://www.animmex.net/search/videos?search_query=&page={page}",
                                    "https://www.animmex.net/",
                                    m_useragent,
                                    m_cookies).ConfigureAwait(false);

            foreach (string videotext in result.Data.Split(new string[] { "<div class=\"col-sm-6 col-md-4 col-lg-4\">" }, StringSplitOptions.None).Skip(1))
            {
                var parsed = VideoParser.InfoParse(videotext);
                if (keepinvalid || parsed.IsValid)
                {
                    retlist.Add(parsed);
                }
            }

            return retlist;
        }

        /// <summary>
        /// Gets a list of videos pertaining to the search conditions.
        /// </summary>
        /// <param name="search">The search terms to be used.</param>
        /// <param name="category">The video category to be searched.</param>
        /// <param name="sorting">The way the videos should be sorted.</param>
        /// <param name="upperiod">The period in which the videos should have been uploaded.</param>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects pertaining to the search.</returns>
        public async Task<List<AnimmexVideo>> Search(string search, VideoCategory category = default(VideoCategory), SortBy sorting = default(SortBy), UploadPeriod upperiod = default(UploadPeriod), bool keepinvalid = false)
        {
            category = category == default(VideoCategory) ? VideoCategory.All : category;
            sorting = sorting == default(SortBy) ? SortBy.MostRecent : sorting;
            upperiod = upperiod == default(UploadPeriod) ? UploadPeriod.All : upperiod;

            var returnlist = new List<AnimmexVideo>();

            var result = await Http.DoGetAsync($"https://www.animmex.net/search/videos/{category.ToString()}?search_query={search}&o={sorting.ToString()}&t={upperiod.ToString()}",
                                                "https://www.animmex.net/",
                                                m_useragent,
                                                m_cookies).ConfigureAwait(false);

            foreach (string videotext in result.Data.Split(new string[] { "<div class=\"col-sm-6 col-md-4 col-lg-4\">" }, StringSplitOptions.None).Skip(1))
            {
                var parsed = VideoParser.InfoParse(videotext);
                if (keepinvalid || parsed.IsValid)
                {
                    returnlist.Add(parsed);
                }
            }

            return returnlist;
        }

        /// <summary>
        /// Gets a list of the most recently viewed videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been viewed recently.</returns>
        public async Task<List<AnimmexVideo>> GetRecentlyViewed(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.BeingWatched, keepinvalid: keepinvalid).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of the most recently uploaded videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been uploaded recently.</returns>
        public async Task<List<AnimmexVideo>> GetMostRecent(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.MostRecent, keepinvalid: keepinvalid).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of the most viewed videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been viewed the most.</returns>
        public async Task<List<AnimmexVideo>> GetMostViewed(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.MostViewed, keepinvalid: keepinvalid).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of the most discussed videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been most discussed.</returns>
        public async Task<List<AnimmexVideo>> GetMostCommented(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.MostCommented, keepinvalid: keepinvalid).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of the most recently viewed videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been viewed recently.</returns>
        public async Task<List<AnimmexVideo>> GetTopRated(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.TopRated, keepinvalid: keepinvalid).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of the most favourited videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been most favourited.</returns>
        public async Task<List<AnimmexVideo>> GetMostFavourited(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.TopFavourites, keepinvalid: keepinvalid).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of the longest videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have the longest duration.</returns>
        public async Task<List<AnimmexVideo>> GetLongest(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.Longest, keepinvalid: keepinvalid).ConfigureAwait(false);
        }

        /// <summary>
        /// Returnbs the first result from a search, using default settings.
        /// </summary>
        /// <param name="searchterm">Search terms to be used for finding video.</param>
        /// <returns>The first AnnimexVideo object found on the search page.</returns>
        public async Task<AnimmexVideo> ImFeelingLucky(string searchterm = "")
        {
            var results = await Search(searchterm).ConfigureAwait(false);
            return results[0];
        }

        /// <summary>
        /// Identifies all streaming links to an Animmex video by its ID.
        /// </summary>
        /// <param name="videoid">The video ID for which direct links should be grabbed.</param>
        /// <returns>A DirectLinks object with the available streams.</returns>
        public async Task<DirectLinks> GetDirectVideoLinksFromID(int videoid)
        {
            var video_page = await Http.DoGetAsync(string.Format(m_endpoint, videoid),
                                                    "https://www.animmex.net/search/",
                                                    m_useragent,
                                                    m_cookies).ConfigureAwait(false);

            return VideoParser.StreamParse(video_page, m_useragent, m_cookies);
        }

        /// <summary>
        /// A wrapper for GetDirectVideoLinkFromID to be used with AnimmexVideo objects.
        /// </summary>
        /// <param name="video">The video for which direct links should be grabbed.</param>
        /// <returns>A DirectLinks object with the available streams.</returns>
        public async Task<DirectLinks> GetDirectVideoLinks(AnimmexVideo video)
        {
            return await GetDirectVideoLinksFromID(video.ID).ConfigureAwait(false);
        }

        /// <summary>
        /// Identifies all cached links to an Animmex video by its ID
        /// </summary>
        /// <param name="videoid">The video ID for which cached links should be grabbed.</param>
        /// <returns>A DirectLinks object with the cached streams.</returns>
        public async Task<DirectLinks> GetCachedVideoLinksFromID(int videoid)
        {
            var small = videoid % 10;
            var medium = (videoid / 10) % 10;
            var big = (videoid / 100) % 10;
            var lg = videoid / 1000;
            var video_page = await Http.DoGetAsync($"https://animmex-cacher.github.io/files/cache/{lg}/{big}/{medium}/{small}.json",
                                                    "https://www.animmex.net/search/",
                                                    m_useragent,
                                                    m_cookies).ConfigureAwait(false);
            return VideoParser.CacheParse(video_page);
        }

        /// <summary>
        /// A wrapper for GetCachedVideoLinksFromID to be used with AnimmexVideo objects.
        /// </summary>
        /// <param name="vid">The video for which cached links should be grabbed.</param>
        /// <returns>A DirectLinks object with the cached streams.</returns>
        public async Task<DirectLinks> GetCachedVideoLinks(AnimmexVideo vid)
        {
            return await GetCachedVideoLinksFromID(vid.ID).ConfigureAwait(false);
        }

        /// <summary>
        /// Obtain a direct link to the media, can be used instead of cached links to support devices that need direct links to stream.
        /// </summary>
        /// <param name="phpstr">The URL that needs to be resolved (from a DirectLinks object)</param>
        /// <returns>A string directly linking to the requested media.</returns>
        public async Task<string> GetDirectStreamLinkFromServer(string phpstr) {
            return (await Http.DoGetAsync(phpstr, "https://www.animmex.net/search/", m_useragent, m_cookies, readdata: false).ConfigureAwait(false)).FinalURL;
        }

        /// <summary>
        /// Obtain a direct link to the media of the specified quality.
        /// </summary>
        /// <param name="d">The DirectLinks object containing the link data.</param>
        /// <param name="q">The video quality of the desired stream. Default is best available.</param>
        /// <returns>A string directly linking to the requested media.</returns>
        public async Task<string> GetDirectStreamLink(DirectLinks d, VideoQuality q = VideoQuality.Best) {
            switch (q) {
                case VideoQuality.SD: return await GetDirectStreamLinkFromServer(d.StreamSD).ConfigureAwait(false);
                case VideoQuality.HD: return await GetDirectStreamLinkFromServer(d.Stream720p).ConfigureAwait(false);
                case VideoQuality.FullHD: return await GetDirectStreamLinkFromServer(d.Stream1080p).ConfigureAwait(false);
                case VideoQuality.QuadHD: return await GetDirectStreamLinkFromServer(d.Stream1440p).ConfigureAwait(false);
                case VideoQuality.UHD: return await GetDirectStreamLinkFromServer(d.Stream2160p).ConfigureAwait(false);
            }
            return await GetDirectStreamLinkFromServer(d.BestQualityStream);
        }

        /// <summary>
        /// Obtain a direct link to the media of the specified quality.
        /// </summary>
        /// <param name="v">The AnimmexVideo object for which the links are desired.</param>
        /// <param name="d">The video quality of the desired stream. Default is best available.</param>
        /// <param name="fromcache">A boolean switch to determine if links should come from cache or directly from the server.</param>
        /// <returns>A string directly linking to the requested media.</returns>
        public async Task<string> GetDirectStreamLink(AnimmexVideo v, VideoQuality d = VideoQuality.Best, bool fromcache = true) {
            return await (fromcache ? GetDirectStreamLink(await GetCachedVideoLinks(v).ConfigureAwait(false), d) 
                                    : GetDirectStreamLink(await GetDirectVideoLinks(v).ConfigureAwait(false), d)).ConfigureAwait(false);
        }
    }
}
