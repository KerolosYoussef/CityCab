namespace CityCab.Rider.API.Features.RiderManagements.RegisterRider
{
    public sealed record RegisterRiderRequest(string Name, string Email, string Phone);
    public class RegisterRiderEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/riders", RegisterRider)
                .WithName("RegisterRider")
                .Produces<Result<Guid>>()
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> RegisterRider(RegisterRiderRequest request, ISender sender)
        {
            // map request to command
            var command = request.Adapt<RegisterRiderCommand>();

            // send the command to its handler
            var result = await sender.Send(command);

            // return appropriate response
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Problem(result.Error!.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
