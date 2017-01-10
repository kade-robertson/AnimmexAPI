namespace AnimmexAPI
{
    public class VideoCategory
    {
        /// <summary>
        /// Holds the string that corresponds to the category URL.
        /// </summary>
        private string m_catstring;

        /// <summary>
        /// Filters for every category, default category.
        /// </summary>
        public static readonly VideoCategory All = new VideoCategory("all");

        /// <summary>
        /// Filters for the Animals category.
        /// </summary>
        public static readonly VideoCategory Animals = new VideoCategory("animals");

        /// <summary>
        /// Filters for the Animations category.
        /// </summary>
        public static readonly VideoCategory Animations = new VideoCategory("animations");

        /// <summary>
        /// Filters for the Anime category.
        /// </summary>
        public static readonly VideoCategory Anime = new VideoCategory("anime");

        /// <summary>
        /// Filters for the Autos & Vehicles category.
        /// </summary>
        public static readonly VideoCategory AutosAndVehicles = new VideoCategory("autos-vehicles");

        /// <summary>
        /// Filters for the Cartoon category.
        /// </summary>
        public static readonly VideoCategory Cartoon = new VideoCategory("cartoon");

        /// <summary>
        /// Filters for the Cinema & Entertainment category.
        /// </summary>
        public static readonly VideoCategory CinemaAndEntertainment = new VideoCategory("cinema-entertainment");

        /// <summary>
        /// Filters for the City & Tourism category.
        /// </summary>
        public static readonly VideoCategory CityAndTourism = new VideoCategory("city-tourism");

        /// <summary>
        /// Filters for the Comedy/Funny/Pranks category.
        /// </summary>
        public static readonly VideoCategory Comedy = new VideoCategory("comedy-funny-pranks");

        /// <summary>
        /// Filters for the Documentaries category.
        /// </summary>
        public static readonly VideoCategory Documentaries = new VideoCategory("documentaries");

        /// <summary>
        /// Filters for the Games category.
        /// </summary>
        public static readonly VideoCategory Games = new VideoCategory("games");

        /// <summary>
        /// Filters for the Internet category.
        /// </summary>
        public static readonly VideoCategory Internet = new VideoCategory("internet");

        /// <summary>
        /// Filters for the Music category.
        /// </summary>
        public static readonly VideoCategory Music = new VideoCategory("music");

        /// <summary>
        /// Filters for the Other category.
        /// </summary>
        public static readonly VideoCategory Other = new VideoCategory("other");

        /// <summary>
        /// Filters for the Science & Technology category.
        /// </summary>
        public static readonly VideoCategory ScienceAndTechnology = new VideoCategory("science-technology");

        /// <summary>
        /// Filters for the Sports category.
        /// </summary>
        public static readonly VideoCategory Sports = new VideoCategory("sports");

        /// <summary>
        /// Filters for the Tutorials category.
        /// </summary>
        public static readonly VideoCategory Tutorials = new VideoCategory("tutorials");

        /// <summary>
        /// Filters for the TV & News category.
        /// </summary>
        public static readonly VideoCategory TVAndNews = new VideoCategory("tv-news");

        private VideoCategory(string catstring = "")
        {
            m_catstring = catstring == string.Empty ? All.ToString() : catstring;
        }

        /// <summary>
        /// Returns the category string to be used by the client
        /// </summary>
        /// <returns>The category string to be used by the client.</returns>
        public override string ToString()
        {
            return m_catstring;
        }
    }
}
