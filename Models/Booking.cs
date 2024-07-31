using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagementSystem.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; } = null!;

        public required string RoomId { get; set; } = null!;

        public required string CustomerId { get; set; } = null!;

        public required DateTime CheckIn { get; set; }

        public required DateTime CheckOut { get; set; }

        public required decimal TotalPrice { get; set; }

        public required string Status { get; set; } = null!;
    }
}
