using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HotelManagement.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IMongoCollection<Room> _roomsCollection;
        private readonly IMongoCollection<Booking> _bookingsCollection;

        public RoomsController(IMongoDatabase database)
        {
            _roomsCollection = database.GetCollection<Room>("Rooms");
            _bookingsCollection = database.GetCollection<Booking>("Bookings");
        }

        // ✅ STEP 1: INDEX PAGE (List All Rooms)
        public IActionResult Index()
        {
            var rooms = _roomsCollection.Find(_ => true).ToList(); // fetch all rooms
            return View(rooms); // Views/Rooms/Index.cshtml
        }

        // ✅ STEP 2: BOOK PAGE (GET)
        // URL: /Rooms/Book?roomId=xxxx
        public IActionResult Book(string roomId)
        {
            var room = _roomsCollection.Find(r => r.Id == roomId).FirstOrDefault();

            if (room == null)
                return NotFound("❌ Room not found!");

            return View(room); // ✅ ye "Views/Rooms/Book.cshtml" ko call karega
        }

        // ✅ STEP 3: CONFIRM BOOKING (POST)
        [HttpPost]
        public IActionResult ConfirmBooking(string roomId, string fullName, string email,
            DateTime checkInDate, DateTime checkOutDate, int guests)
        {
            var room = _roomsCollection.Find(r => r.Id == roomId).FirstOrDefault();

            if (room == null)
                return NotFound("❌ Room not found for booking!");

            var totalDays = (checkOutDate - checkInDate).TotalDays;
            if (totalDays <= 0)
            {
                TempData["Message"] = "⚠️ Invalid date range!";
                return RedirectToAction("BookingConfirmation");
            }

            var booking = new Booking
            {
                RoomId = roomId,
                UserName = fullName,
                Email = email,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                Guests = guests,
                RoomType = room.Type,
                RoomNumber = room.RoomNumber.ToString(),
                TotalAmount = (double)totalDays * (double)room.PricePerNight
            };

            _bookingsCollection.InsertOne(booking);

            TempData["Message"] = $"✅ Booking confirmed for {room.Type} (Room {room.RoomNumber})!";
            return RedirectToAction("BookingConfirmation");
        }

        // ✅ STEP 4: CONFIRMATION VIEW
        public IActionResult BookingConfirmation()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
    }
}
