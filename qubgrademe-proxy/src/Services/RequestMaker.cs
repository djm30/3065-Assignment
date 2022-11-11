using System.Net;
using System.Net.Sockets;
using Proxy.ServiceDefinitions;
using System.Linq;
using System.Net.Security;
using System.Text;
using System;

namespace Proxy.Services;

public class RequestMaker : IRequestMaker
{
    public async Task<string> MakeRequest(Uri host, IEnumerable<byte> request)
    {
        Byte[] bytes = new Byte[1024];
        string response;
        
        
        using var tcp = new TcpClient(host.Host.Trim(), host.Port);
        using (var stream = tcp.GetStream())
        {
            tcp.SendTimeout = 4000;
            tcp.ReceiveTimeout = 4000;
            if (host.Port == 443)
            {
                SslStream sslStream = new SslStream(
                    tcp.GetStream(),
                    false
                );
                
                var req = request.ToArray();

                sslStream.AuthenticateAsClient(host.Host);
                await sslStream.WriteAsync(req, 0, req.Length);
                await sslStream.FlushAsync();

                sslStream.Read(bytes, 0, bytes.Length);

                response = Encoding.UTF8.GetString(bytes);
                response = string.Join("" ,response.Where(x => x != '\0').ToList());
                Console.WriteLine(response);
            }
            else
            {
                var req = request.ToArray();

                await stream.WriteAsync(req, 0, req.Length);
                await stream.FlushAsync();
                // Reading request
                int i = stream.Read(bytes, 0, bytes.Length);
                Console.WriteLine(i);
                response = Encoding.ASCII.GetString(bytes, 0, i);
            }

        }

        return response;
    }
}