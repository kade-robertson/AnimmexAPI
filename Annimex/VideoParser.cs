using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    public class VideoParser
    {
        public static AnimmexVideo Parse(string videotext)
        {
            try
            {
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
    }
}
