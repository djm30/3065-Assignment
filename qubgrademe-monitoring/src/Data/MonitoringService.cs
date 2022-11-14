using System.Diagnostics;
using System.Net;
using System.Timers;
using Serilog;
using Timer = System.Timers.Timer;

namespace src.Data;

public class MonitoringService
{

    private List<ServiceMonitorSchema> _services;
    public DateTime LastChecked { get; private set; }
    private readonly Timer _timer;
    private int _counter;
    public event EventHandler ServiceStatusChanged;
    
    private readonly Config _config;
    private readonly IHttpClientFactory _clientFactory;
    private readonly Serilog.ILogger _logger;
    private readonly EmailService _emailService;



    public MonitoringService(IHttpClientFactory clientFactory, Serilog.ILogger logger, Config config, EmailService emailService)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _logger = logger;
        _config = config;
        _emailService = emailService;
        _counter = 60;
        _timer = new Timer();
        _timer.Interval = 1000;
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        // Subtract 1 from the counter
        _counter--;
        // If the counter is 0
        if (_counter != 0) return;
        // Reset the counter
        _counter = 60;
        // Check the services
        RunChecksAsync();
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
                        name = service.name,
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
        var emailString = _emailService.EmailBody(_services);
        if(emailString != "")
            _emailService.Send("Services are down", emailString);
        OnServiceStatusChanged();
    }

    // Only to be used when it is known the data has been updated 
    public List<ServiceMonitorSchema> GetMonitorDataSync()
    {
        return _services;
    }

    public async Task<List<ServiceMonitorSchema>> GetMonitorData()
    {
        if (_services is null)
            await RunChecksAsync();
        return _services;
    }
    
    public void SetIntervalMethod(Action<int> method)
    {
        _timer.Elapsed += (sender, args) =>
        {
            method(_counter);
        };
    }

    protected virtual void OnServiceStatusChanged()
    {
        ServiceStatusChanged?.Invoke(this, EventArgs.Empty);
    }
}