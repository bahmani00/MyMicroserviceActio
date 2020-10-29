using System.Threading.Tasks;

namespace MyMicroserviceActio.Common.SeedWork
{
    public interface IEventHandler<T> where T: IEvent
    {
        Task HandleAsync(T @event);
    }
}
