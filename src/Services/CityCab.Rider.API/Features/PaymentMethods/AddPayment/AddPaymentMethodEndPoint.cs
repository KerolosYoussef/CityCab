namespace CityCab.Rider.API.Features.PaymentMethods.AddPayment
{
    public sealed record AddPaymentMethodRequest(
        string CardHolderName,
        string CardHolderType,
        string ProviderToken,
        string Last4Digit,
        string ExpiryMonth,
        string ExpiryYear,
        bool IsDefault
        );

    public class AddPaymentMethodEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/riders/{riderId}/payment-methods", AddPaymentMethod)
                .WithName("AddPaymentMethod")
                .Produces<Result<Guid>>()
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> AddPaymentMethod(Guid riderId, AddPaymentMethodRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = new AddPaymentMethodCommand(
                riderId,
                request.CardHolderName,
                request.CardHolderType,
                request.ProviderToken,
                request.Last4Digit,
                request.ExpiryMonth,
                request.ExpiryYear,
                request.IsDefault
            );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Problem(result.Error!.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
