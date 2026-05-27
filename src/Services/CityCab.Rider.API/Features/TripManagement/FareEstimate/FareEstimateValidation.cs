namespace CityCab.Rider.API.Features.TripManagement.FareEstimate
{
    public class FareEstimateValidation : AbstractValidator<FareEstimateQuery>
    {
        public FareEstimateValidation()
        {
            RuleFor(x => x.PickupLocation)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DropoffLocation)
                .NotEmpty()
                .NotNull();
        }
    }
}
