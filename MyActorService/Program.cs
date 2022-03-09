using Dapr.Actors;
using MyActorService;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddActors(options =>
{
    // 註冊 actor type 並 configure actor 設定
    options.Actors.RegisterActor<MyActor>();

    // Configure default settings
    options.ActorIdleTimeout = TimeSpan.FromMinutes(10);
    options.ActorScanInterval = TimeSpan.FromSeconds(30);
    options.DrainOngoingCallTimeout = TimeSpan.FromSeconds(60);
    options.DrainRebalancedActors = true;
    options.RemindersStoragePartitions = 7;
    // reentrancy 在 .NET SDK 還是 preview 階段
    options.ReentrancyConfig = new ActorReentrancyConfig { Enabled = true, MaxStackDepth = 3 };
    options.JsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
});

var app = builder.Build();
// Actors building block does not support HTTPS redirection.
//app.UseHttpsRedirection();
app.UseRouting();

app.MapActorsHandlers();

app.Run();