using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using qubgrademe_statefulsaving.Models;

namespace qubgrademe_statefulsaving;

public static class Health
{
    [FunctionName("Health")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req, ILogger log)
    {
        log.LogInformation("Health Check Request Received");

        return new OkObjectResult(new HealthResponse()
        {
            message = "Ok",
        });

    }
}