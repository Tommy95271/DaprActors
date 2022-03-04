using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Components;
using MyActorInterfaces;

namespace DaprBlazorServer.Pages.Actors
{
    public partial class Actor1
    {
        [Inject]
        public IActorProxyFactory actorProxyFactory { get; set; }

        [Inject]
        public ILogger<Actor1> logger { get; set; }

        public IMyActor proxy { get; set; }

        protected override Task OnInitializedAsync()
        {
            var actorType = "MyActor";
            var actorId = new ActorId($"myactor2");
            proxy = actorProxyFactory.CreateActorProxy<IMyActor>(actorId, actorType);

            return base.OnInitializedAsync();
        }

        private async Task ActorSetData()
        {
            logger.LogInformation("Actor set data!");
            var response = await proxy.SetDataAsync(new MyData()
            {
                PropertyA = "ValueA",
                PropertyB = "ValueB",
            });
            logger.LogInformation(response.ToString());
        }

        private async Task ActorGetData()
        {
            logger.LogInformation("Actor get data!");
            var data = await proxy.GetDataAsync();
            logger.LogInformation(data.ToString());
        }

        private async Task ActorRegisterReminder()
        {
            logger.LogInformation("Actor register reminder!");
            await proxy.RegisterReminder();
            logger.LogInformation("Actor register reminder successfully!");
        }

        private async Task ActorUnregisterReminder()
        {
            logger.LogInformation("Actor unregister reminder!");
            await proxy.UnregisterReminder();
            logger.LogInformation("Actor unregister reminder successfully!");
        }

        private async Task ActorRegisterTimer()
        {
            logger.LogInformation("Actor register timer!");
            await proxy.RegisterTimer();
            logger.LogInformation("Actor register timer successfully!");
        }

        private async Task ActorUnregisterTimer()
        {
            logger.LogInformation("Actor unregister timer!");
            await proxy.UnregisterTimer();
            logger.LogInformation("Actor unregister timer successfully!");
        }
    }
}
