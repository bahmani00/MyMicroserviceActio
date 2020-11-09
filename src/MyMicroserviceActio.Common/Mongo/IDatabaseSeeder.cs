using System.Threading.Tasks;

namespace MyMicroserviceActio.Common.Mongo
{
    public interface IDatabaseSeeder
    {
         Task SeedAsync();
    }
}