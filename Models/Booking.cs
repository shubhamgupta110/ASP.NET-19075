using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagement.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("UserName")]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("RoomId")]
        public string RoomId { get; set; } = string.Empty;

        [BsonElement("RoomNumber")]
        public string RoomNumber { get; set; } = string.Empty;

        [BsonElement("RoomType")]
        public string RoomType { get; set; } = string.Empty;

        [BsonElement("CheckInDate")]
        public DateTime CheckInDate { get; set; }

        [BsonElement("CheckOutDate")]
        public DateTime CheckOutDate { get; set; }

        [BsonElement("Guests")]
        public int Guests { get; set; }

        [BsonElement("TotalAmount")]
        public double TotalAmount { get; set; }

        [BsonElement("PaymentStatus")]
        public string PaymentStatus { get; set; } = "Pending";

        [BsonElement("PaymentDate")]
        public DateTime? PaymentDate { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; } = "Booked";

        [BsonElement("Rating")]
        public double? Rating { get; set; }

        [BsonElement("GuestName")]
        public string GuestName { get; set; } = string.Empty;
    }
}
