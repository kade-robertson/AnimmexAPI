using System;
using System.Collections.Generic;
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

        public static AnimmexVideo VideoPageParse(int id, string videotext)
        {
            try
            {
                int likes = int.Parse(Http.GetBetween(videotext, "<span id=\"video_likes\" class=\"text-white\">", "</span>"));
                int dislikes = int.Parse(Http.GetBetween(videotext, "<span id=\"video_dislikes\" class=\"text-white\">", "</span>"));
                return new AnimmexVideo(id, 
                                        Http.GetBetween(videotext, "<meta property=\"og:title\" content=\"", "\">"),
                                        Http.GetBetween(videotext, "<meta property=\"og:image\" content=\"", "\">"),
                                        int.Parse(Http.GetBetween(videotext, "<span class=\"text-white\">", " days ago<")), 
                                        new int[] { 0, 0, 0 }, // unfortunately can't get duration info 
                                        int.Parse(Http.GetBetween(videotext, "<span class=\"text-white\">", "<", 2)), 
                                        (int) Math.Round((double) 100 * (likes / (likes + dislikes))), 
                                        true);
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

            foreach (var streaminfo in videopage.Data.Split(new string[] { "<source src=" }, StringSplitOptions.None))
            {
                var temp_link = streaminfo.Split('"')[1];
                try
                {
                    //var temp_result = await Http.DoGetAsync(temp_link, "https://www.animmex.net/search/", ua, ck, readdata: false);
                    if (streaminfo.Contains("1080p"))
                    {
                        link_1080 = temp_link;
                    }
                    else if (streaminfo.Contains("720p"))
                    {
                        link_720 = temp_link;
                    }
                    else if (streaminfo.Contains("sd.php"))
                    {
                        link_sd = temp_link;
                    }
                }
                catch
                {   
                    // Weird thing with their backend, they still link to a fake 720p stream which will 404, but if you take the SD stream they have
                    // available, it is actually at 720p and not some SD resolution.
                    if (streaminfo.Contains("mp4hd.php"))
                    {
                        if (link_sd != "")
                        {
                            link_720 = link_sd;
                            link_sd = "";
                        }
                        else
                        {
                            //var temp_result = await Http.DoGetAsync(temp_link.Replace("mp4hd", "mp4sd"), "https://www.animmex.net/search/", ua, ck, readdata: false);
                            link_720 = temp_link.Replace("mp4hd", "mp4sd");
                        }
                    }
                }
            }

            return new DirectLinks(link_sd, link_720, link_1080);
        }
    }
}
