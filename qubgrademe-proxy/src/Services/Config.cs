using System.Net;
using Microsoft.Extensions.Logging;
using Proxy.ServiceDefinitions;
using Newtonsoft.Json;
using Proxy.Exceptions;
using Proxy.Models;

namespace Proxy.Services;

public class Config : IConfig
{

    private readonly ILogger<Config> _logger;
    private CancellationTokenSource _shutdown = new CancellationTokenSource();
    private Dictionary<string, string> routeMaps;
    private int _port;
    private IPAddress _ipAddress;
    private bool firstRun = true;

    public Config(ILogger<Config> logger)
    {
        _logger = logger;
    }

    public int GetPort()
    {
        return _port;
    }

    public IPAddress GetIpAddress()
    {
        return _ipAddress;
    }

    public bool IsShutdown()
    {
        return false;
        // return _shutdown;
    }

    public void InitiateShutdown()
    {

    }

    public Dictionary<string, string> GetRouteMap()
    {
        return routeMaps;
    }

    public async Task LoadSettings()
    {
        using var reader = new StreamReader("./config.json");
        var raw = await reader.ReadToEndAsync();
        try
        {
            var config = JsonConvert.DeserializeObject<ConfigSchema>(raw);
            ValidateConfig(config);

            routeMaps = new Dictionary<string, string>();
            _port = config.Port;
            _ipAddress = IPAddress.Parse(config.IpAddress);
            config.Routes.ForEach(x =>
            {
                if (x.Destination.Contains("localhost"))
                    x.Destination = x.Destination.Replace("localhost", "127.0.0.1");
                routeMaps.Add(x.Route, x.Destination);
            });
        }
        catch (JsonReaderException e)
        {
            if (e.Message.Contains("convert string to integer"))
            {
                _logger.LogError(e, "Please provide a valid integer for the port number");
                if (firstRun)
                    throw new ConfigurationException("Please provide a valid integer for the port number");
            }
        }
        catch (ConfigurationException e)
        {
            _logger.LogError(e, e.Message);
            if (firstRun)
                throw e;
        }
        finally
        {
            firstRun = false;
        }

    }


    // TODO Validate if any routes or destinations have been duplicated
    private void ValidateConfig(ConfigSchema schema)
    {
        if (schema.IpAddress.Trim() == "localhost") schema.IpAddress = "127.0.0.1";
        // Validating IP Address
        if (!IPAddress.TryParse(schema.IpAddress, out var IpAddress))
            throw new ConfigurationException("Please provide a valid IP Address");

        // Validation Port
        if (schema.Port < 1024 && schema.Port > 65535)
            throw new ConfigurationException("Please provide a port in the range 1024 - 65535");

        // Validating Routes
        schema.Routes.ForEach(ValidateRoute);
    }

    private void ValidateRoute(RouteSchema route)
    {
        // Validate route
        if (!Uri.TryCreate(route.Route, UriKind.Relative, out var uri))
            throw new ConfigurationException("Please provide a valid route");
        // Validate destination
        if (!Uri.TryCreate(route.Destination, UriKind.Absolute, out var destination) || !(destination.Scheme == Uri.UriSchemeHttp || destination.Scheme == Uri.UriSchemeHttps))
            throw new ConfigurationException("Please provide a valid http or https address for the destination");
    }

}