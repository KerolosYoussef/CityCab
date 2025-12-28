namespace CityCab.Rider.API.Models
{
    public class Rider : BaseTimeStampedModel
    {
        private readonly List<Address> _addresses = [];
        private readonly List<PaymentMethod> _paymentMethods = [];
        private const decimal DEFAULT_RATING = 5.0m;

        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
        public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();
        public decimal Rating { get; private set; } = DEFAULT_RATING; // Start with 5 stars


        private Rider() { }

        private Rider(string name, string email, string phone)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(email, nameof(email));

            Name = name;
            Email = email;
            PhoneNumber = phone;
        }

        public static Rider Create(string name, string email, string phone)
        {
            return new Rider(name, email, phone);
        }

        public void UpdateRiderDetails(string name, string email, string phone)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(email, nameof(email));
            
            Name = name;
            Email = email;
            PhoneNumber = phone;
        }

        public Result<Address> AddAddress(
            string title, 
            string street, 
            string city, 
            string state, 
            string postalCode, 
            string country, 
            bool isDefault, 
            double lat, 
            double lon
        )
        {
            if (_addresses.Any(a => a.Title == title))
                return Result<Address>.Failure(Error.Validation("Address title already exists."));

            var address = Address.Create(title, street, city, state, postalCode, country, isDefault, lat, lon);
            _addresses.Add(address);
            return Result<Address>.Success(address);
        }

        public Result<PaymentMethod> AddPaymentMethod(
            string cardHolderName,
            string cardHolderType,
            string providerToken,
            string last4Digits,
            string expiryMonth,
            string expiryYear,
            bool isDefault
        )
        {
            if (_paymentMethods.Any(pm => pm.Last4Digits == last4Digits && pm.ExpiryMonth == expiryMonth && pm.ExpiryYear == expiryYear))
                return Result<PaymentMethod>.Failure(Error.Validation("Payment method already exists."));

            var paymentMethod = PaymentMethod.Create(cardHolderName, cardHolderType, providerToken, last4Digits, expiryMonth, expiryYear, isDefault);
            _paymentMethods.Add(paymentMethod);
            return Result<PaymentMethod>.Success(paymentMethod);
        }
    }
}
