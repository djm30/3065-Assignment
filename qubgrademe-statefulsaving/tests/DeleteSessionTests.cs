using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using qubgrademe_statefulsaving;

namespace tests;

public class DeleteSessionTests
{
    [Fact]
    public async void Returns_Bad_Object_When_SessionId_Query_Param_Isnt_Present()
    {
        var moqRequest = new Mock<HttpRequest>();
        moqRequest.SetupGet(x => x.Query).Returns(new QueryCollection());
        var result = await DeleteSession.RunAsync(moqRequest.Object, new Mock<ILogger>().Object);

        result.Should().BeOfType<BadRequestObjectResult>();
    }
}