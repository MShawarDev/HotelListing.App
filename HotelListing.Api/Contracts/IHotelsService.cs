using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.Contracts
{
    public interface IHotelsService
    {
        Task<bool> HotelExistsAsync(int id);
        Task<bool> HotelExistsAsync(string name);
        Task<GetHotel> CreateHotelAsync(CreateHotel newHotel);
        Task DeleteHotelAsync(int id);
        Task<List<GetHotel>> GetHotelsAsync();
        Task<GetHotel> GetHotelAsync(int id);
        Task<bool> UpdateHotelAsync(int id, UpdateHotel updatedHotel);
    }
}