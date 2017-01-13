using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AnimmexAPI
{
    public class CachedLinks
    {
        [JsonProperty(PropertyName = "video-id")]
        public int VideoID;

        [JsonProperty(PropertyName = "video-title")]
        public string VideoTitle;

        [JsonProperty(PropertyName = "stream-sd")]
        public string StreamSD;

        [JsonProperty(PropertyName = "stream-720p")]
        public string Stream720p;

        [JsonProperty(PropertyName = "stream-1080p")]
        public string Stream1080p;

        [JsonProperty(PropertyName = "stream-1440p")]
        public string Stream1440p;

        [JsonProperty(PropertyName = "stream-2160p")]
        public string Stream2160p;
    }
}
