using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Proxy;
using Proxy.ServiceDefinitions;
using Proxy.Services;


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        // services
    services.AddSingleton<IEntrypoint, Entrypoint>()
        .AddSingleton<IListener, ListenerTCP>()
        .AddSingleton<IConnectionHandler, HandlerTCP>()
        .AddLogging())
    .Build();
    
    await host.StartAsync();


host.Services.GetRequiredService<IEntrypoint>().Run();

