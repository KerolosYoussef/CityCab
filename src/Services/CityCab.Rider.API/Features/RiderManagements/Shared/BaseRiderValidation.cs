namespace CityCab.Rider.API.Features.RiderManagements.Shared
{
    public abstract class BaseRiderValidation<T> : AbstractValidator<T> where T : IRiderCommand
    {
        public BaseRiderValidation()
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
                // Egyptian phone number format validation
                .Matches("^01[0125][0-9]{8}$")
                .MaximumLength(13);
        }
    }
}
