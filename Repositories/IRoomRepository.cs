using HotelManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(string id);
        Task CreateAsync(Room room);
        Task UpdateAsync(string id, Room room);
        Task DeleteAsync(string id);
    }
}
