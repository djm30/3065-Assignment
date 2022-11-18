using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class Listener : IListener
{
    private readonly IConfig _config;
    private readonly ILogger<Listener> _logger;
    private readonly TcpListener server;
    private readonly IConnectionHandler _handler;


    public Listener(ILogger<Listener> logger, IConnectionHandler handler, IConfig config)
    {
        _logger = logger;
        _handler = handler;
        _config = config;
        server = new TcpListener(_config.GetIpAddress(), _config.GetPort());
    }

    public async Task Listen()
    {
        server.Start();
        _logger.LogInformation("Listening on port {}", _config.GetPort());
        try
        {
            while(!_config.IsShutdown())
            {
                var client = await server.AcceptTcpClientAsync();
                
                Task.Run(() =>
                {
                    _handler.Handle(client);
                });
               
            }
        }
        catch (SocketException e)
        {
            _logger.LogError(e.Message);
        }
        finally
        {
            server.Stop();
        }
    }
}