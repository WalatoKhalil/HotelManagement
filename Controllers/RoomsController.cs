using HotelManagementSystem.Models;
using HotelManagementSystem.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // Endpoint to get all rooms
        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetRooms()
        {
            var rooms = await _roomService.GetAsync();
            return Ok(rooms);
        }

        // Endpoint to get a specific room by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(string id)
        {
            var room = await _roomService.GetAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // Endpoint to create a new room
        [HttpPost]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            await _roomService.CreateAsync(room);
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        // Endpoint to update an existing room by Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(string id,[FromBody] Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var result = await _roomService.UpdateAsync(room);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
            
        }

        // Endpoint to delete a room by Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(string id)
        {
            var result = await _roomService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
