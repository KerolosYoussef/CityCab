using CityCab.Trip.API;
using Grpc.Core;

namespace CityCab.Trip.API.Grpc.Services
{
    public class FareEstimate : FareEstimateService.FareEstimateServiceBase
    {
        public override async Task<FareEstimateResponse> GetFareEstimate(FareEstimateRequest request, ServerCallContext context)
        {
            // TODO: Add fare estimate logic
            return new()
            {
                RiderId = request.RiderId,
                DistanceKM = 10.0,
                DurationMinutes = 20,
                EstimatedFare = 100.0
            };
        }
    }
}
