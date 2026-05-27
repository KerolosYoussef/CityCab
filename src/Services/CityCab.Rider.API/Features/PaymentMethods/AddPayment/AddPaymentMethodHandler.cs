namespace CityCab.Rider.API.Features.PaymentMethods.AddPayment
{
    public sealed record AddPaymentMethodCommand
        (Guid RiderId, 
        string CardHolderName,
        string CardHolderType,
        string ProviderToken,
        string Last4Digit,
        string ExpiryMonth,
        string ExpiryYear,
        bool IsDefault
        ) 
        : ICommand<Result<Guid>>;
    public class AddPaymentMethodHandler(ApplicationDbContext dbContext)
        : ICommandHandler<AddPaymentMethodCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(AddPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var rider = await dbContext.Riders
                .Include(r => r.PaymentMethods)
                .FirstOrDefaultAsync(r => r.Id == request.RiderId, cancellationToken);

            if (rider is null)
                return Result<Guid>.Failure(Error.NotFound(nameof(Models.Rider), request.RiderId));

            // check if there other default payment methods for the rider and this one is set to default
            // then set this payment methods to non-default
            // to prevent more than one payment method to be the default
            if (rider.PaymentMethods.FirstOrDefault(p => p.IsDefault) is PaymentMethod defaultPaymentMethod && request.IsDefault)
            {
                defaultPaymentMethod.SetDefault(false);
            }

            var result = rider.AddPaymentMethod(request.CardHolderName,
                request.CardHolderType,
                providerToken: request.ProviderToken,
                last4Digits: request.Last4Digit,
                request.ExpiryMonth,
                request.ExpiryYear,
                request.IsDefault
            );

            await dbContext.SaveChangesAsync(cancellationToken);

            return result.IsSuccess && result.Value is not null
                ? Result<Guid>.Success(result.Value.Id)
                : Result<Guid>.Failure(result.Error ?? Error.Validation("Failed to add payment method."));
        }
    }
}
