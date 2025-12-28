using CityCab.Rider.API.Features.RiderManagements.Shared;
using static CityCab.Common.Interfaces.CQRS.Commands.ICommandHandler;

namespace CityCab.Rider.API.Features.RiderManagements.UpdateRider
{
    public sealed record UpdateRiderCommand(Guid Id, string Name, string Email, string Phone) 
        : ICommand<Result<Unit>>, IRiderCommand;
    public class UpdateRiderHandler(ApplicationDbContext dbContext, IRiderUniquenessChecker riderUniquenessChecker) 
        : ICommandHandler<UpdateRiderCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(UpdateRiderCommand request, CancellationToken cancellationToken)
        {
            if (await riderUniquenessChecker.IsRiderUniqueAsync(request.Email, request.Phone, cancellationToken, request.Id) is false)
                return Result<Unit>.Failure(Error.Validation("Rider with the same email or phone number already exists."));

            var rider = await dbContext.Riders.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (rider is null)
                return Result<Unit>.Failure(Error.NotFound(nameof(Models.Rider), request.Id));

            rider.UpdateRiderDetails(request.Name, request.Email, request.Phone);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
