using System;
using System.Threading.Tasks;
using MyMicroserviceActio.Common.Commands;
using MyMicroserviceActio.Common.Events;
using MyMicroserviceActio.Common.Exceptions;
using MyMicroserviceActio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using MyMicroserviceActio.Common.SeedWork;

namespace MyMicroserviceActio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient,
            IUserService userService, 
            ILogger<CreateUser> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: '{command.Email}' with name: '{command.Name}'.");
            try 
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);

                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.Name}'.");
            }
            catch (ActioException ex)
            {
                _logger.LogError(ex, ex.Message);

                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, ex.Code)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, "error"));                
            }
        }
    }
}