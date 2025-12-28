namespace CityCab.Rider.API.Models
{
    public class PaymentMethod : BaseModel
    {
        public string CardHolderName { get; private set; } = string.Empty;
        public string CardHolderType { get; private set; } = string.Empty;
        public string ProviderToken { get; private set; } = string.Empty;
        public string Last4Digits { get; private set; } = string.Empty;
        public string ExpiryMonth { get; private set; } = string.Empty;
        public string ExpiryYear { get; private set; } = string.Empty;
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
