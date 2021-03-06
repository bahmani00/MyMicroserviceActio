using MyMicroserviceActio.Common.SeedWork;
using System;

namespace MyMicroserviceActio.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        //support serilizer
        protected CreateActivityRejected()
        {
        }

        public CreateActivityRejected(Guid id, 
            string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}