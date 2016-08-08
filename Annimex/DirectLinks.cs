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
    /// Property which returns the SD tream link.
    /// </summary>
    public string StreamSD { get { return m_sd480; } }

    /// <summary>
    /// Property which returns the 720p tream link.
    /// </summary>
    public string Stream720p { get { return m_hd720; } }

    /// <summary>
    /// Property which returns the 1080p tream link.
    /// </summary>
    public string Stream1080p { get { return m_hd1080; } }

    /// <summary>
    /// Property which returns the best available link.
    /// </summary>
    public string BestQualityStream {
        get {
            if (m_hd1080 != "") return m_hd1080;
            if (m_hd720 != "") return m_hd720;
            return m_sd480;
        }
    }
    
    /// <summary>
    /// Creates a DirectLinks object for the user to interface with.
    /// </summary>
    /// <param name="sd480">The SD stream link.</param>
    /// <param name="hd720">The 720p stream link.</param>
    /// <param name="hd1080">The 1080p stream link.</param>    
    public DirectLinks(string sd480 = "", string hd720 = "", string hd1080 = "")
    {
        m_sd480 = sd480;
        m_hd720 = hd720;
        m_hd1080 = hd1080;
    }
}
