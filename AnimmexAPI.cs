using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    public class AnimmexAPI
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
        public AnimmexAPI(UserAgent useragent = default(UserAgent))
        {
            m_useragent = useragent == null ? UserAgent.Chrome : useragent;
            m_cookies = new CookieContainer();
        }
        
        /// <summary>
        /// Obtains a list of the most recently viewed videos on Animmex.
        /// </summary>
        /// <returns>A list of AnimmexVideo objects, the videos found in the "Recently Viewed" category.</returns>
        public async Task<List<AnimmexVideo>> GetRecentlyViewed()
        {
            var returnlist = new List<AnimmexVideo>();
            var result = await Http.DoGetAsync("https://www.animmex.net/videos?o=bw",
                                               "https://www.animmex.net/",
                                               m_useragent,
                                               m_cookies);
            foreach (string videotext in result.Data.Split(new string[] { "<div class=\"col-sm-6 col-md-4 col-lg-4\">" }, StringSplitOptions.None).Skip(1))
            {
                try {
                    var videoid = int.Parse(Http.GetBetween(videotext, "<a href=\"/video/", "/").Trim());
                    var title = Http.GetBetween(videotext, "title=\"", "\" alt=\"").Trim().Replace("&#039;", "'");
                    var thumburl = Http.GetBetween(videotext, "<img src=\"", "\" ").Trim();
                    var duration_tmp = Http.GetBetween(videotext, "<div class=\"duration\">", "</div>").Trim();
                    var duration_temp = duration_tmp.Contains(":") ? duration_tmp.Split(':') : new string[] { "0", "0", "0" };
                    var duration = new int[3] { duration_temp.Length == 3 ? int.Parse(duration_temp[2]) : 0,
                                            duration_temp.Length >= 2 ? int.Parse(duration_temp[1]) : 0,
                                            int.Parse(duration_temp[0]) };
                    var update = 0;
                    if (videotext.Contains("days ago"))
                    {
                        update = 86400 * int.Parse(Http.GetBetween(videotext, "<div class=\"video-added\">", " days ago").Trim());
                    } else if (videotext.Contains("hours ago")) {
                        update = 3600 * int.Parse(Http.GetBetween(videotext, "<div class=\"video-added\">", " hours ago").Trim());
                    }
                    var views = int.Parse(Http.GetBetween(videotext, "<div class=\"video-views pull-left\">", " views").Trim());
                    var rating_temp = Http.GetBetween(videotext, "div class=\"video-rating", "</div>");
                    var rating = rating_temp.Contains("no-rating") ? 0 : int.Parse(Http.GetBetween(rating_temp, "<b>", "%</b>"));
                    returnlist.Add(new AnimmexVideo(videoid, title, thumburl, update, duration, views, rating));
                } catch {
                    // Debug Line -- returnlist.Add(new AnimmexVideo(0, videotext, "", 0, new int[] { 0, 0, 0 }, 0, 0));
                }
            }
            return returnlist;
        }
    }
}
