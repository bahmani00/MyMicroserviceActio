using System.Threading.Tasks;
using MyMicroserviceActio.Common.Auth;
using MyMicroserviceActio.Common.Exceptions;
using MyMicroserviceActio.Services.Identity.Domain.Models;
using MyMicroserviceActio.Services.Identity.Domain.Repositories;
using MyMicroserviceActio.Services.Identity.Domain.Services;

namespace MyMicroserviceActio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler jwtHandler;

        public UserService(IUserRepository repository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            _repository = repository;
            _encrypter = encrypter;
            this.jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _repository.GetAsync(email);
            if (user != null)
            {
                throw new ActioException("email_in_use",
                    $"Email: '{email}' is already in use.");
            }
            user = new User(email, name);
            user.SetPassword(password, _encrypter);
            await _repository.AddAsync(user);
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _repository.GetAsync(email);
            if (user == null)
            {
                throw new ActioException("invalid_credentials",
                    $"Invalid credentials.");
            }
            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new ActioException("invalid_credentials",
                    $"Invalid credentials.");
            }

            return jwtHandler.Create(user.Id);
        }
    }
}