using System.Net;

namespace Proxy.Models;

public class ConfigSchema
{
    public string IpAddress { get; set; }
    public int Port { get; set; }
    public List<RouteSchema> Routes { get; set; }
}

public class RouteSchema
{
    public string Route { get; set; }
    public string Destination { get; set; }
}