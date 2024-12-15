using Data_Access.Interfaces;
using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class ScreeningRoomRepository : IScreeningRoomRepository
    {
        private readonly IDatabaseAccess _databaseAccess;

        public ScreeningRoomRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IEnumerable<ScreeningRoom>> GetAllAsync()
        {
            var sql = "SELECT * FROM ScreeningRooms";
            return await _databaseAccess.ExecuteQueryAsync<ScreeningRoom>(sql);
        }

        public async Task<ScreeningRoom> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM ScreeningRooms WHERE Id = @Id";
            var parameters = new { Id = id };
            return (await _databaseAccess.ExecuteQueryAsync<ScreeningRoom>(sql, parameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<ScreeningRoom>> GetAvailableRoomsAsync()
        {
            var sql = "SELECT * FROM ScreeningRooms WHERE Capacity > 0";
            return await _databaseAccess.ExecuteQueryAsync<ScreeningRoom>(sql);
        }

        public async Task AddAsync(ScreeningRoom room)
        {
            var sql = "INSERT INTO ScreeningRooms (Name, Capacity) VALUES (@Name, @Capacity)";
            await _databaseAccess.ExecuteNonQueryAsync(sql, room);
        }

        public async Task UpdateAsync(ScreeningRoom room)
        {
            var sql = "UPDATE ScreeningRooms SET Name = @Name, Capacity = @Capacity WHERE Id = @Id";
            await _databaseAccess.ExecuteNonQueryAsync(sql, room);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM ScreeningRoom WHERE Id = @Id";
            var parameters = new { Id = id };
            await _databaseAccess.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
