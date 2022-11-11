namespace Proxy.Models;

public class ParsedRequestResponse
{
    public Uri Host { get; set; }
    public IEnumerable<byte> Request { get; set; }
    public bool Valid { get; set; }
}