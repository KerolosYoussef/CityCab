namespace CityCab.Rider.API.Models
{
    public class PaymentMethod : BaseModel
    {
        public string CardHolderName { get; private set; }
        public string CardHolderType { get; private set; }
        public string ProviderToken { get; private set; }
        public string Last4Digits { get; private set; }
        public string ExpiryMonth { get; private set; }
        public string ExpiryYear { get; private set; }
        public bool IsDefault { get; private set; }
        public Guid RiderId { get; private set; }
        private PaymentMethod(
            string cardHolderName,
            string cardHolderType,
            string providerToken,
            string last4Digits,
            string expiryMonth,
            string expiryYear,
            bool isDefault
        )
        {
            CardHolderName = cardHolderName;
            CardHolderType = cardHolderType;
            ProviderToken = providerToken;
            Last4Digits = last4Digits;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            IsDefault = isDefault;
        }

        public static PaymentMethod Create(
            string cardHolderName,
            string cardHolderType,
            string providerToken,
            string last4Digits,
            string expiryMonth,
            string expiryYear,
            bool isDefault
        )
        {
            return new PaymentMethod(
                cardHolderName,
                cardHolderType,
                providerToken,
                last4Digits,
                expiryMonth,
                expiryYear,
                isDefault
            );
        }

    }
}
