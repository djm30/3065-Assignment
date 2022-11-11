using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class HandlerTCP : IConnectionHandler
{
    private readonly ILogger<HandlerTCP> _logger;
    private readonly IRequestParser _parser;

    public HandlerTCP(ILogger<HandlerTCP> logger, IRequestParser parser)
    {
        _logger = logger;
        _parser = parser;
    }

    public async Task Handle(TcpClient client)
    {
        
        _logger.LogInformation("Client passed to request handler");

        NetworkStream stream = client.GetStream();
        
        Byte[] bytes = new Byte[1024];


        int i = stream.Read(bytes, 0, bytes.Length);
        var request = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
        Console.WriteLine("Received:\n {0}", request);



        var newRequest = _parser.Parse(request);
        
        var builder = new StringBuilder();
        builder.Append("HTTP/1.1 200 OK\r\n");
        builder.Append("Access-Control-Allow-Origin: *\r\n");
        builder.Append("Content-Type: text/html; charset=utf-8\r\n");
        builder.Append("Date: " + DateTime.Now.ToString("r") + "\r\n");
        builder.Append("Connection: keep-alive\r\n");
        builder.Append("Keep-Alive: timeout=5\r\n\r\n");
      
        var msg = System.Text.Encoding.ASCII.GetBytes(builder.ToString());

        stream.Write(msg, 0, msg.Length);
        stream.Flush();
        client.Close();
    }
}