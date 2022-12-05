using System;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using qubgrademe_statefulsaving.Properties;

namespace qubgrademe_statefulsaving;

public static class CreateSession
{
    [FunctionName("CreateSession")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
    {
        try
        {
            await using var service = SqlConnectionService.GetConnection();
            var guid = Guid.NewGuid();
            var sql = $"INSERT INTO dbo.Data (SessionId, Data) VALUES ('{guid}', '')";
            await service.ExecuteAsync(sql);

            return new OkObjectResult(new CreateSessionSchema()
            {
                SessionId = guid
            });
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}