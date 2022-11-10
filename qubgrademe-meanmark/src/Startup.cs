


using Assignment2.MeanMark.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(Assignment2.MeanMark.Startup))]

namespace Assignment2.MeanMark
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IValidator, Validator>();
            builder.Services.AddSingleton<IMeanService, MeanService>();
        }
    }
}
