using MeldPuntIncidentApp.Services;
using MeldPuntIncidentApp.View;
using MeldPuntIncidentApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace MeldPuntIncidentApp;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		//SERVICES
		builder.Services.AddSingleton<IIncidentService, IncidentService>();


        //VIEWMODELS
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MapViewModel>();


		//PAGES
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MapPage>();
		

        return builder.Build();
	}
}
