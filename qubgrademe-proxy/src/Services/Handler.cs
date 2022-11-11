using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class Handler : IConnectionHandler
{
    private readonly ILogger<Handler> _logger;
    private readonly IRequestParser _parser;
    private readonly IRequestMaker _requestMaker;

    public Handler(ILogger<Handler> logger, IRequestParser parser, IRequestMaker requestMaker)
    {
        _logger = logger;
        _parser = parser;
        _requestMaker = requestMaker;
    }

    public async Task Handle(TcpClient client)
    {
        
        _logger.LogInformation("Client passed to request handler");

        NetworkStream stream = client.GetStream();
        Byte[] bytes = new Byte[1024];


        // Reading request
        int i = stream.Read(bytes, 0, bytes.Length);
        var request = Encoding.ASCII.GetString(bytes, 0, i);
        _logger.LogInformation("Received:\n {0}", request);

        
        var newRequest = _parser.Parse(request);
        byte[] msg;

        if (newRequest.Valid)
        {
            // Make HTTP Request here
            var response = await _requestMaker.MakeRequest(newRequest.Host, newRequest.Request);
            msg = Encoding.ASCII.GetBytes(response);
        }
        else
        {
            var builder = new StringBuilder();
            // Sends a 404 if not found
            builder.Append("HTTP/1.1 404 NOT FOUND\r\n");
            builder.Append("Access-Control-Allow-Origin: *\r\n");
            builder.Append("Content-Type: text/html; charset=utf-8\r\n");
            builder.Append("Content-Length: 124\r\n");
            builder.Append("Date: " + DateTime.Now.ToString("r") + "\r\n");
            builder.Append("Connection: keep-alive\r\n");
            builder.Append("Keep-Alive: timeout=5\r\n\r\n");
            builder.Append("<h1 style=\"text-align:center;\">Not Found</h1>");
            builder.Append("<p style=\"text-align:center;\">Has this route been setup in the config file?</p>");
            msg = System.Text.Encoding.ASCII.GetBytes(builder.ToString());
        }
        

        stream.Write(msg, 0, msg.Length);
        stream.Flush();
        client.Close();
    }
}