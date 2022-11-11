using Proxy.ServiceDefinitions;

namespace Proxy;

// As described, is the entrypoint to the program and will run the main method
public class Entrypoint : IEntrypoint
{
    private readonly IListener _listener;
    private readonly ICommandLineParser _commandLineParser;

    public Entrypoint(IListener listener, ICommandLineParser commandLineParser)
    {
        _listener = listener ?? throw new ArgumentNullException(nameof(listener));
        _commandLineParser = commandLineParser;
    }

    public void Run()
    {
        // Run TcpServer in another thread
        Task.Run(() =>
        {
            _listener.Listen();
        });

        
        // Poll the console for commands
        _commandLineParser.Parse();



    }
}