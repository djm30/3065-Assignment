using System.Net.Sockets;


namespace Proxy.ServiceDefinitions;

public interface IConnectionHandler
{
    public Task Handle(TcpClient client);
}