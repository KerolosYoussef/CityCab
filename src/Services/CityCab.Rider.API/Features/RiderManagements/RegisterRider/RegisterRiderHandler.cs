using CityCab.Rider.API.Features.RiderManagements.Shared;
using static CityCab.Common.Interfaces.CQRS.Commands.ICommandHandler;

namespace CityCab.Rider.API.Features.RiderManagements.RegisterRider
{
    public sealed record RegisterRiderCommand(string Name, string Email, string Phone) 
        : ICommand<Result<Guid>>, IRiderCommand;
    public class RegisterRiderHandler(ApplicationDbContext dbContext, IRiderUniquenessChecker riderUniquenessChecker)
                : ICommandHandler<RegisterRiderCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(RegisterRiderCommand request, CancellationToken cancellationToken)
        {
            if (await riderUniquenessChecker.IsRiderUniqueAsync(request.Email, request.Phone, cancellationToken) is false)
                return Result<Guid>.Failure(Error.Validation("Rider with the same email or phone number already exists."));

            Models.Rider rider = Models.Rider.Create(request.Name, request.Email, request.Phone);

            await dbContext.Riders.AddAsync(rider, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(rider.Id);
        }
    }
}
