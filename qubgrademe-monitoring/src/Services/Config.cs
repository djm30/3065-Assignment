using Newtonsoft.Json;
using src.Data;
using src.Exceptions;
using ILogger = Serilog.ILogger;

namespace src.Services;

public class Config
{
    public Config(ILogger logger)
    {
        _logger = logger;
    }

    public List<string> Emails { get; set; }
    public List<ServiceSchema> Services { get; private set; }
    private readonly ILogger _logger;
    private bool firstRun = true;
    
    public async Task LoadSettings()
    {
        using var reader = new StreamReader("./config.json");
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
            if (string.IsNullOrWhiteSpace(service.expected_result))
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
                s += $"{(i + 1)+".",-21}: {service.urls[i]}\n";
            }

            s += $"{"Query Params",-20} : {service.query_string}\n";
            s += $"{"Expected Result",-20} : {service.expected_result}\n";
            s += "\n";
        }

        return s;
    }
}