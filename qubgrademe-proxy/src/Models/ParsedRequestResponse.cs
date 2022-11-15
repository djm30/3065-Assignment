namespace Proxy.Models;

public class ParsedRequestResponse
{
    public Uri Host { get; set; }
    public IEnumerable<byte> Request { get; set; }
    public bool Valid { get; set; }
    public bool HealthCheck { get; set; }
    public bool Reload { get; set; }
}