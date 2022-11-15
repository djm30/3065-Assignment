using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class CommandLineParser : ICommandLineParser
{

    private readonly IConfig _config;

    public CommandLineParser(IConfig config)
    {
        _config = config;
    }

    public void Parse()
    {
        while (true)
        {
            PrintRoutes();
            var result = Console.ReadLine();
            switch (result?.Trim())
            {
                case "/exit":
                    Environment.Exit(0);
                    break;
                case "/reload":
                    _config.LoadSettings();
                    break;
                case "/list":
                    PrintRoutes();
                    break;
                case "/help":
                    PrintHelp();
                    break;
                case "/clear":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Not a valid command");
                    break;
            }
            Console.WriteLine("\n");
        }
    }

    private void PrintRoutes()
    {
        Console.WriteLine("Route         Destination");
        foreach (var route in _config.GetRouteMap())
        {
            var s = $"|  {route.Key,-10} | {route.Value}";
            Console.WriteLine(s);
        }
    }

    private void PrintHelp()
    {
        Console.WriteLine("\n/exit → Exits the program");
        Console.WriteLine("/reload → Reloads the json config file");
        Console.WriteLine("/list → Prints current proxy bindings");
        Console.WriteLine("/clear → Clears current console output");
    }
}