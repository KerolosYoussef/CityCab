using CityCab.Messaging.Common.Events;

namespace CityCab.Messaging.Rider
{
    public record RideRequestedEvent : IntegrationEvent
    {
        public Guid RiderId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; } = DateTime.Now;
        public Guid PreferredPaymentMethodId { get; set; }
    }
}
