using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagement.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }    // ❗ Nullable

        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("Password")]
        public string? Password { get; set; }

        [BsonElement("Role")]
        public string? Role { get; set; } = "User";

        [BsonElement("ProfileImage")]
        public string? ProfileImage { get; set; }

        [BsonElement("Phone")]
        public string? Phone { get; set; }

        [BsonElement("Address")]
        public string? Address { get; set; }
    }
}
