using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagement.Models
{
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("BookingId")]
        public string BookingId { get; set; } = string.Empty;

        [BsonElement("Amount")]
        public decimal Amount { get; set; }

        [BsonElement("PaymentDate")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [BsonElement("Status")]
        public string Status { get; set; } = "Success";
    }
}
