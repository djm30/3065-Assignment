using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class CommandLineParser : ICommandLineParser
{
    public void Parse()
    {
        while (true)
        {
            var result = Console.ReadLine();
            if (result == "exit")
            {
                Environment.Exit(0);
            }
            Console.WriteLine("Not a valid command");
        }
    }
}