using MyMicroserviceActio.Api.Models;
using MyMicroserviceActio.Api.Repositories;
using MyMicroserviceActio.Common.Events;
using MyMicroserviceActio.Common.SeedWork;
using System;
using System.Threading.Tasks;

namespace MyMicroserviceActio.Api
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await activityRepository.AddAsync(new Activity { 
                Id = @event.Id,
                UserId = @event.UserId,
                Category = @event.Category,
                Name = @event.Name,
                Description = @event.Description,
                CreatedAt = @event.CreatedAt,
            });
            Console.WriteLine($"Activity Created: {@event.Name}");
        }
    }
}
