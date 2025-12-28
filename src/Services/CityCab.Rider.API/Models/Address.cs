namespace CityCab.Rider.API.Models
{
    public class Address : BaseModel
    {
        public string Title { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public bool IsDefault { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public Guid RiderId { get; private set; }
        private Address(
            string title, 
            string street, 
            string city, 
            string state, 
            string postalCode, 
            string country, 
            bool isDefault, 
            double latitude, 
            double longitude
        )
        {
            Title = title;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            IsDefault = isDefault;
            Latitude = latitude;
            Longitude = longitude;
        }
        public static Address Create(
            string title, 
            string street, 
            string city, 
            string state, 
            string postalCode, 
            string country, 
            bool isDefault, 
            double latitude, 
            double longitude
        )
        {
            return new Address(title, street, city, state, postalCode, country, isDefault, latitude, longitude);
        }
    }
}
