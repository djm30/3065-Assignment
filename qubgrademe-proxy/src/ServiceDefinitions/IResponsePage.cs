namespace Proxy.ServiceDefinitions;

public interface IResponsePage
{
    public string BuildPage(int statusCode, string statusMessage, string heading, string info);
}