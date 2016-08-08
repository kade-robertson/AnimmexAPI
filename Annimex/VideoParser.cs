using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    public class VideoParser
    {
        public static AnimmexVideo InfoParse(string videotext)
        {
            try
            {
                var videoid = int.Parse(Http.GetBetween(videotext, "<a href=\"/video/", "/").Trim());
                var title = Http.GetBetween(videotext, "title=\"", "\" alt=\"").Trim().Replace("&#039;", "'");
                var thumburl = Http.GetBetween(videotext, "<img src=\"", "\" ").Trim();
                var duration_tmp = Http.GetBetween(videotext, "<div class=\"duration\">", "</div>").Trim();
                var duration_temp = duration_tmp.Contains(":") ? duration_tmp.Split(':') : new string[] { "0", "0", "0" };
                var duration = new int[3] { duration_temp.Length == 3 ? int.Parse(duration_temp[duration_temp.Length - 3]) : 0,
                                            duration_temp.Length >= 2 ? int.Parse(duration_temp[duration_temp.Length - 2]) : 0,
                                            int.Parse(duration_temp[duration_temp.Length - 1])};
                var update = 0;
                if (videotext.Contains("days ago"))
                {
                    update = 86400 * int.Parse(Http.GetBetween(videotext, "<div class=\"video-added\">", " days ago").Trim());
                }
                else if (videotext.Contains("hours ago"))
                {
                    update = 3600 * int.Parse(Http.GetBetween(videotext, "<div class=\"video-added\">", " hours ago").Trim());
                }
                var views = int.Parse(Http.GetBetween(videotext, "<div class=\"video-views pull-left\">", " views").Trim());
                var rating_temp = Http.GetBetween(videotext, "div class=\"video-rating", "</div>");
                var rating = rating_temp.Contains("no-rating") ? 0 : int.Parse(Http.GetBetween(rating_temp, "<b>", "%</b>"));
                return new AnimmexVideo(videoid, title, thumburl, update, duration, views, rating);
            }
            catch
            {
                return new AnimmexVideo(0, videotext, "", 0, new int[] { 0, 0, 0 }, 0, 0, false);
            }
        }

        public static async Task<DirectLinks> StreamParse(HttpResult videopage, UserAgent ua, CookieContainer ck)
        {
            var link_sd = "";
            var link_720 = "";
            var link_1080 = "";

            foreach (var streaminfo in videopage.Data.Split(new string[] { "<source src=" }, StringSplitOptions.None).Skip(1))
            {
                var temp_link = streaminfo.Split('"')[1];
                var temp_result = await Http.DoGetAsync(temp_link, "https://www.animmex.net/search/", ua, ck, readdata: false);
                if (streaminfo.Contains("1080p"))
                {
                    link_1080 = temp_result.FinalURL;
                }
                else if (streaminfo.Contains("720p"))
                {
                    link_720 = temp_result.FinalURL;
                }
                else if (streaminfo.Contains("sd.php"))
                {
                    link_sd = temp_result.FinalURL;
                }
            }

            return new DirectLinks(link_sd, link_720, link_1080);
        }
    }
}
