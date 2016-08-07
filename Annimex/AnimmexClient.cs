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
        /// Obtains a list of the most recently viewed videos on Animmex.
        /// </summary>
        /// <returns>A list of AnimmexVideo objects, the videos found in the "Recently Viewed" category.</returns>
        public async Task<List<AnimmexVideo>> GetRecentlyViewed(bool keepinvalid = false)
        {
            var returnlist = new List<AnimmexVideo>();
            var result = await Http.DoGetAsync("https://www.animmex.net/videos?o=bw",
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
    }
}
