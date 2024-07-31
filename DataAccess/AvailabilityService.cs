using HotelManagementSystem.Models;
using HotelManagementSystem.DataAccess.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HotelManagementSystem.DataAccess
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IMongoCollection<Availability> _availabilityCollection;

        // Constructor to initialize the MongoDB client and collection
        public AvailabilityService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _availabilityCollection = mongoDatabase.GetCollection<Availability>(mongoDBSettings.Value.AvailabilityCollectionName);
        }

        // Retrieves all availability records
        public async Task<List<Availability>> GetAsync()
        {
            try
            {
                return await _availabilityCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Can't get all availability records. {e}");
            }
        }

        // Retrieves a specific availability based on Room ID, CheckIn, CheckOut
        public async Task<Availability?> GetAsync(Availability availability)
        {
            NormalizeDates(availability);
            try
            {
                return await _availabilityCollection.Find(a => a.RoomId == availability.RoomId && a.CheckIn == availability.CheckIn && a.CheckOut == availability.CheckOut).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to retrieve availability for the specified room and dates.", e);
            }
        }

        // Creates a new availability 
        public async Task CreateAsync(Availability availability)
        {
            NormalizeDates(availability);
            try
            {
                await _availabilityCollection.InsertOneAsync(availability);
            }
            catch (Exception e)
            {
                throw new Exception($"Can't create availability record for RoomId: {availability.RoomId}. {e}");
            }
        }

        // Updates an existing availability
        public async Task<bool> UpdateAsync(Availability availability)
        {
            NormalizeDates(availability);
            try
            {
                var filter = Builders<Availability>.Filter.Where(a => a.RoomId == availability.RoomId && a.CheckIn == availability.CheckIn && a.CheckOut == availability.CheckOut);
                var result = await _availabilityCollection.ReplaceOneAsync(filter, availability);

                if (!result.IsAcknowledged || result.ModifiedCount == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Can't update availability record for RoomId: {availability.RoomId}. {e}");
            }
        }

        // Deletes an availability based on Room ID, CheckIn, CheckOut
        public async Task<bool> DeleteAsync(string roomId, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                var filter = Builders<Availability>.Filter.Where(a => a.RoomId == roomId && a.CheckIn == checkIn && a.CheckOut == checkOut);
                var result = await _availabilityCollection.DeleteOneAsync(filter);

                if (!result.IsAcknowledged || result.DeletedCount == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to delete availability record. {e}");
            }
        }

        private void NormalizeDates(Availability availability)
        {
            availability.CheckIn = availability.CheckIn.Date;
            availability.CheckOut = availability.CheckOut.Date;
        }
    }
}
