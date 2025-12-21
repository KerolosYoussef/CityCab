namespace CityCab.Rider.API.Models
{
    public class Rider : BaseTimeStampedModel
    {
        private readonly List<Address> _addresses = [];
        private readonly List<PaymentMethod> _paymentMethods = [];

        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
        public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();
        public decimal Rating { get; private set; } = 5.0m; // Start with 5 stars
            
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

        public void AddAddress(
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
                throw new InvalidOperationException("Address title already exists.");

            _addresses.Add(Address.Create(title, street, city, state, postalCode, country, isDefault, lat, lon));
        }

        public void AddPaymentMethod(
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
                throw new InvalidOperationException("Payment method already exists.");

            _paymentMethods.Add(PaymentMethod.Create(
                cardHolderName,
                cardHolderType,
                providerToken,
                last4Digits,
                expiryMonth,
                expiryYear,
                isDefault
            ));
        }
    }
}
