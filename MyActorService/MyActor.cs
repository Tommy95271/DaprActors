using Dapr.Actors.Runtime;
using MyActorInterfaces;

namespace MyActorService
{
    public class MyActor : Actor, IMyActor, IRemindable
    {
        // 建構子必須用 ActorHost 當參數，也可以用額外的參數，只要用 DI 就可
        //
        /// <summary>
        /// 初始化 MyActor 實體
        /// </summary>
        /// <param name="host">Dapr.Actors.Runtime.ActorHost 會 host actor 實體</param>
        public MyActor(ActorHost host)
            : base(host)
        {
        }

        /// <summary>
        /// 當一個 actor 被啟動時會呼叫這方法
        /// </summary>
        protected override Task OnActivateAsync()
        {
            // 實作你想做的事情
            Console.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} Activating actor id: {Id}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 當一個 actor 一段時間沒活動被停用會呼叫這方法
        /// </summary>
        protected override Task OnDeactivateAsync()
        {
            // 實作你想做的事情
            Console.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} Deactivating actor id: {Id}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 把 MyData 塞到 actor 的 state store
        /// </summary>
        /// <param name="data">user 定義的 MyData 會存在 state store 的 "my_data" state</param>
        public async Task<string> SetDataAsync(MyData data)
        {
            // 每個方法透過 Actor runtime 執行後，資料會自動被存到 state store
            // 如果要明確存進去可以呼叫 StateManager.SaveStateAsync()
            // 存進去的資料必須可序列化
            await this.StateManager.SetStateAsync<MyData>(
                "my_data",  // state name
                data);      // 存在 "my_data" state 的資料

            return "Success";
        }

        /// <summary>
        /// 從 actor 的 state store 取得 MyData
        /// </summary>
        /// <return>user 定義的 MyData 會存在 state store 的 "my_data" state</return>
        public Task<MyData> GetDataAsync()
        {
            // Gets state from the state store.
            return this.StateManager.GetStateAsync<MyData>("my_data");
        }

        /// <summary>
        /// 在這個 actor 註冊 MyReminder 這個 reminder
        /// </summary>
        public async Task RegisterReminder()
        {
            await this.RegisterReminderAsync(
                "MyReminder",              // Reminder 的名稱
                null,                      // 傳到 IRemindable.ReceiveReminderAsync() 的 User state
                TimeSpan.FromSeconds(5),   // 第一次調用 reminder 前要延遲多久
                TimeSpan.FromMinutes(1));  // 第一次調用 reminder 之後跟下次調用間隔的時間
        }

        /// <summary>
        /// 在這個 actor 取消註冊 MyReminder 這個 reminder
        /// </summary>
        public Task UnregisterReminder()
        {
            Console.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} {Id} Unregistering MyReminder...");
            return this.UnregisterReminderAsync("MyReminder");
        }

        // <summary>
        // 實作 IRemindeable.ReceiveReminderAsync() 這個當 actor reminder 被觸發後要調用的 callback
        // </summary>
        public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
        {
            Console.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} {Id} ReceiveReminderAsync is called!");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 在這個 actor 註冊 MyTimer 這個 timer
        /// </summary>
        public Task RegisterTimer()
        {
            return this.RegisterTimerAsync(
                "MyTimer",                  // Timer 的名稱
                nameof(this.OnTimerCallBack),       // Timer callback
                null,                       // 傳到 OnTimerCallback() 的 User state
                TimeSpan.FromSeconds(5),    // 第一次調用非同步 callback 前要延遲多久
                TimeSpan.FromMinutes(1));   // 第一次調用非同步 callback 後跟下次非同步 callback 要間隔多久
        }

        /// <summary>
        /// 在這個 actor 取消註冊 MyTimer 這個 timer
        /// </summary>
        public Task UnregisterTimer()
        {
            Console.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} {Id} Unregistering MyTimer...");
            return this.UnregisterTimerAsync("MyTimer");
        }

        /// <summary>
        /// 一旦 timer 被觸發要呼叫的 Timer callback
        /// </summary>
        private Task OnTimerCallBack(byte[] data)
        {
            Console.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} {Id} OnTimerCallBack is called!");
            return Task.CompletedTask;
        }
    }
}