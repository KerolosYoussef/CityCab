using CityCab.Messaging.Trip;
using MassTransit;

namespace CityCab.Rider.API.Features.TripManagement.AssignTrip
{
    public class AssignTripConsumer(ISender sender)
        : IConsumer<RideAssignedEvent>
    {
        public async Task Consume(ConsumeContext<RideAssignedEvent> context)
        {
            // Send the context event to handler
            await sender.Send(new AssignTripCommand(context.Message));
        }
    }
}
