using Proxy.Models;

namespace Proxy.ServiceDefinitions;

public interface IRequestParser
{
    public ParsedRequestResponse Parse(string request);

}