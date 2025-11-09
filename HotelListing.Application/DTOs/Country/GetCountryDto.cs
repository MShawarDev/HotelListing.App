using HotelListing.App.Application.DTOs.Hotel;
using HotelListing.App.Common.Models.Paging;

namespace HotelListing.App.Application.DTOs.Country;

public class GetCountryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public List<GetHotelSlimDto> Hotels { get; set; } = new();
}

public class GetCountryHotelsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PagedResult<GetHotelSlimDto> Hotels { get; set; } = new();
}

public class GetCountriesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
}