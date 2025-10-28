using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Country;

public class CreateCountry
{
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(5)]
    public required string ShortName { get; set; }
}
