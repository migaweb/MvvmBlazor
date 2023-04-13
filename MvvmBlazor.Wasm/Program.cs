using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MvvmBlazor.Wasm;
using MvvmBlazor.Wasm.Models;
using MvvmBlazor.Wasm.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IFetchDataViewModel, FetchDataViewModel>();
builder.Services.AddTransient<IFetchDataModel, FetchDataModel>();

await builder.Build().RunAsync();
