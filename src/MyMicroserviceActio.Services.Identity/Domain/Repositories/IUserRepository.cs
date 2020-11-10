using System;
using System.Threading.Tasks;
using MyMicroserviceActio.Services.Identity.Domain.Models;

namespace MyMicroserviceActio.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
    }
}