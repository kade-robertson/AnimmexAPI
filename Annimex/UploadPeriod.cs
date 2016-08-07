namespace AnimmexAPI
{
    public class UploadPeriod
    {
        /// <summary>
        /// Holds the string that corresponds to the Upload Period value.
        /// </summary>
        private string m_upperiod;

        /// <summary>
        /// Filters for all upload dates, default category.
        /// </summary>
        public static readonly UploadPeriod All = new UploadPeriod("a");

        /// <summary>
        /// Filters for videos uploaded today.
        /// </summary>
        public static readonly UploadPeriod Today = new UploadPeriod("t");

        /// <summary>
        /// Filters for videos uploaded this week.
        /// </summary>
        public static readonly UploadPeriod ThisWeek = new UploadPeriod("w");

        /// <summary>
        /// Filters for videos uploaded this month.
        /// </summary>
        public static readonly UploadPeriod ThisMonth = new UploadPeriod("m");


        private UploadPeriod(string upperiod = "")
        {
            m_upperiod = upperiod == "" ? All.ToString() : upperiod;
        }

        /// <summary>
        /// Returns the upload period string to be used by the client.
        /// </summary>
        /// <returns>The upload period string to be used by the client.</returns>
        public override string ToString()
        {
            return m_upperiod;
        }
    }
}
