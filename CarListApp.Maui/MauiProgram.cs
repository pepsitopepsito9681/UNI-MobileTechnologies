using CarListApp.Maui.Services;
using CarListApp.Maui.ViewModels;

using CarListApp.Maui.Views;


using CarListApp.Maui.Views;



using Microsoft.Extensions.Logging;

namespace CarListApp.Maui
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db1");

            builder.Services.AddSingleton(s=> ActivatorUtilities.CreateInstance<CarService>(s, dbPath));

            builder.Services.AddSingleton<CarService>();



            builder.Services.AddSingleton<CarListViewModel>();
            builder.Services.AddTransient<CarDetailsViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<CarDetailsPage>();



            builder.Services.AddSingleton<CarListViewModel>();
            builder.Services.AddSingleton<MainPage>();



            return builder.Build();
        }
    }
}
