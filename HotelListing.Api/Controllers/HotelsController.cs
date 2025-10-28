using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController(AppDBContext context) : ControllerBase
{
    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotel>>> GetHotels()
    {
        var hotels = await context.Hotels.Include(h => h.Country)
            .Select(h => new GetHotel(h.Id, h.Name, h.Address, h.PhoneNumber, h.Rating, h.CountryId, h.Country!.Name))
            .ToListAsync();

        return hotels;
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotel>> GetHotel(int id)
    {
        var hotel = await context.Hotels.Include(h => h.Country).FirstOrDefaultAsync(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }

        return new GetHotel(id, hotel.Name, hotel.Address, hotel.PhoneNumber, hotel.Rating, hotel.CountryId, hotel.Country!.Name);
    }

    // PUT: api/Hotels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, UpdateHotel updatedHotel)
    {
        if (id != updatedHotel.Id)
        {
            return BadRequest();
        }

        var hotel = await context.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
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
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Hotels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(CreateHotel newHotel)
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

        return CreatedAtAction("GetHotel", new { id = hotel.Id }, newHotel);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await context.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        context.Hotels.Remove(hotel);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> HotelExistsAsync(int id)
    {
        return await context.Hotels.AnyAsync(e => e.Id == id);
    }
}
