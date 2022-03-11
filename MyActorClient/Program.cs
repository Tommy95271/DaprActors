using Dapr.Actors;
using Dapr.Actors.Client;
using MyActorInterfaces;

Console.WriteLine("Startup up...");

// 在 Actor Service 註冊 Actor Type
var actorType = "MyActor";

for (int i = 0; i < 10; i++)
{
    // ActorId 用來辨認 actor 實體
    // 如果這個 id 的 actor 不存在就會建立一個
    var actorId = new ActorId($"actor{i}");

    // 利用跟 service 一樣的介面建立本地 proxy
    //
    // 要提供 actor type 跟 id 才能找到是哪個 actor 實體
    var proxy = ActorProxy.Create<IMyActor>(actorId, actorType);

    // 接下來就能用 actor 介面定義的方法去呼叫 actor 的實作方法
    Console.WriteLine($"Calling SetDataAsync on {actorType}:{actorId}...");
    var response = await proxy.SetDataAsync(new MyData()
    {
        PropertyA = "ValueA",
        PropertyB = "ValueB",
    });
    Console.WriteLine($"Got response: {response}");

    Console.WriteLine($"Calling GetDataAsync on {actorType}:{actorId}...");
    var savedData = await proxy.GetDataAsync();
    Console.WriteLine($"Got response: {response}");
    Console.WriteLine($"Got response: {savedData}");
    // 一旦註冊，就算中止 service 重啟，reminder 還是存在，必須呼叫 UnregisterReminder() 取消註冊
    //await proxy.RegisterReminder();

    Thread.Sleep(2000);
}