using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController(IHotelsService hotelsService) : ControllerBase
{
    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotel>>> GetHotels()
    {
        return await hotelsService.GetHotelsAsync();
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotel>> GetHotel(int id)
    {
        return await hotelsService.GetHotelAsync(id);
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

        try
        {
            await hotelsService.UpdateHotelAsync(id, updatedHotel);
        }
        catch (KeyNotFoundException)
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

    // POST: api/Hotels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(CreateHotel newHotel)
    {
        try
        {
            var createdHotel = await hotelsService.CreateHotelAsync(newHotel);

            return CreatedAtAction("GetHotel", new { id = createdHotel.Id }, createdHotel);
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

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        try
        {
            await hotelsService.DeleteHotelAsync(id);
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
