using System;
using System.Diagnostics;
using System.IO;

namespace Zeus.Monitors.Linux.Commands
{
    public static class LinuxCommand
    {
        public static string Execute(string command, string commandArgs = "")
        {
            try
            {
                using (Process process = new Process())
                {
                    string result = "";

                    process.StartInfo.FileName = command;

                    process.StartInfo.UseShellExecute = false;
                    process.EnableRaisingEvents = true;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;

                    if (!string.IsNullOrEmpty(commandArgs))
                    {
                        process.StartInfo.Arguments = commandArgs;
                    }


                    process.Start();

                    StreamReader sOut = process.StandardOutput;

                    result = sOut.ReadToEnd();
                     
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("System cannot find \"" + command + "\" command to execute! Pleasee check your runtime environment");
            }
        }
    }
}