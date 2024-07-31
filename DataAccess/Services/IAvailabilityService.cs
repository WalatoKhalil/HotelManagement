using HotelManagementSystem.Models;

namespace HotelManagementSystem.DataAccess.Services
{
    public interface IAvailabilityService
    {
        Task<List<Availability>> GetAsync();
        Task<Availability?> GetAsync(Availability availability);
        Task CreateAsync(Availability availability);
        Task<bool> UpdateAsync(Availability availability);
        Task<bool> DeleteAsync(string roomId, DateTime checkIn, DateTime checkOut);
    }
}
