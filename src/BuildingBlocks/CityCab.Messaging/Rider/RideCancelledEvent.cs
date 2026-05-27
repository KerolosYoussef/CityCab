using CityCab.Messaging.Common.Events;

namespace CityCab.Messaging.Rider
{
    public record RideCancelledEvent : IntegrationEvent
    {
        public Guid RideId { get; set; }
        public Guid RiderId { get; set; }
        public DateTime CancelledAt { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
