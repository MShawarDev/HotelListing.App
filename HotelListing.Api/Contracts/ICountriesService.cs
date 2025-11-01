using HotelListing.Api.DTOs.Country;

namespace HotelListing.Api.Contracts
{
    public interface ICountriesService
    {
        Task<bool> CountryExistsAsync(int id);
        Task<bool> CountryExistsAsync(string name);
        Task<GetCountry> CreateCountryAsync(CreateCountry newCountry);
        Task DeleteCountryAsync(int id);
        Task<List<GetCountry>> GetCountriesAsync();
        Task<GetCountry> GetCountryAsync(int id);
        Task<bool> UpdateCountryAsync(int id, UpdateCountry updatedCountry);
    }
}