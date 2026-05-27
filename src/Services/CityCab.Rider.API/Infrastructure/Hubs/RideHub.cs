using Microsoft.AspNetCore.SignalR;

namespace CityCab.Rider.API.Infrastructure.Hubs
{
    public class RideHub : Hub
    {
        // Groups by RiderId so Rider service can push to specific riders
        public override async Task OnConnectedAsync()
        {
            var riderId = Context.UserIdentifier;
            if (riderId is not null)
                await Groups.AddToGroupAsync(Context.ConnectionId, riderId);
            await base.OnConnectedAsync();
        }
    }
}
