using CityCab.Common.Results;
using CityCab.Rider.API.Infrastructure;
using static CityCab.Common.Interfaces.CQRS.Commands.ICommandHandler;

namespace CityCab.Rider.API.Features.RiderManagements.RegisterRider
{
    public sealed record RegisterRiderCommand(string Name, string Email, string Phone) : ICommand<Result<Guid>>;
    public class RegisterRiderHandler(ApplicationDbContext dbContext)
        : ICommandHandler<RegisterRiderCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<Result<Guid>> Handle(RegisterRiderCommand request, CancellationToken cancellationToken)
        {
            // Create new rider
            Models.Rider rider = Models.Rider.Create(request.Name, request.Email, request.Phone);

            // If the phone number already exists return failure
            if (IsPhoneAlreadyExists(rider.PhoneNumber))
                return Result<Guid>.Failure(Error.Validation("This phone number is already registerd"));

            // Add rider to database
            await _dbContext.Riders.AddAsync(rider, cancellationToken);

            // Save changes to database
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(rider.Id);
        }

        private bool IsPhoneAlreadyExists(string phoneNumber)
        {
            // check if phone number already exists in the database
            return _dbContext.Riders.Any(r => r.PhoneNumber == phoneNumber);
        }
    }
}
