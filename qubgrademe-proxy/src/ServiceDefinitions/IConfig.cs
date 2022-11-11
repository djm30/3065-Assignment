using System.Net;

namespace Proxy.ServiceDefinitions;

public interface  IConfig
{
    public int GetPort();
    public bool IsShutdown();
    public void InitiateShutdown();

    public IPAddress GetIpAddress();

    public Dictionary<string, string> GetRouteMap();
    
    public Task LoadSettings();
}