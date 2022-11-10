using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class HandlerTCP : IConnectionHandler
{
    private readonly ILogger<HandlerTCP> _logger;

    public HandlerTCP(ILogger<HandlerTCP> logger)
    {
        _logger = logger;
    }

    public async Task Handle(TcpClient client)
    {
        
        _logger.LogInformation("Client passed to request handler");

        NetworkStream stream = client.GetStream();
        
        Byte[] bytes = new Byte[1024];


        int i = stream.Read(bytes, 0, bytes.Length);
        var request = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
        Console.WriteLine("Received:\n {0}", request);

        var bodyText = "Hi there";
        var body = System.Text.Encoding.ASCII.GetBytes(bodyText);

        var builder = new StringBuilder();
        builder.Append("HTTP/1.1 200 OK\r\n");
        builder.Append("Content-Type: text/html; charset=utf-8\r\n");
        builder.Append("Content-Length: " + body.Length+"\r\n");
        builder.Append("Connection: Closed\r\n");
        builder.Append(bodyText);
        
        var msg = System.Text.Encoding.ASCII.GetBytes(builder.ToString());

        stream.Write(msg, 0, msg.Length);
        stream.Flush();
        Console.WriteLine("Hello");
        client.Close();
    }
}