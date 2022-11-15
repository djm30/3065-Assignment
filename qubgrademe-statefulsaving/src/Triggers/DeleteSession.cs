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

namespace qubgrademe_statefulsaving;

public static class DeleteSession
{
    [FunctionName("DeleteSession")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req, ILogger log)
    {
        // Get session id from query
        string sessionId = req.Query["sessionId"];
        if (string.IsNullOrEmpty(sessionId))
        {
            return new BadRequestObjectResult("Please pass a sessionId on the query string or in the request body");
        }


        await using var service = SqlConnectionService.GetConnection();
        try
        {
            var sql = "DELETE FROM [dbo].[Data] WHERE SessionId = @SessionId";
            await service.ExecuteAsync(sql, new { SessionId = sessionId });
            return new OkResult();
        }
        catch (Exception e)
        {
            log.LogError(e, "Error deleting session");
            return new BadRequestResult();
        }
    }
}