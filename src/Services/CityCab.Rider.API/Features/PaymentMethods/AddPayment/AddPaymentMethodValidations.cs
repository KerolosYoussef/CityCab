namespace CityCab.Rider.API.Features.PaymentMethods.AddPayment
{
    public class AddPaymentMethodValidations : AbstractValidator<AddPaymentMethodCommand>
    {
        public AddPaymentMethodValidations()
        {
            RuleFor(x => x.RiderId)
                .NotEmpty()
                .WithMessage("Rider ID is required");

            RuleFor(x => x.ProviderToken)
                .NotEmpty()
                .WithMessage("Provider token is required")
                .MaximumLength(500)
                .WithMessage("Provider token must not exceed 500 characters");

            RuleFor(x => x.Last4Digit)
                .NotEmpty()
                .WithMessage("Last 4 digits are required")
                .Length(4)
                .WithMessage("Last 4 digits must be exactly 4 characters")
                .Matches("^[0-9]{4}$")
                .WithMessage("Last 4 digits must contain only numbers");

            RuleFor(x => x.ExpiryMonth)
                .NotEmpty()
                .WithMessage("Expiry month is required")
                .Must(BeValidMonth)
                .WithMessage("Expiry month must be between 01 and 12");

            RuleFor(x => x.ExpiryYear)
                .NotEmpty()
                .WithMessage("Expiry year is required")
                .Must(BeValidYear)
                .WithMessage("Expiry year must be a valid 4-digit year");

            RuleFor(x => x)
                .Must(NotBeExpired)
                .WithMessage("Payment method has expired")
                .When(x => !string.IsNullOrEmpty(x.ExpiryMonth) && !string.IsNullOrEmpty(x.ExpiryYear));

            RuleFor(x => x.CardHolderType)
                .MaximumLength(50)
                .WithMessage("Card brand must not exceed 50 characters")
                .When(x => !string.IsNullOrEmpty(x.CardHolderType));
        }

        private bool BeValidMonth(string month)
        {
            if (string.IsNullOrWhiteSpace(month))
                return false;

            return int.TryParse(month, out int monthValue) && monthValue >= 1 && monthValue <= 12;
        }

        private bool BeValidYear(string year)
        {
            if (string.IsNullOrWhiteSpace(year))
                return false;

            return int.TryParse(year, out int yearValue) && yearValue >= DateTime.UtcNow.Year;
        }

        private bool NotBeExpired(AddPaymentMethodCommand command)
        {
            if (!int.TryParse(command.ExpiryMonth, out int month) || !int.TryParse(command.ExpiryYear, out int year))
                return false;

            var expiryDate = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
            return expiryDate >= DateTime.UtcNow.Date;
        }
    }
}
