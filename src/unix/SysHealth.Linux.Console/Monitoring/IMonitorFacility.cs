using System.Collections.Generic;

namespace Zeus.Linux.Cli.Monitoring
{
    public interface IMonitorFacility
    {
        T Resolve<T>();
    }
}