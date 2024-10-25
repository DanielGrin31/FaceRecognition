using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FaceRecognition.UI.Wasm;
using FaceRecognition.UI.ClassLib;
using FaceRecognition.UI.ClassLib.Models;
using FaceRecognition.UI.ClassLib.Utilities;
using Blazored.LocalStorage;
using FaceRecognition.UI.ClassLib.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add services to the container.
ApiSettings apiSettings = new ApiSettings();
builder.Configuration.GetSection("ApiSettings").Bind(apiSettings);
builder.Services.AddSingleton(apiSettings);
builder.Services.AddSingleton<MainLayoutViewModel>();
builder.Services.RegisterAPI(apiSettings);
builder.Services.AddBlazoredLocalStorage();


var app=builder.Build();


await app.RunAsync();