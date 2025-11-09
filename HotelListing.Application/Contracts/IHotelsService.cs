using HotelListing.App.Application.DTOs.Hotel;
using HotelListing.App.Common.Models.Filtering;
using HotelListing.App.Common.Models.Paging;
using HotelListing.App.Common.Results;

namespace HotelListing.App.Application.Contracts;

public interface IHotelsService
{
    // Keep these for quick checks elsewhere if needed
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name, int countryId);
    Task<Result<PagedResult<GetHotelDto>>> GetHotelsAsync(PaginationParameters paginationParameters, HotelFilterParameters filters);
    Task<Result<GetHotelDto>> GetHotelAsync(int id);
    Task<Result<GetHotelDto>> CreateHotelAsync(CreateHotelDto createDto);
    Task<Result> UpdateHotelAsync(int id, UpdateHotelDto updateDto);
    Task<Result> DeleteHotelAsync(int id);
}