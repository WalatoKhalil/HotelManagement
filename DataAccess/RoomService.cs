using HotelManagementSystem.DataAccess.Services;
using HotelManagementSystem.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HotelManagementSystem.DataAccess
{
    public class RoomService : IRoomService
    {
        private readonly IMongoCollection<Room> _roomsCollection;

        // Constructor to initialize the MongoDB client and collection
        public RoomService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var MongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _roomsCollection = MongoDatabase.GetCollection<Room>(mongoDBSettings.Value.RoomsCollectionName);
        }

        // Gets all room records
        public async Task<List<Room>> GetAsync()
        {
            try
            {
                return await _roomsCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Can't get all rooms.{e}");
            }
        }
        // Gets a specific room record by based on ID
        public async Task<Room> GetAsync(string id)
        {
            try
            {
                return await _roomsCollection.Find(room => room.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Can't get room with Id: {id}. {e}");
            }  
        }

        // Creates a new room record
        public async Task CreateAsync(Room room)
        {
            try
            {
                await _roomsCollection.InsertOneAsync(room);
            }
            catch (Exception e)
            {
                throw new Exception($"Can't create room with Id: {room.Id}. {e}");
            }  
        }

        // Updates an existing room record
        public async Task<bool> UpdateAsync(Room room)
        {
            try
            {
                var result = await _roomsCollection.ReplaceOneAsync(r => r.Id == room.Id, room);

                if (!result.IsAcknowledged || result.ModifiedCount == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Can't update room with Id: {room.Id}. {e}");
            }
        }

        // Deletes a room record based on its ID
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var result = await _roomsCollection.DeleteOneAsync(room => room.Id == id);

                if (!result.IsAcknowledged || result.DeletedCount == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Can't delete room with Id: {id}. {e}");
            }           
        }

    }
}
