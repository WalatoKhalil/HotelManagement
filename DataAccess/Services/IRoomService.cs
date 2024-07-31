using HotelManagementSystem.Models;

namespace HotelManagementSystem.DataAccess.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetAsync();
        Task<Room> GetAsync(string id);
        Task CreateAsync(Room room);
        Task<bool> UpdateAsync(Room room);
        Task<bool> DeleteAsync(string id);
    }
}
