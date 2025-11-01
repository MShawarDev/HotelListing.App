using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Services
{
    public class CountriesService(AppDBContext context) : ICountriesService
    {
        public async Task<List<GetCountry>> GetCountriesAsync()
        {
            return await context.Countries
                        .Select(c => new GetCountry(c.Id, c.Name, c.ShortName, c.Hotels.Select(h => new GetCountryHotel
                        {
                            Id = h.Id,
                            Name = h.Name,
                            Address = h.Address,
                            Rating = h.Rating
                        }).ToList()))
                        .ToListAsync();
        }

        public async Task<GetCountry> GetCountryAsync(int id)
        {
            var country = await context.Countries.FindAsync(id);
            if (country == null)
            {
                return null!;
            }

            return new GetCountry(country.Id, country.Name, country.ShortName, country.Hotels.Select(h => new GetCountryHotel
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                Rating = h.Rating
            }).ToList());
        }

        public async Task<bool> CountryExistsAsync(int id)
        {
            return await context.Countries.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CountryExistsAsync(string name)
        {
            return await context.Countries.AnyAsync(e => e.Name == name);
        }

        public async Task<GetCountry> CreateCountryAsync(CreateCountry newCountry)
        {
            var country = new Country
            {
                Name = newCountry.Name,
                ShortName = newCountry.ShortName
            };

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            return new GetCountry(country.Id, country.Name, country.ShortName, new List<GetCountryHotel>());
        }

        public async Task<bool> UpdateCountryAsync(int id, UpdateCountry updatedCountry)
        {
            var country = await context.Countries.FindAsync(id);
            if (country == null)
            {
                throw new KeyNotFoundException("Country Not Found.");
            }

            country.Name = updatedCountry.Name;
            country.ShortName = updatedCountry.ShortName;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExistsAsync(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task DeleteCountryAsync(int id)
        {
            await context.Countries.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
