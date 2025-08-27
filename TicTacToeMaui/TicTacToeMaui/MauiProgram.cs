using Microsoft.Extensions.Logging;

namespace TicTacToeMaui
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
                    fonts.AddFont("MountainsofChristmas-Bold.ttf", "MountainsofChristmas-Bold");
                    fonts.AddFont("MountainsofChristmas-Regular.ttf", "MountainsofChristmas-Regular");
                });

            return builder.Build();
        }
    }
}
