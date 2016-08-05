namespace AnimmexAPI
{
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
