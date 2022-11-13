using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog;
using src.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<MonitoringService>();
builder.Services.AddSingleton<Config>();
builder.Services.AddHttpClient();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


var config = app.Services.GetService<Config>();
await config.LoadSettings();

Task.Run(async () =>
{
    Console.WriteLine("Type /help for a list of available commands");
    while (true)
    {
        var command = Console.ReadLine();
        switch (command)
        {
            case "/reload":
                await config.LoadSettings();
                Console.WriteLine("Settings Reloaded!");
                break;
            case "/list":
                Console.WriteLine(config.PrintConfig());
                break;
            case "/help":
                Console.WriteLine("{0,-8} -   Reloads the config file into memory after it has been changed", "/reload");
                Console.WriteLine("{0,-8} -   Lists all information of the currently stored services", "/list");
                Console.WriteLine("{0,-8} -   Exits the program", "/exit");
                Console.WriteLine("{0,-8} -   Lists all the available commands\n", "/help");
                break;
            case "/exit":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Please enter a valid command");
                break;
        }
    }
});

app.Run();