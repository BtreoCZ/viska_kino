using Data_Access.Interfaces;
using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class ScreeningRepository : IScreeningRepository
    {
        private readonly IDatabaseAccess _databaseAccess;

        public ScreeningRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IEnumerable<Screening>> GetAllAsync()
        {
            var sql = "SELECT * FROM Screening";
            return await _databaseAccess.ExecuteQueryAsync<Screening>(sql);
        }

        public async Task<Screening> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Screening WHERE Id = @Id";
            var parameters = new { Id = id };
            return (await _databaseAccess.ExecuteQueryAsync<Screening>(sql, parameters)).FirstOrDefault();
        }

        public async Task<Screening> GetScreeningByTimeAsync(DateTime start, DateTime end)
        {
            var sql = "SELECT * FROM Screening WHERE ScreeningTime BETWEEN @Start AND @End";
            var parameters = new { Start = start, End = end };
            return (await _databaseAccess.ExecuteQueryAsync<Screening>(sql, parameters)).FirstOrDefault();
        }

        public async Task AddAsync(Screening screening)
        {
            var sql = "INSERT INTO Screening (ScreeningTime, FilmId, ScreeningRoomId) VALUES (@ScreeningTime, @FilmId, @ScreeningRoomId)";
            await _databaseAccess.ExecuteNonQueryAsync(sql, screening);
        }

        public async Task UpdateAsync(Screening screening)
        {
            var sql = "UPDATE Screening SET ScreeningTime = @ScreeningTime, FilmId = @FilmId, ScreeningRoomId = @ScreeningRoomId WHERE Id = @Id";
            await _databaseAccess.ExecuteNonQueryAsync(sql, screening);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Screening WHERE Id = @Id";
            var parameters = new { Id = id };
            await _databaseAccess.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
