using System;
using System.Collections.Generic;
using Assignment2.MeanMark.Services;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.MeanMark.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Int32;

namespace Assignment2.MeanMark.Triggers;

public class MeanMark
{
    private  readonly IValidator _validator;
    private  readonly IMeanService _meanService;

    public MeanMark(IValidator validator, IMeanService meanService)
    {
        _validator = validator;
        _meanService = meanService;
    }
    
    [FunctionName("MeanMark")]
    public  async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "mean")] HttpRequest req, ILogger log)
    {
        log.LogInformation("Request received for mean mark");

        if (!req.QueryString.HasValue)
        {
            return new BadRequestObjectResult(new Response
            {
                error = true,
                errorMessage = "Please provide some module codes and their respective marks",
                modules = new List<string>() { "", "", "", "", "" },
                marks = new List<int>() { 0, 0, 0, 0, 0 },
                mean = 0
            });
            log.LogError("No query params provided");
        }

        var module_1 = req.Query["module_1"];
        var module_2 = req.Query["module_2"];
        var module_3 = req.Query["module_3"];
        var module_4 = req.Query["module_4"];
        var module_5 = req.Query["module_5"];
        var mark_1 = req.Query["mark_1"];
        var mark_2 = req.Query["mark_2"];
        var mark_3 = req.Query["mark_3"];
        var mark_4 = req.Query["mark_4"];
        var mark_5 = req.Query["mark_5"];

        var modules = new List<string?>() { module_1, module_2, module_3, module_4, module_5 };
        var marks = new List<string?>() { mark_1, mark_2, mark_3, mark_4, mark_5 };

        var validationResult = _validator.Validate(modules, marks);
        
        var mean = 0.0;
        
        if (!validationResult.error)
            mean = _meanService.CalculateMean(marks);
        else log.LogError(validationResult.errorMessage);

        var response = new Response
        {
            error = validationResult.error,
            errorMessage = validationResult.errorMessage,
            modules = modules.Select(x => x is null ? "" : x).ToList(),
            marks = marks.Select(x => TryParse(x, out var mark) ? mark : 0).ToList(),
            mean = mean
        };

        return !validationResult.error
            ? new OkObjectResult(response)
            : new BadRequestObjectResult(response);

    }
}