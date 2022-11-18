using System.Net;
using System.Net.Sockets;
using Proxy.ServiceDefinitions;
using System.Linq;
using System.Net.Security;
using System.Text;
using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Proxy.Services;

public class RequestMaker : IRequestMaker
{

    private readonly ILogger<RequestMaker> _logger;
    private readonly IResponseBuilder _responseBuilder;

    public RequestMaker(ILogger<RequestMaker> logger, IResponseBuilder responseBuilder)
    {
        _logger = logger;
        _responseBuilder = responseBuilder;
    }

    public async Task<string> MakeRequest(Uri host, IEnumerable<byte> request)
    {
        Byte[] bytes = new Byte[1024];
        string response;

        using var tcp = new TcpClient(host.Host.Trim(), host.Port);
        await using var stream = tcp.GetStream();
        try
        {
            tcp.SendTimeout = 4000;
            tcp.ReceiveTimeout = 4000;

            var req = request.ToArray();
            if (host.Port == 443)
            {
                SslStream sslStream = new SslStream(
                    tcp.GetStream(),
                    false
                );

                await sslStream.AuthenticateAsClientAsync(host.Host);

                await sslStream.WriteAsync(req, 0, req.Length);
                await sslStream.FlushAsync();

                var i = sslStream.Read(bytes, 0, bytes.Length);

                response = Encoding.ASCII.GetString(bytes, 0, i);

                // Was used to remove null bytes from array, not sure if its needed
                // response = string.Join("" ,response.Where(x => x != '\0').ToList());
            }
            else
            {
                await stream.WriteAsync(req, 0, req.Length);
                await stream.FlushAsync();
                // Reading request
                var i = stream.Read(bytes, 0, bytes.Length);
                response = Encoding.ASCII.GetString(bytes, 0, i);
            }
            Console.WriteLine("RESPONSE FROM SERVER:");
            Console.WriteLine(response);
            
            // Add cors header to response
            // var headers = response.Split("\r\n\r\n");
            // headers[0] += "\r\nAccess-Control-Allow-Origin: *\r\n";
            // response = headers[0] +"\r\n" + headers[1];
            
            Console.WriteLine("New Response");
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error occured while forwarding request to destination");
            response = _responseBuilder.BuildPage(500, "INTERNAL SERVER ERROR", "Internal Server Error",
                "An error has occured when making the response");
        }

        return response;
    }
}