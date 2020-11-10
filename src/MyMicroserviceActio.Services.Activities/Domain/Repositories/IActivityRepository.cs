using System;
using System.Threading.Tasks;
using MyMicroserviceActio.Services.Activities.Domain.Models;

namespace MyMicroserviceActio.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task AddAsync(Activity activity);
        Task DeleteAsync(Guid id);
    }
}