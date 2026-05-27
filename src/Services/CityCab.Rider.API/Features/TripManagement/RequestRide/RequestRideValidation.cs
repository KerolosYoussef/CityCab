namespace CityCab.Rider.API.Features.TripManagement.RequestRide
{
    public class RequestRideValidation : AbstractValidator<RequestRideCommand>
    {
        public RequestRideValidation()
        {
            RuleFor(x => x.RiderId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PickupLocation)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DropoffLocation)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PreferredPaymentMethodId)
                .NotEmpty()
                .NotNull();
        }
    }
}
