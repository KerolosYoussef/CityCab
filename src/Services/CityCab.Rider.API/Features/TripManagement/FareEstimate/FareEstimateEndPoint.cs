using Microsoft.AspNetCore.Mvc;

namespace CityCab.Rider.API.Features.TripManagement.FareEstimate
{
    public sealed record FareEstimateRequest(Guid RiderId, string PickupLocation, string DropoffLocation);
    public sealed record FareEstimateResponse(Guid RiderId, double EstimatedFare, double DistanceKM, int DurationMinutes);
    public class FareEstimateEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/riders/fare-estimate", FareEstimate)
                .WithName("FareEstimate")
                .Produces<Result<FareEstimateResponse>>()
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> FareEstimate(FareEstimateRequest request, ISender sender, CancellationToken cancellationToken)
        {
            // map request to command
            var command = request.Adapt<FareEstimateQuery>();

            // send the command to its handler
            var result = await sender.Send(command, cancellationToken);

            // return appropriate response
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Problem(result.Error!.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
