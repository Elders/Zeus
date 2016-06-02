using System.Collections.Generic;
using System.Linq;

namespace Zeus.Linux.Cli
{
    public static class UnixTop
    {
        public static Dictionary<string, string> Execute()
        {
            var result = new Dictionary<string, string>();

            var top = UnixCommand.Execute("top", "-b -n 1");
            var topRows = top.Split('\n').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (int row = 1; row < topRows.Length; row++)
            {
                var currentRow = topRows[row].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                for (int col = 0; col < currentRow.Length; col++)
                {
                    //CPU
                    if (row == 2)
                    {
                        if (currentRow[col].Contains("id"))
                        {
                            var freeCpu = currentRow[col - 1];

                            result.Add("cpu_free", freeCpu);
                            break;
                        }
                    }

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
    }
}