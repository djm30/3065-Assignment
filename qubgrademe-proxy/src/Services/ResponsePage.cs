using System.Text;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class ResponsePage : IResponsePage
{
    public string BuildPage(int statusCode, string statusMessage, string heading, string info)
    {
        var firstHeader = $"HTTP/1.1 {statusCode} {statusMessage}\r\n";
        var h1 = $"<h1 style=\"text-align:center;\">{heading}</h1>";
        var p = $"<p style=\"text-align:center;\">{info}</p>";


        var contentLength = Encoding.ASCII.GetBytes(h1 + p).Length;
        var contentLengthHeader = $"Content-Length: {contentLength}\r\n";

        var builder = new StringBuilder();
        // Sends a 404 if not found
        builder.Append(firstHeader);
        builder.Append("Access-Control-Allow-Origin: *\r\n");
        builder.Append("Content-Type: text/html; charset=utf-8\r\n");
        builder.Append(contentLengthHeader);
        builder.Append("Date: " + DateTime.Now.ToString("r") + "\r\n");
        builder.Append("Connection: keep-alive\r\n");
        builder.Append("Keep-Alive: timeout=5\r\n\r\n");
        builder.Append(h1);
        builder.Append(p);

        return builder.ToString();
    }
}