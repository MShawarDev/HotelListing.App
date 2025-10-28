using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(AppDBContext context) : ControllerBase
{
    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountry>>> GetCountries()
    {
        return await context.Countries
            .Select(c => new GetCountry(c.Id, c.Name, c.ShortName))
            .ToListAsync();
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountry>> GetCountry(int id)
    {
        var country = await context.Countries.Include(c => c.Hotels).FirstOrDefaultAsync(c => c.Id == id);
        if (country == null)
        {
            return NotFound();
        }

        return new GetCountry(country.Id, country.Name, country.ShortName);
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountry updatedCountry)
    {
        if (id != updatedCountry.Id)
        {
            return BadRequest();
        }

        var country = await context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
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
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateCountry newCountry)
    {
        var country = new Country
        {
            Name = newCountry.Name,
            ShortName = newCountry.ShortName
        };

        context.Countries.Add(country);

        await context.SaveChangesAsync();

        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        context.Countries.Remove(country);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.Id == id);
    }
}
