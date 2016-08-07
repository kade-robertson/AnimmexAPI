using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    public class AnimmexClient
    {
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
            sorting = sorting == default(SortBy) ? SortBy.BeingWatched : sorting;
            upperiod = upperiod == default(UploadPeriod) ? UploadPeriod.All : upperiod;

            var returnlist = new List<AnimmexVideo>();
            var result = await Http.DoGetAsync($"https://www.animmex.net/search/videos/{category.ToString()}?search_query={search}&o={sorting.ToString()}&t={upperiod.ToString()}",
                                                "https://www.animmex.net/",
                                                m_useragent,
                                                m_cookies);
            foreach (string videotext in result.Data.Split(new string[] { "<div class=\"col-sm-6 col-md-4 col-lg-4\">" }, StringSplitOptions.None).Skip(1))
            {
                var parsed = VideoParser.Parse(videotext);
                if (keepinvalid || parsed.IsValid)
                {
                    returnlist.Add(parsed);
                }
            }
            return returnlist;
        }

        /// <summary>
        /// Gets a list of the most recently viewed videos.
        /// </summary>
        /// <param name="keepinvalid">Chooses whether to keep videos that weren't able to be parsed correctly.</param>
        /// <returns>A list of AnimmexVideo objects that have been viewed recently.</returns>
        public async Task<List<AnimmexVideo>> GetRecentlyViewed(bool keepinvalid = false)
        {
            return await Search(string.Empty);
        }
    }
}
