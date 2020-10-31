namespace MyMicroserviceActio.Common.SeedWork
{
    public interface IRejectedEvent : IEvent
    {
         string Reason { get; }
         string Code { get; }
    }
}