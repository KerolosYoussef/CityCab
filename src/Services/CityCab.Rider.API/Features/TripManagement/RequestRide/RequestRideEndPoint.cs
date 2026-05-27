namespace CityCab.Rider.API.Features.TripManagement.RequestRide
{
    public sealed record RequestRideRequest(Guid RiderId, string PickupLocation, string DropoffLocation, Guid PreferredPaymentMethodId);
    public class RequestRideEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/riders/request-ride", RideRequest)
                .WithName("RideRequest")
                .Produces<Result<Guid>>()
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> RideRequest(RequestRideRequest request, ISender sender, CancellationToken cancellationToken)
        {
            // map request to command
            var command = request.Adapt<RequestRideCommand>();

            // send the command to its handler
            var result = await sender.Send(command, cancellationToken);

            // return appropriate response
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Problem(result.Error!.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
