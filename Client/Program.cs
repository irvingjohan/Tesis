using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RIEGO.Client.Repositorios;
//using RIEGO.Client.Helpers;
using Radzen;
using Blazored;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;


namespace RIEGO.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddAuthorizationCore();//login
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();//login
            builder.Services.AddScoped<AuthenticationStateProvider>(op => op.GetRequiredService<CustomAuthenticationStateProvider>());//login
            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IRepositorio, Repositorio>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddBlazoredSessionStorage();
            await builder.Build().RunAsync().ConfigureAwait(true);
        }
    }
}
