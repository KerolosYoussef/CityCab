using CityCab.Messaging.Trip;
using CityCab.Rider.API.Features.TripManagement.AssignTrip;

namespace CityCab.Rider.API.Models
{
    public class TripHistory : BaseTimeStampedModel
    {
        public Guid TripId { get; set; }
        public Guid RiderId { get; set; }
        public Guid DriverId { get; set; }
        public TripStatus Status { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public bool IsActive { get; set; }

        private TripHistory() { }

        private TripHistory(
            Guid tripId, Guid riderId, Guid driverId, TripStatus status, string pickupLocation, string dropoffLocation, DateTime? completedAt, bool isActive)
        {
            TripId = tripId;
            RiderId = riderId;
            DriverId = driverId;
            Status = status;
            PickupLocation = pickupLocation;
            DropoffLocation = dropoffLocation;
            CompletedAt = completedAt;
            IsActive = isActive;
        }

        public static TripHistory FromEvent(RideAssignedEvent @event)
        {
            return new(
                @event.RideId,
                @event.RiderId,
                @event.DriverId,
                TripStatus.Assigned,
                @event.PickupLocation,
                @event.DropoffLocation,
                completedAt: null,
                isActive: true
            );
        }

        public void MarkCompleted()
        {   
            IsActive = false;
            CompletedAt = DateTime.UtcNow;
            Status = TripStatus.Completed;
        }

        public void UpdateStatus(TripStatus status)
        {
            Status = status;
        }
    }
}
