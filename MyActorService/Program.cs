using MyActorService.Actors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddActors(options =>
{
    // 註冊 actor types 並 configure actor 設定
    options.Actors.RegisterActor<MyActor>();
});

var app = builder.Build();

app.MapActorsHandlers();

app.Run();