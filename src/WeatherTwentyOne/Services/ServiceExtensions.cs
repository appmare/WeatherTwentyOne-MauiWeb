namespace WeatherTwentyOne;

public static class ServiceProvider
{
    public static TService GetService<TService>()
        => Current is { } services ? services.GetService<TService>() : default;

    public static IServiceProvider Current
        =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
			MauiUIApplicationDelegate.Current.Services;
#else
			// Other hosts (the MauiWeb web host): the app's own service provider.
			IPlatformApplication.Current?.Services;
#endif
}
