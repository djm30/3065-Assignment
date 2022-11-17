using Proxy.Models;
using Proxy.ServiceDefinitions;

namespace Proxy.Services;

public class RequestParser : IRequestParser
{

    private readonly List<string> _restMethods = new List<string>()
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

    private readonly IConfig _config;

    public RequestParser(IConfig config)
    {
        _config = config;
    }

    public ParsedRequestResponse Parse(string request)
    {
        var response = new ParsedRequestResponse()
        {
            Request = null,
            Valid = true,
        };
        
        // Confirm the request is an HTTP request
        var header = request.Split("\n")[0];
        if (VerifyHTTPHeader(header))
        {
            var headerSplit = header.Split(" ");
            // Get the base route of the request somehow
            var url = headerSplit[1];
            
            // Check if URL is either a reload or health check
            if (url.StartsWith("/health"))
            {
                response.HealthCheck = true;
                return response;
            }
            if(url.StartsWith("/reload"))
            {
                _config.LoadSettings();
                response.Reload = true;
                return response;
            }

            // Need to see if there is a query string
            var query = getQueryString(url);

            // Gets new resource route
            var newUrl = matchUrl(url);

            // Need to check if a destination was matched
            if (string.IsNullOrWhiteSpace(newUrl)) response.Valid = false;
            else
            {
                var host = new Uri(newUrl+query);
                
                // New HTTP header   // REST Method        /path                      // HTTP/1.1
                var httpHeader = headerSplit[0] + " " + host.PathAndQuery + " " + headerSplit[2] + "\n";
                
                // Adding new host header
                httpHeader += "Host: " + host.Host + "\r\n";

                // Getting headers from response, skipping first row as there is a new one and filtering out some headers that cause issues
                var oldHeaderRows = request.Split("\n")
                    .Where(x => !x.Contains("Host") && !x.Contains("Connection") && !x.Contains("Accept-Encoding") && !x.Contains("Access-Control-Allow-Origin") && !x.Contains("Vary: Origin"))
                    .Skip(1);
                // Appending new host header as well
                var requestHeaders = httpHeader + string.Join("", oldHeaderRows.Select(x => x + "\n"));
                requestHeaders = requestHeaders.Substring(0, requestHeaders.Length - 1);
                
                
                response.Host = host;
                response.Request = System.Text.Encoding.ASCII.GetBytes(requestHeaders);
            }
        }
        else
            response.Valid = false;

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
                if (_config.GetRouteMap().TryGetValue(currRoute, out var destination))
                {
                    newUrl = destination + string.Join("", routing.Skip(i));
                    break;
                }
            }
        }
        else
        {
            if (_config.GetRouteMap().TryGetValue("/", out var destination))
            {
                newUrl = destination;
            }
        }
        return newUrl;
    }

    private void HeadersToBytes(string headers)
    {
        
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
            if (_restMethods.IndexOf(headerSplit[0].ToUpper()) != -1)
                // Get url from second element
                if (Uri.TryCreate(headerSplit[1], UriKind.Relative, out var httpHeader))
                    // Http Version should be 1.1 on third element
                    return headerSplit[2] == "HTTP/1.1\r" || headerSplit[2] == "HTTP/2\r";
        }
        return false;
    }
    
    
}