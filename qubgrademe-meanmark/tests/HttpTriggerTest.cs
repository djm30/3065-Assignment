using Assignment2.MeanMark.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using Assignment2.MeanMark.Triggers;
using Assignment2.MeanMark.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace tests;

public class HttpTriggerTest
{
    [Fact]
    public async void Returns_200_With_Valid_Request()
    {
        // Creating moq http request and providing query string parameters
        var reqMock = new Mock<HttpRequest>();
        reqMock.Setup(x => x.QueryString)
            .Returns(new QueryString("?not empty to pass validation"));
        reqMock.Setup(x => x.Query).Returns(new QueryCollection(new Dictionary<string, StringValues>
        {
            {"module_1", "Module one"},
            {"module_2", "Module two"},
            {"module_3", "Module three"},
            {"module_4", "Module four"},
            {"module_5", "Module five"},
            {"mark_1", "50"},
            {"mark_2", "50"},
            {"mark_3", "50"},
            {"mark_4", "50"},
            {"mark_5", "50"},
        }));

        var meanMarkClass = new MeanMark(new Validator(), new MeanService());

        var result = await meanMarkClass.RunAsync(reqMock.Object, new Mock<ILogger>().Object);

        var objectResult = (OkObjectResult)result;
        var resBody = (Response)objectResult.Value;
        
        Assert.Equal(200, objectResult.StatusCode);
        Assert.False(resBody.error);
        Assert.Equal("", resBody.errorMessage);
        Assert.Equal(new List<string>(){"Module one", "Module two", "Module three", "Module four", "Module five"}, resBody.modules);
        Assert.Equal(new List<int>(){50, 50,50,50,50}, resBody.marks);
        Assert.Equal(50, resBody.mean);
    }
    
    [Fact]
    public async void Returns_400_With_Invalid_Request()
    {
        // Creating moq http request and providing query string parameters
        var reqMock = new Mock<HttpRequest>();
        reqMock.Setup(x => x.QueryString)
            .Returns(new QueryString("?not empty to pass validation"));
        reqMock.Setup(x => x.Query).Returns(new QueryCollection(new Dictionary<string, StringValues>
        {
            {"module_1", "Module one"},
            {"module_2", "Module two"},
            {"module_3", "Module three"},
            {"module_4", "Module four"},
            {"module_5", "Module five"},
            {"mark_1", "50"},
            {"mark_2", "50"},
            {"mark_3", "50"},
            {"mark_4", "50"},
            {"mark_5", ""},
        }));

        var meanMarkClass = new MeanMark(new Validator(), new MeanService());

        var result = await meanMarkClass.RunAsync(reqMock.Object, new Mock<ILogger>().Object);

        var objectResult = (BadRequestObjectResult)result;
        var resBody = (Response)objectResult.Value;
        
        Assert.Equal(400, objectResult.StatusCode);
        Assert.True(resBody.error);
        Assert.Equal("Please provide a valid integer for every entered module", resBody.errorMessage);
        Assert.Equal(new List<string>(){"Module one", "Module two", "Module three", "Module four", "Module five"}, resBody.modules);
        Assert.Equal(new List<int>(){50, 50,50,50,0}, resBody.marks);
        Assert.Equal(0, resBody.mean);
    }

    [Fact]
    public async void Health_Check_Returns_200()
    {
        // Creating moq http request and providing query string parameters
        var reqMock = new Mock<HttpRequest>();
        
        var result = await Health.RunAsync(reqMock.Object, new Mock<ILogger>().Object);

        var objectResult = (OkObjectResult)result;
        var resBody = (HealthResponse)objectResult.Value;
        
        Assert.Equal(200, objectResult.StatusCode);
        Assert.Equal(resBody.message, "OK");
    }
}