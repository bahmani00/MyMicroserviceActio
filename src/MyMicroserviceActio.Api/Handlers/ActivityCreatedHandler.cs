using MyMicroserviceActio.Common.Events;
using MyMicroserviceActio.Common.SeedWork;
using System;
using System.Threading.Tasks;

namespace MyMicroserviceActio.Api
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        public async Task HandleAsync(ActivityCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity Created: {@event.Name}");
        }
    }
}
