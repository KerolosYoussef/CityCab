namespace CityCab.Rider.API.Features.RiderManagements.Shared
{
    public interface IRiderUniquenessChecker
    {
        Task<bool> IsRiderUniqueAsync(string email, string phoneNumber, CancellationToken cancellationToken, Guid? excludeRiderId = null);
    }
}
