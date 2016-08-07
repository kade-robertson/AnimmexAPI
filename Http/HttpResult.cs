namespace AnimmexAPI
{
    public class HttpResult
    {
        /// <summary>
        /// Contains the data from an HTTP request.
        /// </summary>
        private readonly string m_data;

        /// <summary>
        /// Contains the last URL from an HTTP request.
        /// </summary>
        private readonly string m_finalurl;

        /// <summary>
        /// Property containing the data from an HTTP request.
        /// </summary>
        public string Data { get { return m_data; } }

        /// <summary>
        /// Property containing the last URL from an HTTP request.
        /// </summary>
        public string FinalURL { get { return m_finalurl; } }


        /// <summary>
        /// Creates a new HttpResult object.
        /// </summary>
        /// <param name="data">Data from the request as a string.</param>
        /// <param name="finalurl">Last visited URL from the request as a string.</param>
        public HttpResult(string data, string finalurl)
        {
            m_data = data;
            m_finalurl = finalurl;
        }
    }
}
