namespace Zeus.Monitors
{
    interface IEnvMonitor
    {
        EnvInfo GetEnvInfo();
    }

    public class EnvInfo
    {
        public EnvInfo(string osName)
        {
            OSName = osName;
        }

        public string OSName { get; private set; }
    }
}
