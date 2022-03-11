using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Components;
using MyActorInterfaces;

namespace DaprBlazorServer.Pages.Actors
{
    public partial class Actor2
    {
        [Inject]
        public IActorProxyFactory actorProxyFactory { get; set; }

        [Inject]
        public ILogger<Actor2> logger { get; set; }

        public IMyActor proxy { get; set; }

        protected override Task OnInitializedAsync()
        {
            var actorType = "BlazorActor";
            var actorId = new ActorId($"blazoractor");
            proxy = actorProxyFactory.CreateActorProxy<IMyActor>(actorId, actorType);

            return base.OnInitializedAsync();
        }

        private async Task ActorSetData()
        {
            logger.LogInformation("{CurrentTime} Actor set data!", DateTime.Now);
            var response = await proxy.SetDataAsync(new MyData()
            {
                PropertyA = "ValueA",
                PropertyB = "ValueB",
            });
            logger.LogInformation(response.ToString());
        }

        private async Task ActorGetData()
        {
            logger.LogInformation("{CurrentTime} Actor get data!", DateTime.Now);
            var data = await proxy.GetDataAsync();
            logger.LogInformation(data.ToString());
        }

        private async Task ActorRegisterReminder()
        {
            logger.LogInformation("{CurrentTime} Actor register reminder!", DateTime.Now);
            await proxy.RegisterReminder();
            logger.LogInformation("{CurrentTime} Actor register reminder successfully!", DateTime.Now);
        }

        private async Task ActorUnregisterReminder()
        {
            logger.LogInformation("{CurrentTime} Actor unregister reminder!", DateTime.Now);
            await proxy.UnregisterReminder();
            logger.LogInformation("{CurrentTime} Actor unregister reminder successfully!", DateTime.Now);
        }

        private async Task ActorRegisterTimer()
        {
            logger.LogInformation("{CurrentTime} Actor register timer!", DateTime.Now);
            await proxy.RegisterTimer();
            logger.LogInformation("{CurrentTime} Actor register timer successfully!", DateTime.Now);
        }

        private async Task ActorUnregisterTimer()
        {
            logger.LogInformation("{CurrentTime} Actor unregister timer!", DateTime.Now);
            await proxy.UnregisterTimer();
            logger.LogInformation("{CurrentTime} Actor unregister timer successfully!", DateTime.Now);
        }
    }
}
