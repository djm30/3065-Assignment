using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sql;
using Newtonsoft.Json;
using Dapper;


namespace qubgrademe_statefulsaving;

public static class RetrieveSession
{
    [FunctionName("RetrieveSession")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
    {
        log.LogInformation("Processing request for retrieving session storage.");
        
        var sessionId = req.Query["sessionId"];
        // If no sessionId return bad request
        if (string.IsNullOrEmpty(sessionId))
        {
            return new BadRequestObjectResult("Please pass a sessionId on the query string or in the request body");
        }

        await using var service = SqlConnectionService.GetConnection();
        try
        {
            var sql = "SELECT * FROM [dbo].[Data] WHERE [SessionId] = @SessionId";
            var result = await service.QueryAsync<DatabaseSchema>(sql,
                new { SessionId = req.Query["sessionId"] });
            var row = result.First();
            var data = JsonConvert.DeserializeObject(row.Data);
            return new OkObjectResult(data);
        }
        catch (JsonReaderException e)
        {
            log.LogError(e, "Error occurred deserializing JSON");
            return new BadRequestObjectResult(e.Message);
        }
        catch(InvalidOperationException ex)
        {
            return new NotFoundResult();
        }
        catch(Exception ex)
        {
            log.LogError(ex, "Error retrieving session");
            return new BadRequestObjectResult(ex.Message);
        }
    }
}