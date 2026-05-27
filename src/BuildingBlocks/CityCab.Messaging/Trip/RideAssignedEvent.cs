using CityCab.Messaging.Common.Events;

namespace CityCab.Messaging.Trip
{
    public record RideAssignedEvent : IntegrationEvent
    {
        // Identity
        public Guid RideId { get; set; }
        public Guid RiderId { get; set; }

        // Driver snapshot (rider needs this immediately)
        public Guid DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public VehicleInfo VehicleInfo { get; set; } = default!;
        public TimeSpan EstimatedArrival { get; set; }

        // Pickup snapshot (avoids a query for a common need)
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }

    public record VehicleInfo
    {
        public string Model { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
