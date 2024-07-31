using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagementSystem.Models
{
    public class Room
    {
        // Unique identifier for the room
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; } = null!; 

        // Room category name like Deluxe, Single, Suite
        [BsonElement("Category")]
        public required string CategoryName { get; set; } = null!; 

        public string? Description { get; set; }

        public required int Capacity { get; set; }

        public required decimal PricePerNight { get; set; }

        public List<string>? Images { get; set; }
    }
}

