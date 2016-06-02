using System;
using System.Diagnostics;
using System.IO;

namespace Zeus.Linux.Cli
{
    public static class UnixCommand
    {
        public static string Execute(string command, string commandArgs = "")
        {
            try
            {
                Process testProcess = new Process();
                string result = "";

                testProcess.StartInfo.FileName = command;

                testProcess.StartInfo.UseShellExecute = false;
                testProcess.StartInfo.RedirectStandardInput = true;
                testProcess.StartInfo.RedirectStandardOutput = true;

                if (!string.IsNullOrEmpty(commandArgs))
                {
                    testProcess.StartInfo.Arguments = commandArgs;
                }


                testProcess.Start();

                StreamReader sOut = testProcess.StandardOutput;

                result = sOut.ReadToEnd();

                if (!testProcess.HasExited)
                {
                    testProcess.Kill();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("System cannot find \"" + command + "\" command to execute! Pleasee check your runtime environment");
            }
        }
    }
}