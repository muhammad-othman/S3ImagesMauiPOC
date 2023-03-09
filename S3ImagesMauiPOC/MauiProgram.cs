using S3ImagesMauiPOC.ViewModels;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace S3ImagesMauiPOC;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("S3ImagesMauiPOC.appsettings.json");

        stream.Position = 0;
        builder.Configuration.AddJsonStream(stream);

        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();


        builder.Services.AddTransient<BucketsPage>();
        builder.Services.AddTransient<BucketsViewModel>();
        builder.Services.AddTransient<BucketPage>();
        builder.Services.AddTransient<BucketViewModel>();

        return builder.Build();
	}
}
