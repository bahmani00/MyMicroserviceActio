using System;
using System.Threading.Tasks;
using MyMicroserviceActio.Common.Commands;
using MyMicroserviceActio.Common.Events;
using RawRabbit;
using MyMicroserviceActio.Common.SeedWork;

namespace MyMicroserviceActio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating activity: '{command.Id}' for user: '{command.UserId}'.");
            try {
                await _busClient.PublishAsync(new ActivityCreated(command.Id,
                    command.UserId, command.Category, command.Name, command.Description, command.CreatedAt));

            } catch (Exception ex) {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id,
                    ex.Message, "error"));
            }
        }
    }
}