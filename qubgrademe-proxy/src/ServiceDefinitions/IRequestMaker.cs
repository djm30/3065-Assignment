namespace Proxy.ServiceDefinitions;

public interface IRequestMaker
{
    public Task<string> MakeRequest(Uri host, IEnumerable<byte> request);
}