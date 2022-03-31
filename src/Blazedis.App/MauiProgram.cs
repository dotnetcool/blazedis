using Blazedis.App.Data;
using Blazedis.App.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;
using MudBlazor.Services;

namespace Blazedis.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .RegisterBlazorMauiWebView()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddBlazorWebView().AddMudServices();

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<IRedisConfigurationService, RedisConfigurationService>();
            builder.Services.AddSingleton<IRedisConnectionService, RedisConnectionService>();

            return builder.Build();
        }
    }
}