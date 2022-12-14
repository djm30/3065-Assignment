using Newtonsoft.Json;
using src.Data;
using src.Exceptions;
using ILogger = Serilog.ILogger;

namespace src.Services;

public class Config
{

    public List<string> Emails { get; set; }
    public List<ServiceSchema> Services { get; protected set; }
    private readonly ILogger _logger;
    private bool firstRun = true;
    private string path;
    public Config(ILogger logger)
    {
        _logger = logger;
        path = Environment.GetEnvironmentVariable("ENV") switch
        {
            "DEVELOPMENT" => "./Config/config.development.json",
            "PRODUCTION" => "./Config/config.production.json",
            _ => "./Config/config.local.json"
        };
    }



    public async Task LoadSettings()
    {
        using var reader = new StreamReader(path);
        var raw = await reader.ReadToEndAsync();
        try
        {
            var config = JsonConvert.DeserializeObject<ConfigSchema>(raw);
            ValidateConfig(config.Services);
            Emails = config.Emails;
            Services = config.Services;
        }
        catch (JsonReaderException e)
        {
            _logger.Error(e, "Error occured when loading JSON file, ensure file contains the services key and a list of services that match the ServiceSchema file");
            if (firstRun) throw e;
        }
        catch (ConfigurationException e)
        {
            _logger.Error(e, "Error validating config file");
            if (firstRun) throw e;
        }
    }

    private void ValidateConfig(List<ServiceSchema> services)
    {
        if (services.Count == 0)
            throw new ConfigurationException("Please provide some services in the config file!");
        foreach (var service in services)
        {
            if (string.IsNullOrWhiteSpace(service.name))
                throw new ConfigurationException("Please provide a name for every service");
            if (service.urls.Count == 0)
                throw new ConfigurationException("Please provide a URL for the service. (In an array)");
            if (service.expected_result is null)
                throw new ConfigurationException("Please provide the raw data for the expected result of the service");
        }
    }

    public string PrintConfig()
    {
        var s = "";
        foreach (var service in Services)
        {
            s += $"{"Service Name",-21}:  {service.name}\nService Urls:\n";
            for (int i = 0; i < service.urls.Count; i++)
            {
                s += $"{(i + 1) + ".",-21}: {service.urls[i]}\n";
            }

            s += $"{"Query Params",-20} : {service.query_string}\n";
            s += $"{"Expected Result",-20} : {service.expected_result}\n";
            s += "\n";
        }

        return s;
    }
}