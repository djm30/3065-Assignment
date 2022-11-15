using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Proxy;
using Proxy.Models;
using Proxy.ServiceDefinitions;
using Proxy.Services;


using IHost host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(app =>
    {
        // app.AddJsonFile("appsettings.json" ,optional: false, reloadOnChange:true);
    })
    .ConfigureServices((configuration, services) =>
        // services
    services.AddSingleton<IEntrypoint, Entrypoint>()
        .AddSingleton<IListener, Listener>()
        .AddSingleton<IConnectionHandler, Handler>()
        .AddSingleton<ICommandLineParser, CommandLineParser>()
        .AddSingleton<IRequestParser, RequestParser>()
        .AddSingleton<IRequestMaker, RequestMaker>()
        .AddSingleton<IConfig, Config>()
        .AddSingleton<IResponseBuilder, ResponseBuilder>()
        .AddLogging()
    )
    .Build();
    
    await host.StartAsync();

host.Services.GetRequiredService<IConfig>().LoadSettings();
await host.Services.GetRequiredService<IEntrypoint>().Run();

