namespace AnimmexAPI
{
    /// <summary>
    /// - m_data: string ------------------------------------ Contains the plaintext data of the request
    /// - m_finalurl: string -------------------------------- Contains the final URL of the request, after redirects
    /// + Data: string -------------------------------------- Gettable property for m_data
    /// + FinalUrl: string ---------------------------------- Gettable property for m_finalurl
    /// + HttpResult(data: string, finalurl: string): ctor -- Self-explanatory
    /// </summary>
    public class HttpResult
    {
        private readonly string m_data;
        private readonly string m_finalurl;

        public string Data { get { return m_data; } }
        public string FinalURL { get { return m_finalurl; } }

        public HttpResult(string data, string finalurl)
        {
            m_data = data;
            m_finalurl = finalurl;
        }
    }
}
