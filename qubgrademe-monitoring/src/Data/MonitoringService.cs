using System.Diagnostics;
using System.Net;
using Serilog;
namespace src.Data;

public class MonitoringService
{

    private readonly Config _config;
    private readonly IHttpClientFactory _clientFactory;
    private readonly Serilog.ILogger _logger;
    private List<ServiceMonitorSchema> _services;
    public DateTime LastChecked { get; private set; }


    public MonitoringService(IHttpClientFactory clientFactory, Serilog.ILogger logger, Config config)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _logger = logger;
        _config = config;
    }

    public async Task<List<ServiceMonitorSchema>> RunChecksAsync()
    {
        var services = _config.Services;
        var client = _clientFactory.CreateClient();

        var serviceResults = new List<ServiceMonitorSchema>();
        
        // Need to go through each service
        // Then foreach service, go through each url
        foreach (var service in services)
        {
            var serviceResult = new ServiceMonitorSchema
            {
                name = service.name,
                services = new List<MonitoringSchema>(),
                expected_value = service.expected_result
            };

            foreach (var url in service.urls)
            {
                try
                {
                    var stopWatch = Stopwatch.StartNew();
                    var response = await client.GetAsync(url + service.query_string);
                    var time = stopWatch.ElapsedMilliseconds;
                    stopWatch.Stop();
                    var body = await response.Content.ReadAsStringAsync();
                    body = body?.Replace("\n", "");
                    serviceResult.services.Add(new MonitoringSchema()
                    {
                        url = url,
                        responseTime = (int)time,
                        isExpected = service.expected_result == body,
                        statusCode = response.StatusCode,
                        actual_result = body
                    });
                }
                catch (Exception e)
                {
                    serviceResult.services.Add(new MonitoringSchema()
                    {
                        url = url,
                        responseTime = 0,
                        isExpected = false,
                        statusCode = HttpStatusCode.UnavailableForLegalReasons,
                        actual_result = ""
                    });
                    _logger.Error(e, "Error when sending HTTP request");
                }

            }
            serviceResults.Add(serviceResult);
        }

        LastChecked = DateTime.Now;
        _services = serviceResults;
        return serviceResults;
    }

    public async Task<List<ServiceMonitorSchema>> GetMonitorData()
    {
        return _services ??= await RunChecksAsync();
    }
}