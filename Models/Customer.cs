using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagementSystem.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; } = null!;

        public required string Name { get; set; } = null!;

        public string? Address { get; set; }

        public required string Email { get; set; } = null!;

        public required string Phone { get; set; } = null!;
    }
}
