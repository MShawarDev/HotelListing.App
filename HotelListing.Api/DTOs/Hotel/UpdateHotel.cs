using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Hotel;

public class UpdateHotel : CreateHotel
{
    [Required]
    public int Id { get; set; }
}