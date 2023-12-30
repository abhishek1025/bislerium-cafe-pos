using bislerium_cafe_pos.Data;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using bislerium_cafe_pos.Utils;
using bislerium_cafe_pos.Services;
using bislerium_cafe_pos.Models;

namespace bislerium_cafe_pos
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<AppUtils>();
            builder.Services.AddSingleton<UserServices>();
            builder.Services.AddSingleton<User>();


            return builder.Build();
        }
    }
}
