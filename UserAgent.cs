using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    public class UserAgent
    {
        private readonly string m_uastring;

        public static readonly UserAgent Opera   = new UserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41");
        public static readonly UserAgent Chrome  = new UserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
        public static readonly UserAgent Firefox = new UserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64; rv:48.0) Gecko/20100101 Firefox/48.0");
        public static readonly UserAgent Safari  = new UserAgent("Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2");

        public UserAgent()
        {
            m_uastring = Chrome.ToString();
        }

        public UserAgent(string uastring)
        {
            m_uastring = uastring;
        }

        public override string ToString()
        {
            return m_uastring;
        }
    }
}
