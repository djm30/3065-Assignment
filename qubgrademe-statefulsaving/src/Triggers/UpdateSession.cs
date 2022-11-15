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

public static class UpdateSession
{
    [FunctionName("UpdateSession")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        // Get the session ID from the query string
        var sessionId = req.Query["sessionId"];
        // If no sessionId return bad request
        if (string.IsNullOrEmpty(sessionId))
        {
            return new BadRequestObjectResult("Please pass a sessionId on the query string or in the request body");
        }
        
        // Get the body as a string
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        
        await using var service = SqlConnectionService.GetConnection();
        try
        {
            var sql = "UPDATE [dbo].[Data] SET Data = @RequestBody WHERE SessionID = @SessionId";
            await service.ExecuteAsync(sql,
                new {SessionId = sessionId, RequestBody = requestBody});
            return new OkResult();

        }
        catch (Exception e)
        { 
            log.LogError(e, "Error failed executing query");
            return new BadRequestObjectResult("Error failed executing query");
        }
        // Update the data field in the database with the request body
        
        
    }
}