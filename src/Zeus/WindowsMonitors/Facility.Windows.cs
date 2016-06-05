using System;
using System.Collections.Generic;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli
{
    public class Facility : IMonitorFacility
    {
        Dictionary<Type, Func<object>> factories = new Dictionary<Type, Func<object>>();

        public Facility()
        {
            Register<ICpuMonitor>(() => new WindowsCpuMonitor());
            Register<IHDDMonitor>(() => new WindowsHDDMonitor());
            Register<IMemoryMonitor>(() => new WindowsRamMontior());
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
