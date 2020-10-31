using MyMicroserviceActio.Common.SeedWork;

namespace MyMicroserviceActio.Common.Events
{
    public class AuthenticateUserRejected : IRejectedEvent
    {
        public string Email { get; }
        public string Code { get; }
        public string Reason { get; }

        //support serilizer
        protected AuthenticateUserRejected()
        {
        }

        public AuthenticateUserRejected(string email,
            string code, string reason)
        {
            Email = email;
            Code = code;
            Reason = reason;
        }         
    }
}