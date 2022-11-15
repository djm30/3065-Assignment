using System.Configuration;
using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Proxy.Models;
using Proxy.Services;

using ConfigurationException = Proxy.Exceptions.ConfigurationException;
namespace tests;

public class ConfigTests
{
    [Fact]
    public void ValidateConfig_Doesnt_Throw_For_Correct_Config()
    {
        var Config = new Config(new Mock<ILogger<Config>>().Object);
        
        // Get private ValidateConfig function
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);

        var configValue = new ConfigSchema()
        {
            Port = 9000,
            IpAddress = "localhost",
            Routes = new List<RouteSchema>()
            {
                new RouteSchema()
                {
                    Route = "/test",
                    Destination = "http://localhost:9001"
                }
            }
        };
        
        method.Invoking(x => x.Invoke(Config, new object[] {configValue})).Should().NotThrow();
    }
    
    [Fact]
    public void ValidateConfig_Does_Throw_With_Port_Below_1024()
    {
        var Config = new Config(new Mock<ILogger<Config>>().Object);
        
        // Get private ValidateConfig function
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);

        var configValue = new ConfigSchema()
        {
            Port = 1022,
            IpAddress = "localhost",
            Routes = new List<RouteSchema>()
            {
                new RouteSchema()
                {
                    Route = "/test",
                    Destination = "http://localhost:9001"
                }
            }
        };
        
        method.Invoking(x => x.Invoke(Config, new object[] {configValue})).
            Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide a port in the range 1024 - 65535");
    }
    [Fact]
    
    public void ValidateConfig_Does_Throw_With_Port_Over_65535()
    {
        var Config = new Config(new Mock<ILogger<Config>>().Object);
        
        // Get private ValidateConfig function
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);

        var configValue = new ConfigSchema()
        {
            Port = 65536,
            IpAddress = "localhost",
            Routes = new List<RouteSchema>()
            {
                new RouteSchema()
                {
                    Route = "/test",
                    Destination = "http://localhost:9001"
                }
            }
        };
        
        method.Invoking(x => x.Invoke(Config, new object[] {configValue})).
            Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide a port in the range 1024 - 65535");
    }
    
    [Fact]
    public void ValidateConfig_Does_Throw_With_Invalid_IP()
    {
        var Config = new Config(new Mock<ILogger<Config>>().Object);
        
        // Get private ValidateConfig function
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);

        var configValue = new ConfigSchema()
        {
            Port = 65532,
            IpAddress = "fdsadfaadsfad",
            Routes = new List<RouteSchema>()
            {
                new RouteSchema()
                {
                    Route = "/test",
                    Destination = "http://localhost:9001"
                }
            }
        };
        
        method.Invoking(x => x.Invoke(Config, new object[] {configValue})).
            Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide a valid IP Address");
    }
    
    [Fact]
    public void ValidateConfig_Does_Throw_With_No_Routes()
    {
        var Config = new Config(new Mock<ILogger<Config>>().Object);
        
        // Get private ValidateConfig function
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);

        var configValue = new ConfigSchema()
        {
            Port = 65532,
            IpAddress = "127.0.0.1",
            Routes = new List<RouteSchema>()
            {
            }
        };
        
        method.Invoking(x => x.Invoke(Config, new object[] {configValue})).
            Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide some routes");
    }
    
    [Fact]
    public void ValidateConfig_Does_Throw_With_Invalid_Destination()
    {
        var Config = new Config(new Mock<ILogger<Config>>().Object);
        
        // Get private ValidateConfig function
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);

        var configValue = new ConfigSchema()
        {
            Port = 65532,
            IpAddress = "127.0.0.1",
            Routes = new List<RouteSchema>()
            {
                new RouteSchema()
                {
                    Route = "/route",
                    Destination = "dsajknfdkljdfs"
                }
            }
        };
        
        method.Invoking(x => x.Invoke(Config, new object[] {configValue})).
            Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide a valid http or https address for the destination: at dsajknfdkljdfs");
    }
}