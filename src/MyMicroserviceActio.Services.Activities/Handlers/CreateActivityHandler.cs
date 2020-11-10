using System;
using System.Threading.Tasks;
using MyMicroserviceActio.Common.Commands;
using MyMicroserviceActio.Common.Events;
using RawRabbit;
using MyMicroserviceActio.Common.SeedWork;
using MyMicroserviceActio.Services.Activities.Services;
using MyMicroserviceActio.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace MyMicroserviceActio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> logger;

        public CreateActivityHandler(IBusClient busClient, 
            IActivityService activityService,
            ILogger<CreateActivityHandler> logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            this.logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            logger.LogInformation($"Creating activity: '{command.Id}' for user: '{command.UserId}'.");
            try {

                await _activityService.AddAsync(command.Id, command.UserId, command.Category,
                    command.Name, command.Description, command.CreatedAt);

                await _busClient.PublishAsync(new ActivityCreated(command.Id,
                    command.UserId, command.Category, command.Name, command.Description, command.CreatedAt));

            } catch (ActioException ex) {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id,
                    ex.Message, ex.Code));

                logger.LogError(ex, null, null);

            } catch (Exception ex) {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id,
                    ex.Message, "error"));

                logger.LogError(ex, null, null);
            }
        }
    }
}