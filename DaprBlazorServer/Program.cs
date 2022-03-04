using Dapr.Actors;
using DaprBlazorServer.Actors;
using DaprBlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddActors(options =>
{
    // 註冊 actor type 並 configure actor 設定
    options.Actors.RegisterActor<MyActor3>();

    // Configure default settings
    options.ActorIdleTimeout = TimeSpan.FromMinutes(20);
    options.ActorScanInterval = TimeSpan.FromSeconds(30);
    options.DrainOngoingCallTimeout = TimeSpan.FromSeconds(60);
    options.DrainRebalancedActors = true;
    options.RemindersStoragePartitions = 7;
    // reentrancy 在 .NET SDK 還是 preview 階段
    options.ReentrancyConfig = new ActorReentrancyConfig { Enabled = true, MaxStackDepth = 3 };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
