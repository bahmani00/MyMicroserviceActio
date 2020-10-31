using MyMicroserviceActio.Common.SeedWork;

namespace MyMicroserviceActio.Common.Events
{
    public class UserAuthenticated : IEvent
    {
        public string Email { get; }

        //support serilizer
        protected UserAuthenticated()
        {
        }

        public UserAuthenticated(string email)
        {
            Email = email;
        }
    }
}