using System.Threading.Tasks;

namespace MyMicroserviceActio.Common.SeedWork
{
    public interface ICommandHandler<T> where T: ICommand
    {
        Task HandleAsync(T command);
    }
}
