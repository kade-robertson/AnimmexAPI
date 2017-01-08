public class DirectLinks
{
    /// <summary>
    /// Private field holding the SD stream link.
    /// </summary>
    private string m_sd480;

    /// <summary>
    /// Private field holding the 720p stream link.
    /// </summary>
    private string m_hd720;

    /// <summary>
    /// Private field holding the 1080p stream link.
    /// </summary>
    private string m_hd1080;

    /// <summary>
    /// Private field holding the 1440p stream link.
    /// </summary>
    private string m_hd1440;

    /// <summary>
    /// Private field holding the 2160p stream link.
    /// </summary>
    private string m_hd2160;

    /// <summary>
    /// Property which returns the SD stream link.
    /// </summary>
    public string StreamSD { get { return m_sd480; } }

    /// <summary>
    /// Property which returns the 720p stream link.
    /// </summary>
    public string Stream720p { get { return m_hd720; } }

    /// <summary>
    /// Property which returns the 1080p stream link.
    /// </summary>
    public string Stream1080p { get { return m_hd1080; } }

    /// <summary>
    /// Property which returns the 1440p stream link.
    /// </summary>
    public string Stream1440p { get { return m_hd1440; } }

    /// <summary>
    /// Property which returns the 2160p stream link.
    /// </summary>
    public string Stream2160p { get { return m_hd2160; } }

    /// <summary>
    /// Property which returns the best available link.
    /// </summary>
    public string BestQualityStream {
        get {
            if (m_hd2160 != "") return m_hd2160;
            if (m_hd1440 != "") return m_hd1440;
            if (m_hd1080 != "") return m_hd1080;
            if (m_hd720 != "") return m_hd720;
            return m_sd480;
        }
    }

    /// <summary>
    /// Property which indicates whether any links were found.
    /// </summary>
    public bool LinksFound {
        get {
            return (m_hd2160 != "") || (m_hd1440 != "") || (m_hd1080 != "") || (m_hd720 != "") || (m_sd480 != "");
        }
    }
    
    /// <summary>
    /// Creates a DirectLinks object for the user to interface with.
    /// </summary>
    /// <param name="sd480">The SD stream link.</param>
    /// <param name="hd720">The 720p stream link.</param>
    /// <param name="hd1080">The 1080p stream link.</param>  
    /// <param name="hd1440">The 1440p stream link.</param>
    /// <param name="hd2160">The 2160p stream link.</param>
    public DirectLinks(string sd480 = "", string hd720 = "", string hd1080 = "", string hd1440 = "", string hd2160 = "")
    {
        m_sd480 = sd480;
        m_hd720 = hd720;
        m_hd1080 = hd1080;
        m_hd1440 = hd1440;
        m_hd2160 = hd2160;
    }
}
