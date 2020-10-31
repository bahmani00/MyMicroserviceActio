using MyMicroserviceActio.Common.SeedWork;
using System;

namespace MyMicroserviceActio.Common.Events
{
    public class ActivityCreated : IEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }

        //support serilizer
        protected ActivityCreated()
        {
        }

        public ActivityCreated(Guid id, Guid userId,
            string category, string name, 
            string description, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}