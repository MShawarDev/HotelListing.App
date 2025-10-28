using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static List<Hotel> Hotels = new List<Hotel>
         {
             new Hotel { Id = 1, Name = "Hotel One", Address = "123 Main St", PhoneNumber = "555-1234", Rating = 4.7 },
             new Hotel { Id = 2, Name = "Hotel Two", Address = "456 Elm St", PhoneNumber = "555-5678", Rating = 2.5 },
             new Hotel { Id = 3, Name = "Hotel Three", Address = "789 Oak St", PhoneNumber = "555-9012", Rating = 3.4 }
         };

        // GET: api/<HotelsController>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(Hotels);
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotel = Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(hotel);
            }
        }

        // POST api/<HotelsController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
        {
            if (Hotels.Any(h => h.Id == newHotel.Id))
            {
                return BadRequest("Already exist");
            }

            Hotels.Add(newHotel);

            return CreatedAtAction(nameof(Get), new { id = newHotel.Id }, newHotel);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
        {
            var existingHotel = Hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel is null)
            {
                return NotFound();
            }

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;
            existingHotel.PhoneNumber = updatedHotel.PhoneNumber;
            existingHotel.Rating = updatedHotel.Rating;

            return NoContent();
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel is null)
            {
                return NotFound(new { message = "Hotel not found"});
            }

            Hotels.Remove(hotel);

            return NoContent();
        }
    }
}
