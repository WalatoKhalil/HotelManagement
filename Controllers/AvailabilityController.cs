using HotelManagementSystem.Models;
using HotelManagementSystem.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        
        // Constructor to initialize the availability service
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        // Endpoint to get all availability records
        [HttpGet]
        public async Task<ActionResult<List<Availability>>> GetAvailabilities()
        {
            var availabilities = await _availabilityService.GetAsync();
            return Ok(availabilities);
        }

        // Endpoint to check availability based on Room ID, CheckIn, CheckOut
        [HttpPost("check")]
        public async Task<ActionResult<Availability>> GetAvailability([FromBody] Availability availability)
        {
            var foundAvailability = await _availabilityService.GetAsync(availability);
            if (foundAvailability == null)
            {
                return NotFound("No availability found for the specified room and dates.");
            }
            return Ok(foundAvailability);
        }

        // Endpoint to create a new availability record
        [HttpPost]
        public async Task<ActionResult<Availability>> CreateAvailability(Availability availability)
        {
            await _availabilityService.CreateAsync(availability);
            return CreatedAtAction("GetAvailability", new { roomId = availability.RoomId, checkIn = availability.CheckIn, checkOut = availability.CheckOut }, availability);
        }

        // Endpoint to update an existing availability record
        [HttpPut]
        public async Task<IActionResult> UpdateAvailability(Availability availability)
        {
            var result = await _availabilityService.UpdateAsync(availability);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Endpoint to delete an availability record based on room ID and dates
        [HttpDelete]
        public async Task<IActionResult> DeleteAvailability(string roomId, DateTime checkIn, DateTime checkOut)
        {
            var result = await _availabilityService.DeleteAsync(roomId, checkIn.Date, checkOut.Date);
            if (!result)
            {
                return NotFound("Failed to delete availability");
            }
            return NoContent();
        }
    }
}
