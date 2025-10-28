namespace HotelListing.Api.DTOs.Hotel;

public record GetHotel(
    int Id,
    string Name,
    string Address,
    string PhoneNumber,
    double Rating,
    int CountryId,
    string CountryName
);
