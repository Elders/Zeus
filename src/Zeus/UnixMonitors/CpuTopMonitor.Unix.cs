using System;

namespace Zeus.Linux.Cli
{
    public class UnixCpuTopMonitor : ICpuMonitor
    {
        public CpuInfo GetCpuUsage()
        {
            string availableCpuAsString;
            decimal result = 0;
            if (UnixTop.Execute().TryGetValue("cpu_free", out availableCpuAsString))
            {
                if (!decimal.TryParse(availableCpuAsString, out result))
                {
                    throw new ArgumentException("CPU usage cannot be parsed to float (" + availableCpuAsString + ")");
                }
            }
            Console.WriteLine("Free cpu said :" + availableCpuAsString);
            result = 100 - result;
            return new CpuInfo(result);
        }
    }
}