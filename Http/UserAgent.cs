namespace AnimmexAPI
{
    public class UserAgent
    {
        /// <summary>
        /// Private field for the user-agent string.
        /// </summary>
        private readonly string m_uastring = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";

        /// <summary>
        /// Default Opera user-agent.
        /// </summary>
        public static readonly UserAgent Opera = new UserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41");

        /// <summary>
        /// Default Chrome user-agent.
        /// </summary>
        public static readonly UserAgent Chrome = new UserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");

        /// <summary>
        /// Default Firefox user-agent.
        /// </summary>
        public static readonly UserAgent Firefox = new UserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64; rv:48.0) Gecko/20100101 Firefox/48.0");

        /// <summary>
        /// Default Safari user-agent.
        /// </summary>
        public static readonly UserAgent Safari = new UserAgent("Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2");

        /// <summary>
        /// Default IE user-agent.
        /// </summary>
        public static readonly UserAgent InternetExplorer = new UserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");

        /// <summary>
        /// Default Microsoft Edge user-agent.
        /// </summary>
        public static readonly UserAgent Edge = new UserAgent("Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10136");

        /// <summary>
        /// Creates the default UserAgent object, for Chrome.
        /// </summary>
        public UserAgent()
        {
            m_uastring = Chrome.ToString();
        }

        /// <summary>
        /// Creates a new UserAgent object with specified user-agent string.
        /// </summary>
        /// <param name="uastring">The desired user-agent string.</param>
        public UserAgent(string uastring)
        {
            m_uastring = uastring;
        }

        /// <summary>
        /// Overrides to return the user-agent string.
        /// </summary>
        /// <returns>The user-agent string of the object.</returns>
        public override string ToString()
        {
            return m_uastring;
        }
    }
}
