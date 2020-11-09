using System.Threading.Tasks;

namespace MyMicroserviceActio.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}