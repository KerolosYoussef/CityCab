using CityCab.Messaging.Trip;
using CityCab.Rider.API.Infrastructure.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace CityCab.Rider.API.Features.TripManagement.AssignTrip
{
    public sealed record AssignTripCommand(RideAssignedEvent Event)
        : ICommand<Result<Guid>>;
    public class AssignTripHandler(ApplicationDbContext dbContext, IHubContext<RideHub> hubContext) 
        : ICommandHandler<AssignTripCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(AssignTripCommand request, CancellationToken cancellationToken)
        {
            // Add the trip to database
            var @event = request.Event;
            var trip = TripHistory.FromEvent(@event);
            if (trip is null)
                return Result<Guid>.Failure(Error.Validation("Can't resolve trip history"));

            // Save changes to database
            await dbContext.TripHistory.AddAsync(trip, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Push the message to SignalR hub
            await hubContext.Clients
                .User(@event.RiderId.ToString())
                .SendAsync("RideAssigned", @event, cancellationToken);

            return Result<Guid>.Success(trip.Id);
        }
    }
}
