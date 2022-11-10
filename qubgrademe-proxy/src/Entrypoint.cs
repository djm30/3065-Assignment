using Proxy.ServiceDefinitions;

namespace Proxy;

// As described, is the entrypoint to the program and will run the main method
public class Entrypoint : IEntrypoint
{
    private readonly IListener _listener;

    public Entrypoint(IListener listener)
    {
        _listener = listener ?? throw new ArgumentNullException(nameof(listener));
    }

    public void Run()
    {
        _listener.Listen();
    }
}