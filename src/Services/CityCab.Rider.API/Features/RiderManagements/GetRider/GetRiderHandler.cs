using CityCab.Common.Interfaces.CQRS.Queries;

namespace CityCab.Rider.API.Features.RiderManagements.GetRider
{
    public sealed record GetRiderQuery(Guid RiderId) : IQuery<Result<GetRiderResponse>>;
    public class GetRiderHandler(ApplicationDbContext dbContext) 
        : IQueryHandler<GetRiderQuery, Result<GetRiderResponse>>
    {
        public async Task<Result<GetRiderResponse>> Handle(GetRiderQuery request, CancellationToken cancellationToken)
        {
            var rider = await dbContext.Riders.AsNoTracking()
                .Include(r => r.Addresses)
                .Include(r => r.PaymentMethods)
                .FirstOrDefaultAsync(r => r.Id == request.RiderId, cancellationToken);

            if(rider is null)
                return Result<GetRiderResponse>.Failure(Error.NotFound(nameof(Rider), request.RiderId));

            var result = rider.Adapt<GetRiderResponse>();
            return Result<GetRiderResponse>.Success(result);
        }
    }
}
