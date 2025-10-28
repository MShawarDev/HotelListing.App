using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Country;

public class  UpdateCountry : CreateCountry
{
    [Required]
    public int Id { get; set; }
}