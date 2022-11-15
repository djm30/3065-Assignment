using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;
using src.Data;
using src.Services;
using ILogger = Serilog.ILogger;
using FluentAssertions;
using src.Exceptions;

namespace tests;

public class ConfigTest
{
    [Fact]
    public void Doesnt_Throw_When_Everything_Is_Valid()
    {
        // Arrange
        var serviceList = new List<ServiceSchema>()
        {
            new ServiceSchema()
            {
                expected_result = "",
                name = "Name",
                urls = new List<string>()
                {
                    "http://localhost:5000/health"
                },
                query_string = "?module_1=3"
            },
        };
        
        var config = new Config(Mock.Of<ILogger>());

        // Act

        // Getting private method
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);
        

        // Assert
        method.Invoking(c => c.Invoke(config, new object[] {serviceList})).Should().NotThrow();
    }
    
    [Fact]
    public void Throws_Exception_When_No_Services_Are_Provided()
    {
        // Arrange
        var serviceList = new List<ServiceSchema>() { };
        
        var config = new Config(Mock.Of<ILogger>());

        // Act

        // Getting private method
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);
        

        // Assert
        method.Invoking(c => c.Invoke(config, new object[] {serviceList})).Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide some services in the config file!");
        
    }

    
    [Fact]
    public void Throws_Exception_When_No_Expected_Value_Is_Provided()
    {
        // Arrange
        var serviceList = new List<ServiceSchema>()
        {
            new ServiceSchema()
            {
                expected_result = null,
                name = "Name",
                urls = new List<string>()
                {
                    "http://localhost:5000/health"
                },
                query_string = "?module_1=3"
            },
        };
        
        var config = new Config(Mock.Of<ILogger>());

        // Act

        // Getting private method
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);
        

        // Assert
        method.Invoking(c => c.Invoke(config, new object[] {serviceList})).Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide the raw data for the expected result of the service");
    }
    
    [Fact]
    public void Throws_Exception_When_No_Url_Is_Provided()
    {
        // Arrange
        var serviceList = new List<ServiceSchema>()
        {
            new ServiceSchema()
            {
                expected_result = "OK",
                name = "Name",
                urls = new List<string>(){},
                query_string = "?module_1=3"
            },
        };
        
        var config = new Config(Mock.Of<ILogger>());

        // Act

        // Getting private method
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);
        

        // Assert
        method.Invoking(c => c.Invoke(config, new object[] {serviceList})).Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide a URL for the service. (In an array)");
    }
    
    [Fact]
    public void Throws_Exception_When_No_Name_Is_Provided()
    {
        // Arrange
        var serviceList = new List<ServiceSchema>()
        {
            new ServiceSchema()
            {
                expected_result = "OK",
                name = "",
                urls = new List<string>()
                {
                    "http://localhost:5000/health"
                },
                query_string = "?module_1=3"
            },
        };
        
        var config = new Config(Mock.Of<ILogger>());

        // Act
        // Assert the follwoing code doesnt throw an error
        
        // Call the private ValidateConfig method
        var method = typeof(Config).GetMethod("ValidateConfig", BindingFlags.NonPublic | BindingFlags.Instance);
        

        // Assert
        method.Invoking(c => c.Invoke(config, new object[] {serviceList})).Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<ConfigurationException>()
            .WithMessage("Please provide a name for every service");
    }
}