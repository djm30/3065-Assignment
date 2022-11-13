using System.Diagnostics;
using Serilog;
namespace src.Data;

public class MonitoringService
{

    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly Serilog.ILogger _logger;


    public MonitoringService(IConfiguration configuration, IHttpClientFactory clientFactory, Serilog.ILogger logger)
    {
        _configuration = configuration;
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _logger = logger;
    }

    public async Task<List<ServiceMonitorSchema>> RunChecksAsync()
    {
        var services = _configuration.GetSection("Services").Get<List<ServiceSchema>>();
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
                    serviceResult.services.Add(new MonitoringSchema()
                    {
                        url = url,
                        responseTime = (int)time,
                        isExpected = service.expected_result == body,
                        statusCode = response.StatusCode,
                    });
                }
                catch (Exception e)
                {
                    
                }

            }
            serviceResults.Add(serviceResult);
        }

        return serviceResults;
    }
}