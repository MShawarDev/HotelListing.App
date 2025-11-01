namespace HotelListing.Api.DTOs.Country;

public record GetCountry(
    int Id,
    string Name,
    string ShortName,
    List<GetCountryHotel> CountryHotels
    );
