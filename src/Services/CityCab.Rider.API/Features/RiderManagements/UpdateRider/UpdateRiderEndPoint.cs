namespace CityCab.Rider.API.Features.RiderManagements.UpdateRider
{
    public sealed record UpdateRiderRequest(string Name, string Email, string Phone);
    public class UpdateRiderEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPatch("/riders/{id}", UpdateRider)
                .WithName("UpdateRider")
                .Produces<Result<Unit>>()
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> UpdateRider(UpdateRiderRequest request, Guid id, ISender sender)
        {
            var command = new UpdateRiderCommand(id, request.Name, request.Email, request.Phone);

            var result = await sender.Send(command);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Problem(result.Error!.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
