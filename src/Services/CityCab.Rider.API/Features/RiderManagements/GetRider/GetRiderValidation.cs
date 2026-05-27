namespace CityCab.Rider.API.Features.RiderManagements.GetRider
{
    public class GetRiderValidation : AbstractValidator<GetRiderQuery>
    {
        public GetRiderValidation()
        {
            RuleFor(x => x.RiderId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("RiderId must be a valid non-empty GUID.");
        }
    }
}
