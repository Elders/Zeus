using System.Collections.Generic;
using System.Linq;

namespace Zeus.Monitors.Linux.Commands
{
    public static class Top
    {
        public static Dictionary<string, string> Execute()
        {
            var result = new Dictionary<string, string>();

            result.Concat(GetRam(result));
            result.Concat(GetCPU(result));


            return result;
        }

        private static Dictionary<string, string> GetRam(Dictionary<string, string> result)
        {
            var top = LinuxCommand.Execute("top", "-b -n 1");
            var topRows = top.Split('\n').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (int row = 1; row < topRows.Length; row++)
            {
                var currentRow = topRows[row].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                for (int col = 0; col < currentRow.Length; col++)
                {
                    //Memory
                    if (row == 3)
                    {
                        if (currentRow[col].Contains("KiB") && currentRow[col + 1].Contains("Mem"))
                        {
                            var totalMemory = currentRow[col + 2];
                            var usedMemory = currentRow[col + 4];
                            var freeMemory = currentRow[col + 6];

                            result.Add("memory_total", totalMemory);
                            result.Add("memory_used", usedMemory);
                            result.Add("memory_free", freeMemory);

                            break;
                        }
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, string> GetCPU(Dictionary<string, string> result)
        {
            var top = LinuxCommand.Execute("top", "-b -n 2");
            var topRows = top.Split('\n').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var tops = 0;
            var indexToRemove = 0;

            foreach (var row in topRows)
            {
                indexToRemove++;
                if (row.Contains("top -"))
                {
                    tops++;
                    if (tops == 2)
                    {
                        break;
                    }

                }
            }

            for (int row = indexToRemove; row < topRows.Length; row++)
            {
                var currentRow = topRows[row].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                for (int col = 0; col < currentRow.Length; col++)
                {
                    //Memory

                    if (row == indexToRemove + 1)
                    {
                        if (currentRow[col].Contains("us"))
                        {
                            var freeCpu = currentRow[col - 1];
                            result.Add("cpu_free", freeCpu);
                            break;
                        }

                    }
                }
            }



            return result;
        }
    }
}