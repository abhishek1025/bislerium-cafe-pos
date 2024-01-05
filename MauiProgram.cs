﻿using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using bislerium_cafe_pos.Services;

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
            builder.Services.AddMudServices();
          
            builder.Services.AddSingleton<UserServices>();
            builder.Services.AddSingleton<CoffeeServices>();
            builder.Services.AddSingleton<AddInItemsServices>();
            builder.Services.AddSingleton<OrderItemServices>();
            builder.Services.AddSingleton<OrderServices>();
            builder.Services.AddSingleton<CustomerService>();
            
            return builder.Build();
        }
    }
}
