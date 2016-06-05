using CommandLine;
using Zeus.Linux.Cli.Commands;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Zeus.Linux.Cli
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var commandTypes = typeof(Program).GetTypeInfo()
                                .Assembly.GetTypes().Where(x => typeof(ICliCommand).IsAssignableFrom(x));
            var result = Parser.Default.ParseArguments(args, commandTypes.ToArray())
                    .MapResult(
                    command => Handle(command),
                    errors => Handle(errors));
            return result;
        }

        private static int Handle(object command)
        {
            if (command is ICliCommand)
            {
                return (command as ICliCommand).Run();
            }
            else
            {
                System.Console.WriteLine("Unknown command {0}", command);
                return 1;
            }
        }

        private static int Handle(IEnumerable<Error> errors)
        {
            foreach (var item in errors)
            {
                if (item.Tag == ErrorType.BadVerbSelectedError || item.Tag == ErrorType.HelpRequestedError || item.Tag == ErrorType.HelpVerbRequestedError)
                    continue;
                System.Console.WriteLine(item.ToString());
            }
            return 1;
        }
    }
}
