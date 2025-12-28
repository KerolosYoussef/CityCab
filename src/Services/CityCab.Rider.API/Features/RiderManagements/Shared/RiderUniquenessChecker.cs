using Microsoft.EntityFrameworkCore;
namespace CityCab.Rider.API.Features.RiderManagements.Shared
{
    public class RiderUniquenessChecker(ApplicationDbContext dbContext)
        : IRiderUniquenessChecker
    {
        public async Task<bool> IsRiderUniqueAsync(string email, string phoneNumber, CancellationToken cancellationToken, Guid? excludeRiderId = null)
        {
            var query = dbContext.Riders.AsQueryable();

            // If this is an update (ID provided), exclude the current rider from the check
            if (excludeRiderId.HasValue)
            {
                query = query.Where(r => r.Id != excludeRiderId.Value);
            }

            // Check if any OTHER rider has this email or phone
            bool isTaken = await query.AnyAsync(r => r.Email == email || r.PhoneNumber == phoneNumber, cancellationToken);

            return !isTaken; // Return true if unique (not taken)
        }
    }
}
