namespace CityCab.Rider.API.Features.RiderManagements.GetRider
{
    public class GetRiderEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/riders/{id:guid}", GetRiderById)
                .WithName("GetRider")
                .Produces<Result<GetRiderResponse>>()
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> GetRiderById(Guid id, ISender sender, CancellationToken cancellationToken)
        {
            var query = new GetRiderQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Problem(result.Error!.Message, statusCode: StatusCodes.Status400BadRequest);
        }    
    }
}
