using HotelListing.App.Application.DTOs.Country;
using HotelListing.App.Common.Models.Filtering;
using HotelListing.App.Common.Models.Paging;
using HotelListing.App.Common.Results;
using Microsoft.AspNetCore.JsonPatch;

namespace HotelListing.App.Application.Contracts;

public interface ICountriesService
{
    Task<bool> CountryExistsAsync(int id);
    Task<bool> CountryExistsAsync(string name);
    Task<Result<GetCountryDto>> CreateCountryAsync(CreateCountryDto createDto);
    Task<Result> DeleteCountryAsync(int id);
    Task<Result<IEnumerable<GetCountriesDto>>> GetCountriesAsync(CountryFilterParameters? filters);
    Task<Result<GetCountryHotelsDto>> GetCountryHotelsAsync(int countryId, PaginationParameters paginationParameters, CountryFilterParameters filters);
    Task<Result<GetCountryDto>> GetCountryAsync(int id);
    Task<Result> UpdateCountryAsync(int id, UpdateCountryDto updateDto);
    Task<Result> PatchCountryAsync(int id, JsonPatchDocument<UpdateCountryDto> patchDoc);  // Add this

}