using System.Diagnostics;
using System.Net;
using Serilog;
using src.Data;

namespace src.Services;

public class MonitoringService
{
    private List<ServiceMonitorSchema> _services;
    public DateTime LastChecked { get; private set; }

    public event EventHandler<List<ServiceMonitorSchema>> ServiceStatusChanged;

    private readonly Config _config;
    private readonly IHttpClientFactory _clientFactory;
    private readonly Serilog.ILogger _logger;


    public MonitoringService(IHttpClientFactory clientFactory, Serilog.ILogger logger, Config config, TimerService timerService)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _logger = logger;
        _config = config;
        timerService.AddTimerDone(() => { RunChecksAsync(); });
    }

    public async Task RunChecksAsync()
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
                        statusCode = HttpStatusCode.ServiceUnavailable,
                        actual_result = ""
                    });
                    _logger.Error(e, "Error when sending HTTP request");
                }

            }
            serviceResults.Add(serviceResult);
        }

        LastChecked = DateTime.Now;
        _services = serviceResults;
        OnServiceStatusChanged();
    }

    // Only to be used when it is known the data has been updated 
    public List<ServiceMonitorSchema> GetMonitorDataSync()
    {
        return _services;
    }

    public async Task<List<ServiceMonitorSchema>> GetMonitorData()
    {
        // Caching kinda thing going on
        if (_services is null)
            await RunChecksAsync();
        return _services;
    }

    // Used to retrieve current seconds count for the frontend
    protected virtual void OnServiceStatusChanged()
    {
        ServiceStatusChanged?.Invoke(this, _services);
    }
}