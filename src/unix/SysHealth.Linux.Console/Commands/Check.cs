using CommandLine;

namespace Zeus.Linux.Cli.Commands
{
    [Verb("check", HelpText = "Performs a check on the pc")]
    public class Check
    {
        [Option('a', "all", HelpText = "Performs all possible checks.")]
        public bool All { get; set; }
        // Remainder omitted

        public int Run()
        {
            System.Console.WriteLine("Run" + this);
            return 0;
        }
    }
}
