using WeatherTwentyOne;
using MauiWeb.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// The same MAUI app as the phone heads, served as a website by MauiWeb.
app.MapMauiWeb(() => MauiProgram.CreateMauiApp(b => b.UseMauiWeb()), options =>
{
    options.Title = "Weather '21";
});

app.Run();
