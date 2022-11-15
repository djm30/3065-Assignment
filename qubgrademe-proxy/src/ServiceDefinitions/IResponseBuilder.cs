namespace Proxy.ServiceDefinitions;

public interface IResponseBuilder
{
    public string BuildPage(int statusCode, string statusMessage, string heading, string info);
    public string BuildJson(int statusCode, string statusMessage, object data);
}