namespace CityCab.Rider.API.Features.RiderManagements.RegisterRider
{
    public class RegisterRiderValidation : AbstractValidator<RegisterRiderCommand>
    {
        public RegisterRiderValidation()
        {
            // Name validation
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            // Email validation
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .MaximumLength(100);

            // Phone number validation
            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                // Egyptain phone number format validation
                .Matches("^01[0125][0-9]{8}$")
                .MaximumLength(13);
        }
    }
}
