using MongoDB.Driver;
using HotelManagement.Models;

namespace HotelManagement.Services
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService(IConfiguration config)
        {
            var connectionString = config["MongoDbSettings:ConnectionString"];
            var databaseName = config["MongoDbSettings:DatabaseName"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoDatabase GetDatabase() => _database;

        // ✅ Collections
        private IMongoCollection<User> UsersCollection =>
            _database.GetCollection<User>("Users");

        private IMongoCollection<Booking> BookingsCollection =>
            _database.GetCollection<Booking>("Bookings");

        private IMongoCollection<Room> RoomsCollection =>
            _database.GetCollection<Room>("Rooms");

        // ✅ Add user
        public void AddUser(User user)
        {
            UsersCollection.InsertOne(user);
        }

        // ✅ Validate user login
        public User? ValidateUser(string email, string password)
        {
            return UsersCollection
                .Find(u => u.Email == email && u.Password == password)
                .FirstOrDefault();
        }

        // ✅ Get all users
        public List<User> GetAllUsers()
        {
            return UsersCollection.Find(_ => true).ToList();
        }

        // ✅ Get all rooms
        public List<Room> GetRooms()
        {
            return RoomsCollection.Find(_ => true).ToList();
        }

        // ✅ Get all bookings
        public List<Booking> GetAllBookings()
        {
            return BookingsCollection.Find(_ => true).ToList();
        }

        // ✅ Get bookings for a specific user
        public List<Booking> GetUserBookings(string userName)
        {
            return BookingsCollection
                .Find(b => b.GuestName == userName)
                .ToList();
        }

        // ✅ Get payment info (for dashboard)
        public List<Booking> GetPayments()
        {
            return BookingsCollection
                .Find(b => b.PaymentStatus == "Paid" || b.PaymentStatus == "Pending")
                .ToList();
        }
    }
}
