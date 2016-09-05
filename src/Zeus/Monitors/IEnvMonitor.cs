namespace Zeus.Monitors
{
    interface IEnvMonitor
    {
        EnvInfo GetEnvInfo();
    }

    public class EnvInfo
    {
        public EnvInfo(string osName, EnvTimeInfo time)
        {
            OSName = osName;
            Time = time;
        }

        public string OSName { get; private set; }

        public EnvTimeInfo Time { get; private set; }

    }


    public class EnvTimeInfo
    {
        public EnvTimeInfo(string time, string zone, string utcOffset)
        {
            Time = time;
            TimeZone = zone;
            UtcOffset = utcOffset;
        }

        public string Time { get; private set; }

        public string TimeZone { get; private set; }

        public string UtcOffset { get; private set; }
    }
}
