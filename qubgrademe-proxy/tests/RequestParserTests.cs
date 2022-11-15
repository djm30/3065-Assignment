using FluentAssertions;
using Proxy.ServiceDefinitions;

namespace tests;
using Proxy.Services;
using Proxy.Models;
using Moq;

public class RequestParserTests
{
    [Fact]
    public void Testing_Valid_Route()
    {
        // Create a request parser instance
        var mockConfig = new Mock<IConfig>();
        mockConfig.Setup(x => x.GetRouteMap()).Returns(new Dictionary<string, string>()
        {
            { "/test", "http://127.0.0.1:5000" }
        });
        
        var requestParser = new RequestParser(mockConfig.Object);
        
        // Create a request
        var request = "GET /test HTTP/1.1\r\nHost: localhost:5000\r\n\r\n";
        
        // Parse the request
        var parsedRequestResult = requestParser.Parse(request);

        parsedRequestResult.Host.Host.Should().Be("127.0.0.1");
        parsedRequestResult.Host.Port.Should().Be(5000);
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Valid.Should().BeTrue();
        parsedRequestResult.Request.Should().NotBeNull();
    }
    
    [Fact]
    public void Testing_With_Multiple_Routes_And_Choosing_Most_Specific()
    {
        // Create a request parser instance
        var mockConfig = new Mock<IConfig>();
        mockConfig.Setup(x => x.GetRouteMap()).Returns(new Dictionary<string, string>()
        {
            { "/test", "http://127.0.0.1:5000" },
            { "/test/marks", "http://127.0.0.2:5000" }
        });
        
        var requestParser = new RequestParser(mockConfig.Object);
        
        // Create a request
        var request = "GET /test/marks HTTP/1.1\r\nHost: localhost:5000\r\n\r\n";
        
        // Parse the request
        var parsedRequestResult = requestParser.Parse(request);

        parsedRequestResult.Host.Host.Should().Be("127.0.0.2");
        parsedRequestResult.Host.Port.Should().Be(5000);
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Valid.Should().BeTrue();
        parsedRequestResult.Request.Should().NotBeNull();
    }
    
    [Fact]
    public void Testing_With_Multiple_Routes_And_Choosing_Least_Specific_Also_Appends_Rest_Of_Path()
    {
        // Create a request parser instance
        var mockConfig = new Mock<IConfig>();
        mockConfig.Setup(x => x.GetRouteMap()).Returns(new Dictionary<string, string>()
        {
            { "/test", "http://127.0.0.1:5000" },
            { "/test/marks", "http://127.0.0.2:5000" }
        });
        
        var requestParser = new RequestParser(mockConfig.Object);
        
        // Create a request
        var request = "GET /test/shouldbeonhost HTTP/1.1\r\nHost: localhost:5000\r\n\r\n";
        
        // Parse the request
        var parsedRequestResult = requestParser.Parse(request);

        parsedRequestResult.Host.Host.Should().Be("127.0.0.1");
        parsedRequestResult.Host.Port.Should().Be(5000);
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Valid.Should().BeTrue();
        parsedRequestResult.Request.Should().NotBeNull();
        parsedRequestResult.Host.LocalPath.Should().Be("/shouldbeonhost");
    }
    
    [Fact]
    public void Query_Params_Get_Added()
    {
        // Create a request parser instance
        var mockConfig = new Mock<IConfig>();
        mockConfig.Setup(x => x.GetRouteMap()).Returns(new Dictionary<string, string>()
        {
            { "/test", "http://127.0.0.1:5000" },
        });
        
        var requestParser = new RequestParser(mockConfig.Object);
        
        // Create a request
        var request = "GET /test?hello=1 HTTP/1.1\r\nHost: localhost:5000\r\n\r\n";
        
        // Parse the request
        var parsedRequestResult = requestParser.Parse(request);

        parsedRequestResult.Host.Host.Should().Be("127.0.0.1");
        parsedRequestResult.Host.Port.Should().Be(5000);
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Reload.Should().BeFalse();
        parsedRequestResult.Valid.Should().BeTrue();
        parsedRequestResult.Request.Should().NotBeNull();
        parsedRequestResult.Host.Query.Should().Be("?hello=1");
    }
    
    [Fact]
    public void No_Matching_Route_Returns_False()
    {
        // Create a request parser instance
        var mockConfig = new Mock<IConfig>();
        mockConfig.Setup(x => x.GetRouteMap()).Returns(new Dictionary<string, string>()
        {
            { "/test", "http://127.0.0.1:5000" },
        });
        
        var requestParser = new RequestParser(mockConfig.Object);
        
        // Create a request
        var request = "GET /nope HTTP/1.1\r\nHost: localhost:5000\r\n\r\n";
        
        // Parse the request
        var parsedRequestResult = requestParser.Parse(request);
        
        
        parsedRequestResult.Valid.Should().BeFalse();
    }
    
    [Fact]
    public void Health_Check_Should_Set_Health_Property_To_True()
    {
        // Create a request parser instance
        var mockConfig = new Mock<IConfig>();
        mockConfig.Setup(x => x.GetRouteMap()).Returns(new Dictionary<string, string>()
        {
            { "/test", "http://127.0.0.1:5000" },
        });
        
        var requestParser = new RequestParser(mockConfig.Object);
        
        // Create a request
        var request = "GET /health HTTP/1.1\r\nHost: localhost:5000\r\n\r\n";
        
        // Parse the request
        var parsedRequestResult = requestParser.Parse(request);


        parsedRequestResult.HealthCheck.Should().BeTrue();
        parsedRequestResult.Valid.Should().BeTrue();
        parsedRequestResult.Reload.Should().BeFalse();
    }
    
    [Fact]
    public void Reload_Should_Set_Reload_Property_To_True()
    {
        // Create a request parser instance
        var mockConfig = new Mock<IConfig>();
        mockConfig.Setup(x => x.GetRouteMap()).Returns(new Dictionary<string, string>()
        {
            { "/test", "http://127.0.0.1:5000" },
        });
        
        var requestParser = new RequestParser(mockConfig.Object);
        
        // Create a request
        var request = "GET /reload HTTP/1.1\r\nHost: localhost:5000\r\n\r\n";
        
        // Parse the request
        var parsedRequestResult = requestParser.Parse(request);


        parsedRequestResult.Reload.Should().BeTrue();
        parsedRequestResult.Valid.Should().BeTrue();
        parsedRequestResult.HealthCheck.Should().BeFalse();
    }
}