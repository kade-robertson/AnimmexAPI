namespace AnimmexAPI
{
    public class SortBy
    {
        /// <summary>
        /// Holds the string that corresponds to the sorting value.
        /// </summary>
        private string m_sortby;

        /// <summary>
        /// Sorts the videos by what's being watched. Default value.
        /// </summary>
        public static readonly SortBy BeingWatched = new SortBy("bw");

        /// <summary>
        /// Sorts the videos by the most recently uploaded.
        /// </summary>
        public static readonly SortBy MostRecent = new SortBy("mr");

        /// <summary>
        /// Sorts the videos by view count.
        /// </summary>
        public static readonly SortBy MostViewed = new SortBy("mv");

        /// <summary>
        /// Sorts the videos by the number of comments.
        /// </summary>
        public static readonly SortBy MostCommented = new SortBy("mc");

        /// <summary>
        /// Sorts the videos by rating.
        /// </summary>
        public static readonly SortBy TopRated = new SortBy("tr");

        /// <summary>
        /// Sorts the videos by how many favourites.
        /// </summary>
        public static readonly SortBy TopFavourites = new SortBy("tf");

        /// <summary>
        /// Sorts the videos by duration.
        /// </summary>
        public static readonly SortBy Longest = new SortBy("lg");

        private SortBy(string sortby = "")
        {
            m_sortby = sortby == "" ? BeingWatched.ToString() : sortby;
        }

        /// <summary>
        /// Returns the sorting string to be used by the client.
        /// </summary>
        /// <returns>The sorting string to be used by the client.</returns>
        public override string ToString()
        {
            return m_sortby;
        }
    }
}
