using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    public class AnimmexClient
    {
        private string m_endpoint = "https://amx.4553t5pugtt1qslvsnmpc0tpfz5fo.xyz/KL8jJhGjUN0g3HuGhUHSa5XRZ9MVrjXUuvkbCmFyo1GBMFPhvcFyc7gGKdoBxSV/N3WPL4Y3RIcyKUcBunsEyFZal6Imwlrkgcf6E2ZSZG0M8AvvtcB1.php?id={0}";
        /// <summary>
        /// Holds the cookies collected from making requests.
        /// </summary>
        private CookieContainer m_cookies;

        /// <summary>
        /// Holds the chosen user-agent for making requests.
        /// </summary>
        private UserAgent m_useragent;

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
            m_endpoint = (await Http.DoGetAsync("https://github.com/kade-robertson/AnimmexAPI/raw/master/animmex-endpoint.txt", "https://github.com/", m_useragent, m_cookies)).Data;
        }

        public async Task<AnimmexVideo> GetDetailsFromVideoID(int id)
        {
            var result = await Http.DoGetAsync($"https://www.animmex.net/video/{id}/",
                                                "https://www.animmex.net/",
                                                m_useragent,
                                                m_cookies);
            return VideoParser.VideoPageParse(id, result.Data);
        }

        public async Task<List<AnimmexVideo>> GetVideos(int page = 1, bool keepinvalid = false)
        {
            var retlist = new List<AnimmexVideo>();

            var result = await Http.DoGetAsync($"https://www.animmex.net/search/videos?search_query=&page={page}",
                                    "https://www.animmex.net/",
                                    m_useragent,
                                    m_cookies);

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
                                                m_cookies);

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
            return await Search(searchterm, sorting: SortBy.BeingWatched, keepinvalid: keepinvalid);
        }

        /// <summary>
        /// Gets a list of the most recently uploaded videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been uploaded recently.</returns>
        public async Task<List<AnimmexVideo>> GetMostRecent(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.MostRecent, keepinvalid: keepinvalid);
        }

        /// <summary>
        /// Gets a list of the most viewed videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been viewed the most.</returns>
        public async Task<List<AnimmexVideo>> GetMostViewed(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.MostViewed, keepinvalid: keepinvalid);
        }

        /// <summary>
        /// Gets a list of the most discussed videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been most discussed.</returns>
        public async Task<List<AnimmexVideo>> GetMostCommented(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.MostCommented, keepinvalid: keepinvalid);
        }

        /// <summary>
        /// Gets a list of the most recently viewed videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been viewed recently.</returns>
        public async Task<List<AnimmexVideo>> GetTopRated(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.TopRated, keepinvalid: keepinvalid);
        }

        /// <summary>
        /// Gets a list of the most favourited videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been most favourited.</returns>
        public async Task<List<AnimmexVideo>> GetMostFavourited(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.TopFavourites, keepinvalid: keepinvalid);
        }

        /// <summary>
        /// Gets a list of the longest videos, optionally with a search term.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have the longest duration.</returns>
        public async Task<List<AnimmexVideo>> GetLongest(string searchterm = "", bool keepinvalid = false)
        {
            return await Search(searchterm, sorting: SortBy.Longest, keepinvalid: keepinvalid);
        }

        /// <summary>
        /// Returnbs the first result from a search, using default settings.
        /// </summary>
        /// <param name="searchterm">Search terms to be used for finding video.</param>
        /// <returns>The first AnnimexVideo object found on the search page.</returns>
        public async Task<AnimmexVideo> ImFeelingLucky(string searchterm = "")
        {
            var results = await Search(searchterm);
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
                                                    m_cookies);

            return VideoParser.StreamParse(video_page, m_useragent, m_cookies);
        }

        /// <summary>
        /// A wrapper for GetDirectVideoLinkFromID to be used with AnimmexVideo objects.
        /// </summary>
        /// <param name="video">The video for which direct links should be grabbed.</param>
        /// <returns>A DirectLinks object with the available streams.</returns>
        public async Task<DirectLinks> GetDirectVideoLinks(AnimmexVideo video)
        {
            return await GetDirectVideoLinksFromID(video.ID);
        }

        public async Task<DirectLinks> GetCachedVideoLinksFromID(int videoid)
        {
            var small = videoid % 10;
            var medium = (videoid / 10) % 10;
            var big = (videoid / 100) % 10;
            var lg = videoid / 1000;
            var video_page = await Http.DoGetAsync($"https://animmex-cacher.github.io/files/cache/{lg}/{big}/{medium}/{small}.json",
                                                    "https://www.animmex.net/search/",
                                                    m_useragent,
                                                    m_cookies);
            return VideoParser.CacheParse(video_page);
        }

        public async Task<DirectLinks> GetCachedVideoLinks(AnimmexVideo vid)
        {
            return await GetCachedVideoLinksFromID(vid.ID);
        }
    }
}
