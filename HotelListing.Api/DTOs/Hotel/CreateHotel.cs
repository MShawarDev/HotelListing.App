using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Hotel;
public class CreateHotel
{
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Address { get; set; }
    [Phone]
    public required string PhoneNumber { get; set; }
    [Range(1, 5)]
    public double Rating { get; set; }
    [Required]
    public int CountryId { get; set; }
}
