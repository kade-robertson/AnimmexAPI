using System;

namespace AnimmexAPI
{
    public class AnimmexVideo
    {
        /// <summary>
        /// Private field for the video's ID.
        /// </summary>
        private readonly int m_videoid;
        
        /// <summary>
        /// Private field for the video's title.
        /// </summary>
        private readonly string m_title;

        /// <summary>
        /// Private field for the video's thumbnail URL.
        /// </summary>
        private readonly string m_thumburl;

        /// <summary>
        /// Private field for the video's upload date.
        /// </summary>
        private readonly DateTime m_adddays;

        /// <summary>
        /// Private field for the video's duration.
        /// </summary>
        private readonly TimeSpan m_duration;

        /// <summary>
        /// Private field for the video's view count.
        /// </summary>
        private readonly int m_views;

        /// <summary>
        /// Private field for the video's rating.
        /// </summary>
        private readonly int m_rating;

        /// <summary>
        /// Private field for seeing if a valid video was parsed.
        /// </summary>
        private readonly bool m_isvalid;

        /// <summary>
        /// Gets the video ID of an AnimmexVideo.
        /// </summary>
        public int ID { get { return m_videoid; } }

        /// <summary>
        /// Gets the video title of an AnimmexVideo.
        /// </summary>
        public string Title { get { return m_title; } }

        /// <summary>
        /// Gets the thumbnail URL of an AnimmexVideo.
        /// </summary>
        public string ThumbnailURL { get { return m_thumburl; } }

        /// <summary>
        /// Gets the upload date and time of an AnimmexVideo.
        /// </summary>
        public DateTime UploadDate { get { return m_adddays; } }

        /// <summary>
        /// Gets the duration of an AnimmexVideo.
        /// </summary>
        public TimeSpan Duration { get { return m_duration; } }

        /// <summary>
        /// Gets the view count of an AnimmexVideo.
        /// </summary>
        public int ViewCount { get { return m_views; } }

        /// <summary>
        /// Gets the rating of an AnimmexVideo.
        /// </summary>
        public int Rating { get { return m_rating; } }

        /// <summary>
        /// Gets the validity of the parsed video.
        /// </summary>
        public bool IsValid { get { return m_isvalid; } }

        /// <summary>
        /// Creates an AnimmexVideo object.
        /// </summary>
        /// <param name="videoid">The video ID pertaining to the video.</param>
        /// <param name="title">The title of the video.</param>
        /// <param name="thumburl">The URL to the video's thumbnail.</param>
        /// <param name="adddays">The time this was upload, in the number of seconds after the video was uploaded.</param>
        /// <param name="duration">Array of length 3 with the hours, minutes, seconds of the video duration.</param>
        /// <param name="views">The number of views the video has received.</param>
        /// <param name="rating">The rating the video has received, sould be between 0 and 100.</param>
        public AnimmexVideo(int videoid, string title, string thumburl, int adddays, int[] duration, int views, int rating, bool isvalid = true)
        {
            m_videoid = videoid;
            m_title = title;
            m_thumburl = thumburl;
            m_adddays = new DateTime(DateTime.Now.AddSeconds(-adddays).Ticks);
            m_duration = new TimeSpan(duration[0], duration[1], duration[2]);
            m_views = views;
            m_rating = rating;
            m_isvalid = isvalid;
        }

        public override string ToString()
        {
            return m_title;
        }
    }
}
