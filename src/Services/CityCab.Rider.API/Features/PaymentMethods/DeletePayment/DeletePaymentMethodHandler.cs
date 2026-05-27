namespace CityCab.Rider.API.Features.PaymentMethods.DeletePayment
{
    public sealed record DeletePaymentMethodCommand(Guid RiderId, Guid PaymentMethodId) : ICommand<Result>;
    public class DeletePaymentMethodHandler(ApplicationDbContext dbContext)
        : ICommandHandler<DeletePaymentMethodCommand, Result>
    {
        public async Task<Result> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var rider = await dbContext
                .Riders
                .Include(r => r.PaymentMethods)
                .FirstOrDefaultAsync(r => r.Id == request.RiderId, cancellationToken);

            if (rider is null)
                return Result.Failure(Error.NotFound(nameof(Models.Rider), request.RiderId));

            var result = rider.DeletePaymentMethod(request.PaymentMethodId);

            if (!result.IsSuccess)
                return result;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
