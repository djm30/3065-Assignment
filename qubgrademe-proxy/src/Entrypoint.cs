using Proxy.ServiceDefinitions;

namespace Proxy;

// As described, is the entrypoint to the program and will run the main method
public class Entrypoint : IEntrypoint
{
    private readonly IListener _listener;
    private readonly ICommandLineParser _commandLineParser;
    private readonly IConfig _config;

    public Entrypoint(IListener listener, ICommandLineParser commandLineParser, IConfig config)
    {
        _listener = listener ?? throw new ArgumentNullException(nameof(listener));
        _commandLineParser = commandLineParser;
        _config = config;
    }

    public async Task Run()
    {
        Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Shutting down");
            _config.InitiateShutdown();
            Environment.Exit(0);
        };

        // Run TcpServer in another thread
        await _listener.Listen();


            // Poll the console for commands
        if (Environment.GetEnvironmentVariable("ENV") != "PRODUCTION")
        {
            Console.WriteLine("Type /help for a list of commands");
            var thread = Task.Run(() =>
            {
                _commandLineParser.Parse();
            });
        }
    }
}