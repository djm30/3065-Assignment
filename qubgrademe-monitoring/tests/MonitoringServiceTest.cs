using System.Reflection;
using FluentAssertions;
using Moq;
using Serilog;
using src.Data;
using src.Services;

namespace tests;

public class MonitoringServiceTest
{
    [Fact]
    public void Runs_Checks_And_Fires_Event()
    {
        // Arrange
        var mockHttpFactory = new Mock<IHttpClientFactory>();
        var mockConfig = new Config(Mock.Of<ILogger>());
        
        var services = mockConfig.GetType().GetProperty(nameof(mockConfig.Services), BindingFlags.Public | BindingFlags.Instance);
        services.SetValue(mockConfig, new List<ServiceSchema>()
        {
            new ServiceSchema()
            {
                expected_result = "1",
                name = "Name",
                urls = new List<string>()
                {
                    "http://localhost:5000/health"
                },
                query_string = "?module_1=3"
            },
        });
        
        
        
        var mockTimerService = new Mock<TimerService>();
        
        var monitoringService = new MonitoringService(mockHttpFactory.Object, Mock.Of<ILogger>(), mockConfig, mockTimerService.Object);
        
        // Act
        using var monitoredServices = monitoringService.Monitor();
        
        // Doesnt throw an exception
        monitoringService.Invoking(c => c.RunChecksAsync()).Should().NotThrowAsync();

        monitoringService.GetMonitorDataSync().Should().HaveCount(1);
        // Asserts it raises an event
        monitoredServices.Should().Raise("ServiceStatusChanged");


    }
    
    [Fact]
    public async void Doesnt_Run_Checks_If_Services_Isnt_Null_When_GetMonitorData_Is_Called()
    {
        // Arrange
        var mockHttpFactory = new Mock<IHttpClientFactory>();
        var mockConfig = new Mock<Config>(Mock.Of<ILogger>());
        var mockTimerService = new Mock<TimerService>();
        var monitoringService = new MonitoringService(mockHttpFactory.Object, Mock.Of<ILogger>(), mockConfig.Object, mockTimerService.Object);
        
        
        // get private services field of monitoring service and set it to null

        var emptyReturn = new List<ServiceMonitorSchema>();
        var services = monitoringService.GetType().GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        services.SetValue(monitoringService, emptyReturn);
        
        // Act
        using var monitoredServices = monitoringService.Monitor();
        
        // Doesnt throw an exception
        monitoringService.Invoking(c => c.RunChecksAsync()).Should().NotThrowAsync();
        
        // Asserts it raises an event
        monitoredServices.Should().NotRaise("ServiceStatusChanged");

        (await monitoringService.GetMonitorData()).Should().BeSameAs(emptyReturn);


    }
}