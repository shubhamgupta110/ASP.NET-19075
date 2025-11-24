using HotelManagement.Data;
using HotelManagement.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using HotelManagement.Services;  // ✅ Add this line


namespace HotelManagement.Repositories
{
    public class RoomRepository
    {
        private readonly IMongoCollection<Room> _roomsCollection;

        public RoomRepository(MongoService mongoService)
        {
            // ✅ Correct way to get collection from MongoDB
            var database = mongoService.GetDatabase();
            _roomsCollection = database.GetCollection<Room>("Rooms");
        }

        // ✅ Get all rooms
        public List<Room> GetAllRooms()
        {
            return _roomsCollection.Find(_ => true).ToList();
        }

        // ✅ Add new room (optional)
        public void AddRoom(Room room)
        {
            _roomsCollection.InsertOne(room);
        }

        // ✅ Get room by Id
        public Room GetRoomById(string id)
        {
            return _roomsCollection.Find(r => r.Id == id).FirstOrDefault();
        }

        // ✅ Delete room
        public void DeleteRoom(string id)
        {
            _roomsCollection.DeleteOne(r => r.Id == id);
        }
    }
}
