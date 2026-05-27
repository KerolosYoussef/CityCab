namespace CityCab.Rider.API.Features.RiderManagements.GetRider;

public record GetRiderResponse(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    decimal Rating,
    List<AddressDto> Addresses,
    List<PaymentMethodDto> PaymentMethods,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record AddressDto(
    Guid Id,
    string Title,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    bool IsDefault,
    double Latitude,
    double Longitude
);

public record PaymentMethodDto(
    Guid Id,
    string CardHolderName,
    string CardHolderType,
    string Last4Digits,
    string ExpiryMonth,
    string ExpiryYear,
    bool IsDefault
);
