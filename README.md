# Weather '21, running in the browser with MauiWeb

[Weather '21](https://github.com/dotnet/maui-samples/tree/main/10.0/Apps/WeatherTwentyOne) is the .NET MAUI
sample from Microsoft that shows a responsive UI adapting to desktop and mobile (MIT). This repository is the
same app, moved to **.NET 11 / .NET MAUI 11.0.0**, with one extra project that serves it as a website through
[MauiWeb](https://mauiweb.appmare.com). The Shell, pages, widgets, styles and view models are untouched: the
browser gets real HTML rendered on the server, and every tap runs the app's own C#.

| Home (desktop width) | Favorites | Phone width |
| --- | --- | --- |
| ![Home](docs/weather-home.png) | ![Favorites](docs/weather-favorites.png) | ![Phone](docs/weather-phone.png) |

## Run it

Requirements: the .NET 11 SDK and the .NET MAUI 11 workload (`dotnet workload install maui`).

```bash
dotnet run --project src/WeatherTwentyOne.Web
```

Open <http://localhost:5323>. MauiWeb is free on localhost, `.local`, `.test` and private hosts; a public
domain needs a licence (see the [pricing](https://mauiweb.appmare.com)).

The phone and desktop heads are still there: `src/WeatherTwentyOne` targets `net11.0-android`, `net11.0-ios`,
`net11.0-maccatalyst` (and Windows on Windows) as before.

## What changed from the original

1. **`Directory.Build.props`** — `MauiVersion` 10.0.60 → 11.0.0, `DotNetVersion` 10.0.7 → 11.0.0.
2. **`src/WeatherTwentyOne/WeatherTwentyOne.csproj`** — targets moved from `net10.0-*` to `net11.0-*` and a
   plain `net11.0` target added (built as a library, so the web host can reference it).
3. **`src/WeatherTwentyOne/MauiProgram.cs`** — `CreateMauiApp` takes an optional `Action<MauiAppBuilder>` so a
   host can add its own setup. The phone heads call it with no argument, as before.
4. **`src/WeatherTwentyOne/Services/ServiceExtensions.cs`** — the sample's static service locator only knew
   the Windows, Android and Apple hosts and returned `null` elsewhere, which threw on the first page. It now
   falls back to `IPlatformApplication.Current?.Services` and `GetService` is null-safe. The tray icon and
   notifications stay Windows/macOS-only, as in the original.
5. **`src/WeatherTwentyOne.Web/`** (new) — an empty ASP.NET Core project with the `AppMare.MauiWeb` package and
   one line in `Program.cs`:

   ```csharp
   app.MapMauiWeb(() => MauiProgram.CreateMauiApp(b => b.UseMauiWeb()));
   ```

   plus an item group that copies the app's `Resources/` (fonts, images) next to the web host.

## Licence

The sample is MIT, © Microsoft (see [LICENSE](LICENSE)); the original readme is kept as
[README.upstream.md](README.upstream.md). The web host project added here is MIT too. MauiWeb itself is a
commercial package by [AppMare](https://appmare.com), free for local development.
