using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountriesService countriesService) : ControllerBase
{
    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountry>>> GetCountries()
    {
        var countries = await countriesService.GetCountriesAsync();

        return countries;
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountry>> GetCountry(int id)
    {
        var country = await countriesService.GetCountryAsync(id);
        if( country == null)
        {
            return NotFound();
        }

        return country;
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

        try
        {
            await countriesService.UpdateCountryAsync(id, updatedCountry);
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // Log error

            return Problem(detail: "An error occured while updating", statusCode: StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateCountry newCountry)
    {
        try
        {
            var createdCountry = await countriesService.CreateCountryAsync(newCountry);

            return CreatedAtAction("GetCountry", new { id = createdCountry.Id }, createdCountry);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // Log error

            return Problem(detail: "An error occured while creating", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        try
        {
            await countriesService.DeleteCountryAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // Log error

            return Problem(detail: "An error occured while deleting", statusCode: StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}
