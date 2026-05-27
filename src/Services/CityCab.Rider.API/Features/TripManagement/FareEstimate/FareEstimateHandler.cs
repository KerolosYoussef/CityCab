using CityCab.Common.Interfaces.CQRS.Queries;
using CityCab.Trip.API;

namespace CityCab.Rider.API.Features.TripManagement.FareEstimate
{
    public sealed record FareEstimateQuery(Guid RiderId, string PickupLocation, string DropoffLocation) : IQuery<Result<FareEstimateResult>>;
    public sealed record FareEstimateResult(Guid RiderId, double EstimatedFare, double DistanceKM, int DurationMinutes);
    public class FareEstimateHandler(FareEstimateService.FareEstimateServiceClient client) 
        : IQueryHandler<FareEstimateQuery, Result<FareEstimateResult>>
    {
        public async Task<Result<FareEstimateResult>> Handle(FareEstimateQuery query, CancellationToken cancellationToken)
        {
            // Map FareEstimateQuery to FareEstimateRequest
            var fareEstimateRequest = query.Adapt<Trip.API.FareEstimateRequest>();

            // Get the result from the grpc client
            var fareEstimateResponse = await client.GetFareEstimateAsync(fareEstimateRequest, cancellationToken: cancellationToken);

            if (fareEstimateResponse is null)
                return Result<FareEstimateResult>.Failure(Error.Validation("Couldn't get the fare estimate"));

            // Map FareEstimateResponse to FareEstimateResult
            var result = new FareEstimateResult(
                new Guid(fareEstimateResponse.RiderId), 
                fareEstimateResponse.EstimatedFare, 
                fareEstimateResponse.DistanceKM, 
                fareEstimateResponse.DurationMinutes
            );

            return Result<FareEstimateResult>.Success(result);
        }
    }
}
