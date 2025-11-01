using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HotelListing.Api.Services
{
    public class HotelsService(AppDBContext context) : IHotelsService
    {
        public async Task<List<GetHotel>> GetHotelsAsync()
        {
            return await context.Hotels
                        .Select(h => new GetHotel(h.Id, h.Name, h.Address, h.PhoneNumber, h.Rating, h.CountryId, h.Country!.Name)).ToListAsync();
        }

        public async Task<GetHotel> GetHotelAsync(int id)
        {
            var hotel = await context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return null!;
            }

            return new GetHotel(hotel.Id, hotel.Name, hotel.Address, hotel.PhoneNumber, hotel.Rating, hotel.CountryId, hotel.Country!.Name);
        }

        public async Task<bool> HotelExistsAsync(int id)
        {
            return await context.Hotels.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> HotelExistsAsync(string name)
        {
            return await context.Hotels.AnyAsync(e => e.Name == name);
        }

        public async Task<GetHotel> CreateHotelAsync(CreateHotel newHotel)
        {
            var hotel = new Hotel
            {
                Name = newHotel.Name,
                Address = newHotel.Address,
                PhoneNumber = newHotel.PhoneNumber,
                Rating = newHotel.Rating,
                CountryId = newHotel.CountryId
            };

            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();

            return new GetHotel(hotel.Id, hotel.Name, hotel.Address, hotel.PhoneNumber, hotel.Rating, hotel.CountryId, hotel.Country!.Name);
        }

        public async Task<bool> UpdateHotelAsync(int id, UpdateHotel updatedHotel)
        {
            var hotel = await context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                throw new KeyNotFoundException("Hotel Not Found.");
            }

            hotel.Name = updatedHotel.Name;
            hotel.Address = updatedHotel.Address;
            hotel.PhoneNumber = updatedHotel.PhoneNumber;
            hotel.Rating = updatedHotel.Rating;
            hotel.CountryId = updatedHotel.CountryId;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExistsAsync(id))
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

        public async Task DeleteHotelAsync(int id)
        {
            await context.Hotels.Where(h => h.Id == id).ExecuteDeleteAsync();
        }
    }
}
