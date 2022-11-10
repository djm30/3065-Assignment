using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class ListenerTCP : IListener
{
    private const Int32 Port = 9005;
    private readonly IPAddress localHost = IPAddress.Parse("127.0.0.1");
    private readonly ILogger<ListenerTCP> _logger;
    private readonly TcpListener server;
    private readonly IConnectionHandler _handler;


    public ListenerTCP(ILogger<ListenerTCP> logger, IConnectionHandler handler)
    {
        _logger = logger;
        _handler = handler;
        server = new TcpListener(localHost, Port);
    }

    public async void Listen()
    {
        server.Start();
        _logger.LogInformation("Listening on port {}", Port);
        try
        {
            while(true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                using TcpClient client = server.AcceptTcpClient();
                await _handler.Handle(client);

            }
        }
        catch (SocketException e)
        {
        }
        finally
        {
            server.Stop();
        }

    }
}