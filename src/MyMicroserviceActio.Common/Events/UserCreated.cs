using MyMicroserviceActio.Common.SeedWork;

namespace MyMicroserviceActio.Common.Events
{
    public class UserCreated: IEvent
    {
        public string Email { get; }
        public string Name { get; }

        //support serilizer
        protected UserCreated()
        {
        }

        public UserCreated(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}