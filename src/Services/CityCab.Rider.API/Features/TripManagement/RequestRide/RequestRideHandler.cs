using CityCab.Messaging.Rider;
using MassTransit;

namespace CityCab.Rider.API.Features.TripManagement.RequestRide
{
    public sealed record RequestRideCommand(Guid RiderId, string PickupLocation, string DropoffLocation, Guid PreferredPaymentMethodId)
        : ICommand<Result<Guid>>;
    public class RequestRideHandler(ApplicationDbContext context, IPublishEndpoint publishEndpoint) 
        : ICommandHandler<RequestRideCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(RequestRideCommand request, CancellationToken cancellationToken)
        {
            // Get the rider and check if it's existed
            var rider = await context.Riders.FindAsync(request.RiderId, cancellationToken);
            if(rider is null)
                return Result<Guid>.Failure(Error.NotFound(nameof(Models.Rider), request.RiderId));
            
            // Create the message body
            RideRequestedEvent rideRequestedEvent = request.Adapt<RideRequestedEvent>();
            
            // Send the message to the queue
            await publishEndpoint.Publish(rideRequestedEvent, cancellationToken);

            // Return the GUID from the message
            return Result<Guid>.Success(rideRequestedEvent.Id);
        }
    }
}
