using FaceRecognition.UI.ClassLib;
using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;


var host = CreateHostBuilder(args).Build();
var app=host.Services.GetRequiredService<Application>();
await app.StartAsync();

static IConfiguration Configure()
{
    IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
    return configuration;
}
static IHostBuilder CreateHostBuilder(string[] args)
{
    var configuration=Configure();
    return Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        ConfigureServices(services,configuration);
    });
}

static void ConfigureServices(IServiceCollection services,IConfiguration configuration)
{
    ApiSettings apiSettings = new ApiSettings();
    configuration.GetSection("ApiSettings").Bind(apiSettings);
    services.RegisterAPI(apiSettings);
    services.AddSingleton<Application>();
}
