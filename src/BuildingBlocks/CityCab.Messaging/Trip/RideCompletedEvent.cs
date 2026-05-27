using CityCab.Messaging.Common.Events;

namespace CityCab.Messaging.Trip
{
    public record RideCompletedEvent : IntegrationEvent
    {
        public Guid RideId { get; set; }
        public decimal FinalFare { get; set; }
        public float ActualDistance { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
