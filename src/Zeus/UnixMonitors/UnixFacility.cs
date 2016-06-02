using System;
using System.Collections.Generic;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.UnixMonitors
{
    public class UnixFacility : IMonitorFacility
    {
        Dictionary<Type, Func<object>> factories = new Dictionary<Type, Func<object>>();

        public UnixFacility()
        {
            Register<ICpuMonitor>(() => new UnixCpuTopMonitor());
            Register<IHDDMonitor>(() => new UnixHDDDFMonitor());
            Register<IMemoryMonitor>(() => new UnixRamTOPMontior());
        }

        public void Register<T>(Func<T> factory)
            where T : class
        {
            factories.Add(typeof(T), factory);
        }

        public T Resolve<T>()
        {
            return (T)factories[typeof(T)]();
        }
    }
}
