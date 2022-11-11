namespace Proxy.Models;

public class ParsedRequestResponse
{
    public IEnumerable<byte> request { get; set; }
    public bool valid { get; set; }
}