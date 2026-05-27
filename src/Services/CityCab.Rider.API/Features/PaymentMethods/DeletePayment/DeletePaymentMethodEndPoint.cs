
namespace CityCab.Rider.API.Features.PaymentMethods.DeletePayment
{
    public class DeletePaymentMethodEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/riders/{riderId}/payment-methods/{paymentMethodId}", DeletePaymentMethod)
                .WithName("DeletePaymentMethod")
                .Produces<Result>()
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> DeletePaymentMethod(Guid riderId, Guid paymentMethodId, ISender sender, CancellationToken cancellationToken)
        {
            var command = new DeletePaymentMethodCommand(
                riderId,
                paymentMethodId
            );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Ok()
                : Results.Problem(result.Error!.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
