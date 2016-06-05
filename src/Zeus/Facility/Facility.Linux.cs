using System;
using System.Collections.Generic;
using Zeus.Monitors;
using Zeus.Monitors.Linux;

namespace Zeus.Facility
{
    public class Facility : IMonitorFacility
    {
        Dictionary<Type, Func<object>> factories = new Dictionary<Type, Func<object>>();

        public Facility()
        {
            Register<ICpuMonitor>(() => new CpuTopMonitor());
            Register<IHDDMonitor>(() => new HDDDFMonitor());
            Register<IMemoryMonitor>(() => new RamTOPMontior());
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
