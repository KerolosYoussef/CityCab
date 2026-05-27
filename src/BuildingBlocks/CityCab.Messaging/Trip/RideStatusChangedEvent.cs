using CityCab.Messaging.Common.Events;

namespace CityCab.Messaging.Trip
{
    public record RideStatusChangedEvent : IntegrationEvent
    {
        public Guid RideId { get; set; }
        public string NewStatus { get; set; } = string.Empty;
    }
}
