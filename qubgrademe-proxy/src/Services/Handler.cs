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
    private readonly IResponseBuilder _responseBuilder;

    public Handler(ILogger<Handler> logger, IRequestParser parser, IRequestMaker requestMaker, IResponseBuilder responseBuilder)
    {
        _logger = logger;
        _parser = parser;
        _requestMaker = requestMaker;
        _responseBuilder = responseBuilder;
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
            if (newRequest.HealthCheck)
            {
                var okResponse = _responseBuilder.BuildJson(200, "OK", new { Message = "OK" });
                msg = Encoding.ASCII.GetBytes(okResponse);
            }
            else if (newRequest.Reload)
            {
                var reloadResponse = _responseBuilder.BuildJson(200, "OK", new { Message = "Reloaded Config" });
                msg = Encoding.ASCII.GetBytes(reloadResponse);
            }
            else
            {
                // Make HTTP Request here
                var response = await _requestMaker.MakeRequest(newRequest.Host, newRequest.Request);
                msg = Encoding.ASCII.GetBytes(response);
            }
        }
        else
        {
            var notFound = _responseBuilder.BuildPage(404, "NOT FOUND", "Not Found",
                "Has the url been configured in the config file?");
            msg = System.Text.Encoding.ASCII.GetBytes(notFound);
        }
        

        stream.Write(msg, 0, msg.Length);
        stream.Flush();
        client.Close();
    }
}