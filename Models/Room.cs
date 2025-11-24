using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagement.Models
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("RoomNumber")]
        public int RoomNumber { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("Type")]
        public string Type { get; set; } = string.Empty;

        [BsonElement("PricePerNight")]
        public decimal PricePerNight { get; set; }

        [BsonElement("IsAvailable")]
        public bool IsAvailable { get; set; } = true;

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonElement("Ratings")]
        public List<double> Ratings { get; set; } = new(); // simplified ✅

        [BsonElement("AverageRating")]
        public double AverageRating { get; set; } = 0;
    }
}
