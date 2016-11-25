using System;
using Zeus.Monitors.Linux.Commands;

namespace Zeus.Monitors.Linux
{
    public class RamTOPMontior : IMemoryMonitor
    {
        const int BytesInMB = 1024;

        public MemoryInfo GetMemoryUsage()
        {
            var usage = new MemoryInfo(GetTotalMemory(), GetFreeMemoryRam());
            return usage;
        }

        public int GetTotalMemory()
        {
            string totalMemoryAsString;
            int result = 0;

            if (Top.Execute().TryGetValue("memory_total", out totalMemoryAsString))
            {

                if (!int.TryParse(totalMemoryAsString, out result))
                {
                    throw new ArgumentException("Total Physical Memory cannot be parsed to int (" + totalMemoryAsString + ")");
                }
            }

            result = result / BytesInMB;  //Kib to MiB

            return result;
        }

        public int GetFreeMemoryRam()
        {
            string availableMemoryAsString;
            int result = 0;

            if (Top.Execute().TryGetValue("memory_free", out availableMemoryAsString))
            {
                if (!int.TryParse(availableMemoryAsString, out result))
                {
                    throw new ArgumentException("Total Available Memory cannot be parsed to int (" + availableMemoryAsString + ")");
                }
            }

            result = result / BytesInMB; //Kib to MiB

            return result;
        }
    }
}