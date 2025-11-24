using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagement.Models
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; } = "";
        public string Message { get; set; } = "";
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
