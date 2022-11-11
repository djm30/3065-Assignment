using Proxy.Models;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class RequestParser : IRequestParser
{

    private readonly List<string> restMethods = new List<string>()
    {
        "GET",
        "POST",
        "PUT",
        "PATCH",
        "DELETE",
        "HEAD",
        "CONNECT",
        "OPTIONS",
        "TRACE"
    };

    private readonly Dictionary<string, string> _routing = new Dictionary<string, string>()
    {
        { "/hello", "localhost:3000" },
        { "/hello/health", "localhost:9000" }
    };
    
    public ParsedRequestResponse Parse(string request)
    {

        var response = new ParsedRequestResponse()
        {
            request = null,
            valid = true,
        };
        
        // Confirm the request is an HTTP request
        var header = request.Split("\n")[0];
        if (VerifyHTTPHeader(header))
        {
            var headerSplit = header.Split(" ");
            // Get the base route of the request somehow
            var url = headerSplit[1];

            // Need to see if there is a query string
            var query = getQueryString(url);

            var newUrl = matchUrl(url);

            // Need to check if a destination was matched
            var finalDestination = newUrl + query;

            // If new url is "", means no matching url was found in the configuration
            if (newUrl == "")
            {
                response.valid = false;
            }

            // Putting the new url together
            var newHeader = headerSplit[0] + " " + newUrl + " " + headerSplit[2] + "\n";
            var newMessage = newHeader + string.Join("", request.Split("\n").Skip(1).Select(x => x + "\n"));
            response.request = System.Text.Encoding.ASCII.GetBytes(newMessage);
        }
        else
            response.valid = false;

        return response;
    }

    private string matchUrl(string url)
    {
        var newUrl = "";
        var route = getIndexOfQuery(url) == -1 ? url : url.Substring(0, getIndexOfQuery(url));

        if (route.Trim() != "/")
        {
            var routing = route.Split("/").Where(x => x != "").Select(x => "/" + x).ToList();
            for (int i = routing.Count(); i > 0; i--)
            {
                // Need to start with full url from route,
                // then take one off each time if no match is found
                var currRoute = string.Join("", routing.Take(i));
                if (_routing.TryGetValue(currRoute, out var destination))
                {
                    newUrl = destination + string.Join("", routing.Skip(i));
                    break;
                }
            }
        }
        else
        {
            if (_routing.TryGetValue("/", out var destination))
            {
                newUrl = destination;
            }
        }
        return newUrl;
    }

    private int getIndexOfQuery(string url) => url.IndexOf("?");

    private string getQueryString(string url)
    {
        return getIndexOfQuery(url) == -1 ? "" : url.Substring(getIndexOfQuery(url));
    }


    private bool VerifyHTTPHeader(string header)
    {
        var headerSplit = header.Split(" ");
        if (headerSplit.Length == 3)
        {
            // Check if first element if valid rest endpoint
            if (restMethods.IndexOf(headerSplit[0].ToUpper()) != -1)
                // Get url from second element
                if (Uri.TryCreate(headerSplit[1], UriKind.Relative, out var httpHeader))
                    // Http Version should be 1.1 on third element
                    return headerSplit[2] == "HTTP/1.1\r" || headerSplit[2] == "HTTP/2\r";
        }
        return false;
    }
    
    
}